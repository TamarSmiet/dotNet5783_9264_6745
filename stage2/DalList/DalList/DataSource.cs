
using DO;


namespace Dal;

internal static class DataSource
{
    internal static DO.Products[] productsArr = new DO.Products[50];
    internal static DO.Orders[] ordersArr = new DO.Orders[100];
    internal static DO.OrderItem[] orderItemsArr = new DO.OrderItem[200];
    internal readonly static Random num = new Random();

    internal static class Config
    {
        internal static int indexProducts { get; set; } = 0;
        internal static int indexOrders { get; set; } = 0;
        internal static int indexOrderItems { get; set; } = 0;

        internal static int idOrder = 1;
        internal static int IdOrder { get {return idOrder++; } }

        internal static int idOrderItem = 1;
        internal static int IdOrderItem { get { return idOrderItem++; } }

      

        //private static int idOrderOfIdOrderItem=num.Next(indexOrders);
        public static int IdOrderOfIdOrderItem { get { return num.Next(indexOrders); } }

        //private static int idProductOfIdOrderItem = num.Next(indexOrders);
        public static int IdProductOfIdOrderItem { get { return num.Next(indexProducts); } }




    }
    static DataSource()
    {
        s_Initialize();
    }
    private static void addOrder(dalOrders order)
    {
        ordersArr[Config.indexOrders++] = order;
    }

    private static void addProduct(Products product)
    {
        productsArr[Config.indexProducts++] = product;
    }

    private static void addOrderItem(OrderItem orderItem)
    {
        orderItemsArr[Config.indexOrderItems++] = orderItem;
    }

    private static void s_Initialize()
    {
        addProduct(new DO.Products() { _productId = num.Next(100000, 999999), _productName = "Oven", _price = 2000, _category = Enums.eCategory.a, _amountInStock = 70 });
        addProduct(new DO.Products() { _productId = num.Next(100000, 999999), _productName = "Stove", _price = 500, _category = "Kitchen", _amountInStock = 100 });
        addProduct(new DO.Products() { _productId = num.Next(100000, 999999), _productName = "Refrigirator", _price = 3000, _category = "Kitchen", _amountInStock = 50 });
        addProduct(new DO.Products() { _productId = num.Next(100000, 999999), _productName = "Vacuum Cleaner", _price = 500, _category = "Home", _amountInStock = 60 });
        addProduct(new DO.Products() { _productId = num.Next(100000, 999999), _productName = "Kettle", _price = 150, _category = "Kitchen", _amountInStock = 150 });
      

        addOrder(new DO.Orders() { _orderId = Config.IdOrder, _customerName = "Esther Bat Zion Levinson", _email = "ebzlevinson@gmail.com", _adress = "Haroe 20/2", _orderDate = new DateTime(27 / 10 / 2022), _shippingDate = new DateTime(28 / 10 / 2022), _deliveryDate = new DateTime(30 / 10 / 2022) });
        addOrder(new DO.Orders() { _orderId = Config.IdOrder, _customerName = "Tamar Smietanski", _email = "tamarsmiet@gmail.com", _adress = "Haroe 20/2", _orderDate = new DateTime(28 / 10 / 2022), _shippingDate = new DateTime(29 / 10 / 2022), _deliveryDate = new DateTime(31 / 10 / 2022) });

        addOrderItem(new DO.OrderItem() { _id = Config.IdOrderItem, _orderId = Config.IdOrderOfIdOrderItem, _productId = Config.IdProductOfIdOrderItem, _PricePerUnit = 2000, _quantity = 1 });
        addOrderItem(new DO.OrderItem() { _id = Config.IdOrderItem, _orderId = Config.IdOrderOfIdOrderItem, _productId = Config.IdProductOfIdOrderItem, _PricePerUnit = 3000, _quantity = 1 });
        addOrderItem(new DO.OrderItem() { _id = Config.IdOrderItem, _orderId = Config.IdOrderOfIdOrderItem, _productId = Config.IdProductOfIdOrderItem, _PricePerUnit = 500, _quantity = 2 });



    }

}


