// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static DO.Exceptions;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Net;
using DO;
using System.Xml.Linq;

namespace Dal;

public class Program
{
    static Program()
    {
        s_Initialize();
        initializeProductData();
        initializeOrderData();
        initializeOrderItemData();
    }
    //DO.Products product;
    //List<DO.Products> list;
    internal static List<Products?> productsList = new List<Products?>();
    internal static List<Orders?> ordersList = new List<Orders?>();
    internal static List<OrderItem?> orderItemsList = new List<OrderItem?>();
    internal readonly static Random num = new Random();
    private static void addOrder(Orders order)
    {
        ordersList.Add(order);
        XElement productRootElem = XMLTools.LoadListFromXMLElement(@"Order.xml");
        productRootElem.Add(order);
        XMLTools.SaveListToXMLElement(productRootElem, @"Order.xml");
    }
    private static int getOrderId()
    {
        XElement config = XMLTools.LoadListFromXMLElement(@"Config.xml");
        int id = (int)config.Element("idOrder");
        id++;
        config.Element("idOrder")!.SetValue(id);
        config.Save(@"Config.xml");
        return id;

    }

    static private void addOrder(string newCustomerName, string newCustomerEmail, string newCustomerAdress)
    {
        DateTime _today = DateTime.Now;
        int daysAgo = new Random().Next(600);
        DateTime NewOrderDate = _today.AddDays(-daysAgo);
        int daysbetweenOrderToShip = new Random().Next(10);
        DateTime newShipDate = NewOrderDate.AddDays(daysbetweenOrderToShip);
        int daysbetweenDeliveryToShip = new Random().Next(7);
        DateTime newDeliveryDate = newShipDate.AddDays(daysbetweenDeliveryToShip);
        
        addOrder(new DO.Orders() { _orderId = getOrderId(), _customerName = newCustomerName, _email = newCustomerEmail, _address = newCustomerAdress, _orderDate = NewOrderDate, _shippingDate = newShipDate, _deliveryDate = newDeliveryDate });
    }

    private static void addProduct(Products product)
    {
        productsList.Add(product);
        XElement productRootElem = XMLTools.LoadListFromXMLElement(@"Products.xml");
        productRootElem.Add(product);
        XMLTools.SaveListToXMLElement(productRootElem, @"Products.xml");

    }

    private static void addOrderItem(OrderItem orderItem)
    {
        orderItemsList.Add(orderItem);
        XElement productRootElem = XMLTools.LoadListFromXMLElement(@"OrderItem.xml");
        productRootElem.Add(orderItem);
        XMLTools.SaveListToXMLElement(productRootElem, @"OrderItem.xml");

    }
    private static void s_Initialize()
    {

        addProduct(new DO.Products() { _productId = 10000, _productName = "Oven", _price = 2000, _category = Enums.eCategory.kitchen, _amountInStock = 70 }); ;
        addProduct(new DO.Products() { _productId = 10001, _productName = "Stove", _price = 500, _category = Enums.eCategory.kitchen, _amountInStock = 100 });
        addProduct(new DO.Products() { _productId = 10002, _productName = "Refrigirator", _price = 3000, _category = Enums.eCategory.kitchen, _amountInStock = 50 });
        addProduct(new DO.Products() { _productId = 10003, _productName = "Vacuum Cleaner", _price = 500, _category = Enums.eCategory.house, _amountInStock = 60 });
        addProduct(new DO.Products() { _productId = 10004, _productName = "Kettle", _price = 150, _category = Enums.eCategory.house, _amountInStock = 150 });
        addProduct(new DO.Products() { _productId = 10005, _productName = "Phone Samsung", _price = 2000, _category = Enums.eCategory.cellular, _amountInStock = 70 });
        addProduct(new DO.Products() { _productId = 10006, _productName = "Hair Brush", _price = 350, _category = Enums.eCategory.hair, _amountInStock = 100 });
        addProduct(new DO.Products() { _productId = 10007, _productName = "Phone Nokia", _price = 1500, _category = Enums.eCategory.cellular, _amountInStock = 50 });
        addProduct(new DO.Products() { _productId = 10008, _productName = "Babyliss", _price = 200, _category = Enums.eCategory.hair, _amountInStock = 60 });
        addProduct(new DO.Products() { _productId = 10009, _productName = "Phone Apple", _price = 3500, _category = Enums.eCategory.cellular, _amountInStock = 150 });

        addOrder("Esther Bat Zion Levinson", "ebzlevinson@gmail.com", "Haroe 20/2");
        addOrder("Tamar Smietanski", "tamarsmiet@gmail.com", "Haroe 20/2");
        addOrder("Israel Israeli", "israel@gmail.com", "buksboim 12");
        addOrder("Avraham Cohen", "avic@gmail.com", "tlalim 21");
        addOrder(new DO.Orders() { _orderId = 1, _customerName = "Itschak", _email = "I@gmail.com", _address = "my address", _orderDate = DateTime.MinValue, _shippingDate = DateTime.MinValue, _deliveryDate = DateTime.MinValue });


        addOrderItem(new DO.OrderItem() { _id = 10, _orderId = 1, _productId = 100001, _pricePerUnit = 2000, _quantity = 1 });
        addOrderItem(new DO.OrderItem() { _id = 11, _orderId = 2, _productId = 100002, _pricePerUnit = 500, _quantity = 1 });
        addOrderItem(new DO.OrderItem() { _id = 12, _orderId = 3, _productId = 100003, _pricePerUnit = 3000, _quantity = 2 });



    }
  
      
    private static void initializeProductData()
    {
        FileStream fs = new FileStream(@"C:\Users\צרצוש\source\repos\ebzLevinson\dotNet5783_9264_6745\whithLinqs\xml\Products.xml", FileMode.OpenOrCreate);
        XmlSerializer xs = new XmlSerializer(typeof(List<DO.Products>));
        xs.Serialize(fs, productsList);
        fs.Close();
    }

    private static void initializeOrderData()
    {
        FileStream fs = new FileStream(@"C:\Users\צרצוש\source\repos\ebzLevinson\dotNet5783_9264_6745\whithLinqs\xml\Orders.xml", FileMode.OpenOrCreate);
        XmlSerializer xs = new XmlSerializer(typeof(List<DO.Orders>));
        xs.Serialize(fs, ordersList);
        fs.Close();
    }

    private static void initializeOrderItemData()
    {
        FileStream fs = new FileStream(@"C:\Users\צרצוש\source\repos\ebzLevinson\dotNet5783_9264_6745\whithLinqs\xml\OrderItem.xml", FileMode.OpenOrCreate);
        XmlSerializer xs = new XmlSerializer(typeof(List<DO.OrderItem>));
        xs.Serialize(fs, orderItemsList);
        fs.Close();
    }

}
