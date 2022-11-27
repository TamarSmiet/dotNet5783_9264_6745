﻿
using DO;


namespace Dal;
public static class DataSource
{

    internal static List<Products> productsList = new();
    internal static List<Orders> ordersList = new ();
    internal static List<OrderItem> orderItemsList = new();
    internal readonly static Random num = new Random();
    public static void startDataSource()
    {
        return;
    }
    internal static class Config
    {

      
        internal static int idOrder = 1;
        internal static int IdOrder { get {return idOrder++; } }
        internal static int idProduct = 100000;
        internal static int IdProduct { get { return idProduct++; } }

        internal static int idOrderItem = 1;
        internal static int IdOrderItem { get { return idOrderItem++; } }

        internal static int idOrderOfIdOrderItem = 1;
        internal static int IdOrderOfIdOrderItem { get { return idOrderOfIdOrderItem++; } }
        internal static int idProductOfIdOrderItem = 1;
        internal static int IdProductOfIdOrderItem { get { return idProductOfIdOrderItem++; } }

        //האינדקס פו הוא בשביל מספר רץ לבדוק מה עושים עם זה
        //public static int IdOrderOfIdOrderItem { get { return num.Next(indexOrders); } }

        //public static int IdProductOfIdOrderItem { get { return num.Next(indexProducts); } }

    }
    static DataSource()
    {
        s_Initialize();
    }
    private static void addOrder(Orders order)
    {
        ordersList.Add(order);
        //ordersArr[Config.indexOrders++] = order;
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
        addOrder(new DO.Orders(){ _orderId = Config.IdOrder,_customerName = newCustomerName, _email = newCustomerEmail, _address = newCustomerAdress, _orderDate = NewOrderDate, _shippingDate=newShipDate, _deliveryDate=newDeliveryDate });

    }

    private static void addProduct(Products product)
    {
        productsList.Add(product);  
        //productsArr[Config.indexProducts++] = product;
    }

    private static void addOrderItem(OrderItem orderItem)
    {
        orderItemsList.Add(orderItem);
        //orderItemsArr[Config.indexOrderItems++] = orderItem;
    }

    private static void s_Initialize()
    {
        addProduct(new DO.Products() { _productId = Config.IdProduct, _productName = "Oven", _price = 2000, _category = Enums.eCategory.kitchen, _amountInStock = 70 }); ;
        addProduct(new DO.Products() { _productId =  Config.IdProduct, _productName = "Stove", _price = 500, _category = Enums.eCategory.kitchen, _amountInStock = 100 });
        addProduct(new DO.Products() { _productId =  Config.IdProduct, _productName = "Refrigirator", _price = 3000, _category = Enums.eCategory.kitchen, _amountInStock = 50 });
        addProduct(new DO.Products() { _productId =  Config.IdProduct, _productName = "Vacuum Cleaner", _price = 500, _category = Enums.eCategory.house, _amountInStock = 60 });
        addProduct(new DO.Products() { _productId =  Config.IdProduct, _productName = "Kettle", _price = 150, _category = Enums.eCategory.house, _amountInStock = 150 });

        addProduct(new DO.Products() { _productId =  Config.IdProduct, _productName = "Oven", _price = 2000, _category = Enums.eCategory.kitchen, _amountInStock = 70 });
        addProduct(new DO.Products() { _productId =  Config.IdProduct, _productName = "Stove", _price = 500, _category = Enums.eCategory.kitchen, _amountInStock = 100 });
        addProduct(new DO.Products() { _productId =  Config.IdProduct, _productName = "Refrigirator", _price = 3000, _category = Enums.eCategory.kitchen, _amountInStock = 50 });
        addProduct(new DO.Products() { _productId =  Config.IdProduct, _productName = "Vacuum Cleaner", _price = 500, _category = Enums.eCategory.house, _amountInStock = 60 });
        addProduct(new DO.Products() { _productId =  Config.IdProduct, _productName = "Kettle", _price = 150, _category = Enums.eCategory.house, _amountInStock = 150 });

        addOrder("Esther Bat Zion Levinson",  "ebzlevinson@gmail.com", "Haroe 20/2");
        addOrder("Tamar Smietanski",  "tamarsmiet@gmail.com",  "Haroe 20/2");
        addOrder("Israel Israeli", "israel@gmail.com", "buksboim 12");
        addOrder("Avraham Cohen", "avic@gmail.com", "tlalim 21");


        addOrderItem(new DO.OrderItem() { _id = Config.IdOrderItem, _orderId = Config.IdOrderOfIdOrderItem, _productId = 100001, _pricePerUnit = 2000, _quantity = 1 });
        addOrderItem(new DO.OrderItem() { _id = Config.IdOrderItem, _orderId = Config.IdOrderOfIdOrderItem, _productId = 100002, _pricePerUnit = 500, _quantity = 1 });
        addOrderItem(new DO.OrderItem() { _id = Config.IdOrderItem, _orderId = Config.IdOrderOfIdOrderItem, _productId = 100003, _pricePerUnit = 3000, _quantity = 2 });



    }

}

