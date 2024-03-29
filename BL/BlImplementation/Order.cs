﻿using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static BO.Enums;
using static DO.Exceptions;

namespace BlImplementation;

internal class Order : IOrder
{
    DalApi.IDal? Dal = DalApi.Factory.Get();

    public IEnumerable<BO.OrderForList> GetOrders()
    {
        IEnumerable<DO.Orders?> ordersFromDal = Dal.order.GetAll();

        var ordersList = ordersFromDal
                       .Where(order => order != null)
                       .Select(order => new BO.OrderForList()
                       {
                           Id = order.Value._orderId,
                           Name = order.Value._customerName,
                           Status = findStatus(order.Value),
                           AmountProducts = findAmountOfItems(order.Value._orderId),
                           TotalPrice = findTotalPrice(order.Value._orderId)
                       }).ToList();


        return ordersList;
    }


    private StatusOrder findStatus(DO.Orders order)
    {
        StatusOrder status = StatusOrder.OrderCommited;
        if (order._shippingDate != DateTime.MinValue && order._shippingDate < DateTime.Now)
            status = StatusOrder.OrderShipped;
        if (order._deliveryDate != DateTime.MinValue && order._deliveryDate < DateTime.Now)
            status = StatusOrder.OrderDelivered;
        return status;
    }
    private int findAmountOfItems(int orderID)
    {
        IEnumerable<DO.OrderItem?> orderItems = Dal.orderItem.GetAll();
        int count = 0;
        var myOrderItems = orderItems
            .Where(orderItem => orderItem != null && orderItem.Value._orderId == orderID)
            .Select(orderItem => orderItem);

        foreach (var item in myOrderItems)
            count += item!.Value._quantity;
        return count;
    }

    private double findTotalPrice(int orderID)
    {
        IEnumerable<DO.OrderItem?> orderItems = Dal.orderItem.GetAll();
        double totalPrice = 0;
        foreach (DO.OrderItem? orderItem in orderItems)
        {
            if (orderItem != null)
                if (orderItem.Value._orderId == orderID)
                {
                    totalPrice += orderItem.Value._pricePerUnit * orderItem.Value._quantity;
                }
        }
        return totalPrice;
    }

    public BO.Order GetOrder(int id)
    {
        if (id < 0)
            throw new BO.InvalidValueException("not valid id");
        DO.Orders getO = new DO.Orders();
        try
        {
            getO = Dal.order.Get(order => order!.Value._orderId == id);
        }
        catch (DO.Exceptions.RequestedItemNotFoundException ex)
        {
            throw new BO.NotFound("order not found", ex);
        }
        IEnumerable<DO.OrderItem?> OrderItemsDal = new List<DO.OrderItem?>();
        try
        {
            OrderItemsDal = Dal.orderItem.GetAll(item => item!.Value._orderId == id);
        }
        catch (DO.Exceptions.RequestedItemNotFoundException ex)
        {
            throw new BO.NotFound("items not found", ex);
        }
        BO.Order order = new BO.Order()
        {
            Id = id,
            CustomerName = getO._customerName,
            Email = getO._email,
            Address = getO._address,
            OrderStatus = findStatus(getO),
            PlaceOrderDate = getO._orderDate,
            ExpeditionDate = getO._shippingDate,
            DeliveryDate = getO._deliveryDate,
            ItemList = getOrderItem(OrderItemsDal),
            TotalPrice = findTotalPrice(id)
        };
        return order;
    }
    public BO.Order UpdateShippingOrder(int id)
    {
        DO.Orders updateOForDal = Dal.order.Get(order => order!.Value._orderId == id);
        BO.Order updateOForBL = GetOrder(id);

        if (updateOForDal._shippingDate == DateTime.MinValue)
        {
            updateOForDal._shippingDate = DateTime.Now;
            updateOForBL.ExpeditionDate = DateTime.Now;
        }
        else
            throw new BO.TheOperationFailed("The date already updated!");
        try
        {
            Dal.order.Update(updateOForDal);
        }
        catch
        {
            throw new BO.TheOperationFailed("couldnt update date");
        }

        return updateOForBL;
    }
    public BO.Order UpdateDeliveryOrder(int id)
    {
        if (id < 0)
            throw new BO.InvalidValueException("not valid id");
        DO.Orders updateOForDal = Dal.order.Get(order => order!.Value._orderId == id);
        BO.Order updateOForBL = GetOrder(id);
        if (updateOForDal._deliveryDate == DateTime.MinValue && updateOForDal._shippingDate < DateTime.Now)
        {
            updateOForDal._deliveryDate = DateTime.Now;
            updateOForBL.DeliveryDate = DateTime.Now;
        }
        else
            throw new BO.TheOperationFailed("The date already updated!");
        try
        {
            Dal.order.Update(updateOForDal);
        }
        catch
        {
            throw new BO.TheOperationFailed("couldnt update date");
        }

        return updateOForBL;
    }
    public BO.OrderTracking trackingOrder(int id)
    {
        DO.Orders updateOForDal;
        if (id < 0)
            throw new BO.InvalidValueException("not valid id");
        try
        {
            updateOForDal = Dal.order.Get(order => order!.Value._orderId == id);
            List<BO.DescriptionStatusDate?> listOfStatus = new List<BO.DescriptionStatusDate?>();
            for (int i = 0; i < 3; i++)
            {
                BO.DescriptionStatusDate status = new BO.DescriptionStatusDate();
                if (i == 0)
                {
                    status.Date = updateOForDal._orderDate;
                    status.Description = "The order was created";
                    listOfStatus.Add(status);
                }
                if (i == 1)
                {
                    if (updateOForDal._shippingDate < DateTime.Now && updateOForDal._shippingDate != DateTime.MinValue)
                    {
                        status.Date = updateOForDal._shippingDate;
                        status.Description = "The order was sent";
                        listOfStatus.Add(status);
                    }

                }
                if (i == 2)
                {
                    if (updateOForDal._deliveryDate < DateTime.Now && updateOForDal._shippingDate != DateTime.MinValue)
                    {
                        status.Date = updateOForDal._deliveryDate;
                        status.Description = "The order was deliveried";
                        listOfStatus.Add(status);
                    }
                }
            }
            BO.OrderTracking orderTracking = new BO.OrderTracking()
            {
                Id = id,
                Status = findStatus(updateOForDal),
                DescriptionStatus = listOfStatus

            };
            return orderTracking;
        }
        catch (RequestedItemNotFoundException ex)
        {
            throw new BO.InvalidValueException(ex.Message);
        }


    }

