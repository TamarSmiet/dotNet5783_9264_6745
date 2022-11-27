using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Cart:ICart
{
    IDal Dal = new DalList();
    public BO.Cart AddProductToCart(BO.Cart cart, int id)
    {
        DO.Products getP=new DO.Products();
        DO.OrderItem getOI = new DO.OrderItem();
        try
        {
            getP = Dal.product.Get(id);
        }
        catch (DO.Exceptions.RequestedItemNotFoundException ex)
        {
            throw new BO.NotFound("product not found", ex);
        }
        try
        {
            getOI = Dal.orderItem.Get(id);
        }
        catch (DO.Exceptions.RequestedItemNotFoundException ex)
        {
            throw new BO.NotFound("order item not found", ex);
        }
        if(cart.Orders.Count()!=0)
        {
            foreach (BO.OrderItem item in cart.Orders )
                if (item.IdOrderItem == id)
                {
                    if (getP._amountInStock > 0)
                    {
                        item.AmountItems++;
                        item.TotalPriceItem += item.Price;
                        cart.TotalPriceCart += item.Price;
                        return cart;
                    }
                    else
                        throw new BO.InvalidValueException("not valid amount in stock");
                }
        }
        
        IEnumerable<DO.Products> productListFromDo = Dal.product.GetAll();

        BO.OrderItem orderItemToAdd = new BO.OrderItem()
        {
            IdOrderItem = getOI._productId,
            Id = id,
            Name = getP._productName,
            Price = getOI._pricePerUnit,
            AmountItems = getOI._quantity,
            TotalPriceItem = getOI._pricePerUnit * getOI._quantity
        };
        foreach (DO.Products p in productListFromDo) 
            if (p._productId == id && p._amountInStock > 0)
            {
                cart.Orders.ToList().Add(orderItemToAdd);
                cart.TotalPriceCart += p._price;
                return cart;
            }
        throw new BO.InvalidValueException("not valid id or amount in stock");

    }

    public BO.Cart UpdateProductInCart(BO.Cart cart, int id, int newAmount)
    {
        DO.Products getP = new DO.Products();
        DO.OrderItem orderItem = new DO.OrderItem();
        try
        {
            getP = Dal.product.Get(id);
        }
        catch (DO.Exceptions.RequestedItemNotFoundException ex)
        {
            throw new BO.NotFound("product not found", ex);
        }
        try
        {
            orderItem = Dal.orderItem.Get(id);
        }
        catch (DO.Exceptions.RequestedItemNotFoundException ex)
        {
            throw new BO.NotFound("order item not found", ex);
        }
       
        if(newAmount >orderItem._quantity )
        {
            foreach (BO.OrderItem item in cart.Orders)
                if (item.IdOrderItem == id)
                {
                    if (getP._amountInStock > 0)
                    {
                        int amountToAdd = newAmount - item.AmountItems;
                        item.AmountItems+= amountToAdd;
                        item.TotalPriceItem +=amountToAdd*item.Price;
                        cart.TotalPriceCart += amountToAdd * item.Price;
                    }
                    else
                        throw new BO.InvalidValueException("not valid amount in stock");
                }
        }
        if (newAmount < orderItem._quantity)
        {
            foreach (BO.OrderItem item in cart.Orders)
                if (item.IdOrderItem == id)
                {

                    int amountToReduce =item.AmountItems - newAmount;
                    item.AmountItems -= amountToReduce;
                    item.TotalPriceItem -= amountToReduce * item.Price;
                    cart.TotalPriceCart -= amountToReduce * item.Price;
                }
        }
        if(newAmount==0)
        {
            foreach (BO.OrderItem item in cart.Orders)
                if (item.IdOrderItem == id)
                {
                    cart.Orders.ToList().Remove(item);
                    double priceToReduce = item.AmountItems*item.Price;
                    cart.TotalPriceCart -= priceToReduce;
                }
        }
        return cart;
    }
    private bool IsValid(string email)
    {
        bool valid = true;
        try { MailAddress emailAddress = new MailAddress(email); }
        catch { valid = false; }
        return valid;
    }
    public void PlaceOrder(BO.Cart cart)
    {
        if (cart.CustomerName == "")
            throw new BO.InvalidValueException("not valid name");
        if (cart.Address == "")
            throw new BO.InvalidValueException("not valid address");
        if (cart.Email=="" || !IsValid(cart.Email))
            throw new BO.InvalidValueException("not valid email");
        foreach (BO.OrderItem item in cart.Orders)
        {
            DO.Products getP=new DO.Products();
            try
            {
                getP = Dal.product.Get(item.Id);
            }
            catch (DO.Exceptions.RequestedItemNotFoundException ex)
            {
                throw new BO.NotFound("product not found", ex);
            }
            
            if (getP._amountInStock<item.AmountItems)
                throw new BO.InvalidValueException("not valid amont - Not enough in stock");
            if (item.AmountItems < 0)
                throw new BO.InvalidValueException("not valid amount");
        }
        DO.Orders newOrder = new DO.Orders()
        {
            _customerName = cart.CustomerName,
            _email = cart.Email,
            _address = cart.Address,
            _orderDate = DateTime.Now,
            _shippingDate =DateTime.MinValue,
            _deliveryDate=DateTime.MinValue
        };
        int orderId;
        try
        {
            orderId = Dal.order.Add(newOrder);
        }
        catch
        {
            throw new TheOperationFailed("couldnt add order");
        }
       
        foreach (BO.OrderItem item in cart.Orders)
        {
            DO.OrderItem newOrderItem = new DO.OrderItem()
            {
                _orderId= orderId,
                _productId=item.IdOrderItem,
                _pricePerUnit=item.Price,
                _quantity=item.AmountItems
            };
            try
            {
                Dal.orderItem.Add(newOrderItem);
            }
            catch
            {
                throw new TheOperationFailed("couldnt add order item");
            }

        }
        foreach (BO.OrderItem item in cart.Orders)
        {
            DO.Products product = new DO.Products();
            product._amountInStock -= item.AmountItems;
            try
            {
                Dal.product.Update(product);
            }
            catch
            {
                throw new TheOperationFailed("couldnt update details");
            }
           
        }
    }
}
