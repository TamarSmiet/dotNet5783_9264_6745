using BlApi;
using BO;
using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BlImplementation;

internal class Order : IOrder
{
    IDal Dal = new DalList();

   public IEnumerable<BO.OrderForList> GetOrders()
    {
        IEnumerable<DO.Orders> ordersFromDal = Dal.order.GetAll();
        List<BO.OrderForList> orderForLists = new List<BO.OrderForList>();
        foreach (DO.Orders order in ordersFromDal)
        {
            BO.OrderForList orderForListToAdd = new BO.OrderForList()
            {
                Id = order._orderId,
                Name = order._customerName,
                Status = findStatus(order),
                AmountProducts = findAmountOfItems(order._orderId),
                TotalPrice = findTotalPrice(order._orderId)
            };
            orderForLists.Add(orderForListToAdd);
        }
        return orderForLists;
    }
    private StatusOrder findStatus(DO.Orders order)
    {
        StatusOrder status = StatusOrder.OrderCommited;
        if (order._shippingDate < DateTime.Now)
            status = StatusOrder.OrderShipped;
        if (order._deliveryDate < DateTime.Now)
            status = StatusOrder.OrderDelivered;
        return status;
    }
    private int findAmountOfItems(int orderID)
    {
        IEnumerable<DO.OrderItem> orderItems = Dal.orderItem.GetAll();
        int amountOfItems = 0;
        foreach (DO.OrderItem orderItem in orderItems)
        {
            if (orderItem._id == orderID)
            {
                amountOfItems++;
            }
        }
        return amountOfItems;
    }

    private double findTotalPrice(int orderID)
    {
        IEnumerable<DO.OrderItem> orderItems = Dal.orderItem.GetAll();
        double totalPrice = 0;
        foreach (DO.OrderItem orderItem in orderItems)
        {
            if (orderItem._orderId == orderID)
            {
                totalPrice += orderItem._pricePerUnit * orderItem._quantity;
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
            getO= Dal.order.Get(id);
        }
         catch(DO.Exceptions.RequestedItemNotFoundException ex)
        {
            throw new BO.NotFound("order not found", ex);
        }
        IEnumerable<DO.OrderItem> OrderItemsDal = new List<DO.OrderItem>();
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
        DO.Orders updateOForDal = Dal.order.Get(id);
        BO.Order updateOForBL = GetOrder(id);

        if (updateOForDal._shippingDate==DateTime.MinValue)
        {  
            updateOForDal._shippingDate = DateTime.Now;
            updateOForBL.ExpeditionDate = DateTime.Now;
        }
        try
        {
            Dal.order.Update(updateOForDal);
        }
        catch
        {
            throw new  BO.TheOperationFailed("couldnt update date");
        }
       
        return updateOForBL;
    }
    public BO.Order UpdateDeliveryOrder(int id)
    {
        if (id < 0)
            throw new BO.InvalidValueException("not valid id");
        DO.Orders updateOForDal = Dal.order.Get(id);
        BO.Order updateOForBL = GetOrder(id);
        if (updateOForDal._deliveryDate==DateTime.MinValue&& updateOForDal._shippingDate<DateTime.Now)
        {
            updateOForDal._deliveryDate= DateTime.Now;
            updateOForBL.DeliveryDate= DateTime.Now;
        }
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
        if (id < 0)
            throw new BO.InvalidValueException("not valid id");
        DO.Orders updateOForDal = Dal.order.Get(id);
        List<BO.DescriptionStatusDate> listOfStatus = new List<BO.DescriptionStatusDate>(); 
        for(int i=0;i<3;i++)
        {
            BO.DescriptionStatusDate status = new BO.DescriptionStatusDate();
            if(i==0)
            {
                status.Date = updateOForDal._orderDate;
                status.Description = "The order was created";
                listOfStatus.Add(status);
            }
            if(i==1)
            {
                if(updateOForDal._shippingDate < DateTime.Now)
                {
                    status.Date = updateOForDal._shippingDate;
                    status.Description = "The order was sent";
                    listOfStatus.Add(status);
                }
                
            }
            if(i==2)
            {
                if (updateOForDal._deliveryDate < DateTime.Now)
                {
                    status.Date= updateOForDal._deliveryDate;
                    status.Description = "The order was deliveried";
                    listOfStatus.Add(status);
                }
            }
        }
        BO.OrderTracking orderTracking = new BO.OrderTracking()
        {
            Id= id, 
            Status=findStatus(updateOForDal),
            DescriptionStatus=listOfStatus

        };
        return orderTracking;
    }

    private List<BO.OrderItem> getOrderItem(IEnumerable<DO.OrderItem> OrderItemsDal)
    {
        List<BO.OrderItem> OrderItemsBL = new List<BO.OrderItem>();
        foreach (DO.OrderItem orderItem in OrderItemsDal)
        {
            BO.OrderItem orderItemBL = new BO.OrderItem()
            {
                Id = orderItem._id,
                Name = Dal.product.Get(orderItem._productId)._productName,//try
                IdOrderItem = orderItem._productId,
                Price = orderItem._pricePerUnit,
                AmountItems = orderItem._quantity,
                TotalPriceItem = orderItem._pricePerUnit * orderItem._quantity
            };
            OrderItemsBL.Add(orderItemBL);
        }
        return OrderItemsBL;
    }
}