    private List<BO.OrderItem?> getOrderItem(IEnumerable<DO.OrderItem?> OrderItemsDal)
    { 
        var OrderItemsBL = OrderItemsDal
                      .Where(orderItem => orderItem != null)
                      .Select(orderItem => new BO.OrderItem()
                      {
                          Id = orderItem.Value._id,
                          Name = Dal.product.Get(order => order!.Value._productId == orderItem.Value._productId)._productName,
                          IdOrder = orderItem.Value._orderId,
                          Price = orderItem.Value._pricePerUnit,
                          AmountItems = orderItem.Value._quantity,
                          TotalPriceItem = orderItem.Value._pricePerUnit * orderItem.Value._quantity
                      }) ;

        return OrderItemsBL.ToList();
    }

    //private OrderForList getMin(List<BO.OrderForList> list , StatusOrder status )
    //{
    //    OrderForList MinOrder = list[0];
    //    foreach (OrderForList order in list)
    //    {
    //       if(status== StatusOrder.OrderCommited)
    //        {
    //            if (GetOrder(order.Id).PlaceOrderDate < GetOrder(MinOrder.Id).PlaceOrderDate)
    //                MinOrder = order;
    //        }
    //       else
    //        {
    //            if (GetOrder(order.Id).ExpeditionDate < GetOrder(MinOrder.Id).ExpeditionDate)
    //                MinOrder = order;
    //        }

    //    }
    //    return MinOrder;
    //}
    //public int? selectOrderToTreatment()
    //{   List<BO.OrderForList>? listOrderIsCreated = GetOrders().Where(order=>order.Status==StatusOrder.OrderCommited).Select(order=>order).ToList();
    //    List<BO.OrderForList>? listOrderIsShipped = GetOrders().Where(order => order.Status == StatusOrder.OrderShipped).Select(order => order).ToList();
    //    if(listOrderIsCreated==null&& listOrderIsShipped==null)
    //    {
    //        return null;
    //    }
    //    OrderForList MinOrderIsCreated = getMin(listOrderIsCreated!, StatusOrder.OrderCommited);
    //    OrderForList MinOrderIsShipped = getMin(listOrderIsShipped, StatusOrder.OrderShipped);
    //    if (GetOrder(MinOrderIsCreated.Id).PlaceOrderDate < GetOrder(MinOrderIsShipped.Id).ExpeditionDate)
    //        return MinOrderIsCreated.Id;
    //    else
    //        return MinOrderIsShipped.Id;


    //}

    public int? selectOrderToTreatment()
    {
        DateTime minDate = DateTime.Now;
        int? orderId = null;
        List<OrderForList>? orderList = GetOrders().ToList();
        orderList?.ForEach(o =>

        {
            switch (o.Status)
            {
                case StatusOrder.OrderCommited:
                    if (GetOrder(o.Id).PlaceOrderDate < minDate)
                    {
                        orderId = o.Id;
                        minDate = (DateTime)GetOrder(o.Id).PlaceOrderDate;
                    }
                    break;
                case StatusOrder.OrderShipped:
                    if (GetOrder(o.Id).ExpeditionDate < minDate)
                    {
                        orderId = o.Id;
                        minDate = (DateTime)GetOrder(o.Id).ExpeditionDate;
                    }
                    break;
                default:
                    break;
            }
        });
        return orderId;
    }
}
