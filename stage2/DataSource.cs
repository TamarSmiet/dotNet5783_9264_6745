using DO;
using static DO.Enums;

namespace Dal;
internal static class DataSource
{
    static readonly internal Random _randNum = new Random();
    static internal Product[] _arrProduct = new Product[50];
    static internal Order[] _arrOrder = new Order[100];
    static internal OrderItem[] _arrOrderItem = new OrderItem[200];
    static DataSource()
    {
        s_initialize();
    }

    static private void addNewProduct(string newName, ECategory newCategory, double newPrice, int newInStock)
    {
        _arrProduct[Config._productIndex].ID = Config.CalNumOfProduct;
        _arrProduct[Config._productIndex].Name = newName;
        _arrProduct[Config._productIndex].Category = newCategory;
        _arrProduct[Config._productIndex].InStock = newInStock;
        Config._productIndex++;
    }
    static private void addNewOrder(string newCustomerName, string newCustomerEmail, string newCustomerAdress, DateTime NewOrderDate, DateTime newShipDate, DateTime newDeliveryDate)
    {
        _arrOrder[Config._orderIndex].ID = Config.CalNumOfIDOrder;
        _arrOrder[Config._orderIndex].CustomerName = newCustomerName;
        _arrOrder[Config._orderIndex].CustomerEmail = newCustomerEmail;
        _arrOrder[Config._orderIndex].CustomerAdress = newCustomerAdress;
        _arrOrder[Config._orderIndex].OrderDate = NewOrderDate;
        _arrOrder[Config._orderIndex].ShipDate = newShipDate;
        _arrOrder[Config._orderIndex].DeliveryDate = newDeliveryDate;
        Config._orderIndex++;
    }
    static private void addNewOrder(string newCustomerName, string newCustomerEmail, string newCustomerAdress)
    {
        DateTime _today = DateTime.Now;
        int daysAgo = new Random().Next(600);
        DateTime NewOrderDate = _today.AddDays(-daysAgo);
        int daysbetweenOrderToShip = new Random().Next(10);
        DateTime newShipDate = NewOrderDate.AddDays(daysbetweenOrderToShip);
        int daysbetweenDeliveryToShip = new Random().Next(7); 
        DateTime newDeliveryDate = newShipDate.AddDays(daysbetweenDeliveryToShip);  
        addNewOrder(newCustomerName, newCustomerEmail, newCustomerAdress,NewOrderDate,newShipDate,newDeliveryDate);


    }
    static private void addNewOrderItem(int newProductID, int newOrderID, double newPrice, int newAmount)
    {
        _arrOrderItem[Config._orderItemIndex].ProductID = newProductID;
        _arrOrderItem[Config._orderItemIndex].OrderID = newOrderID;
        _arrOrderItem[Config._orderItemIndex].Price = newPrice;
        _arrOrderItem[Config._orderItemIndex].Amount = newAmount;
        Config._productIndex++;
    }
    static internal class Config
    {
        static internal int _productIndex = 0;
        static internal int _orderIndex = 0;
        static internal int _orderItemIndex = 0;
        static private int _calNumOfProduct = 100000;
        static public int CalNumOfProduct { get { return _calNumOfProduct++; } }
        static private int _calNumOfIDOrder = 200000;
        static public int CalNumOfIDOrder { get { return _calNumOfIDOrder++; } }



    }
    static private void s_initialize()
    {
        #region addNewProduct
        //להוסיף לפונקציה שתקבל סטרינג
        addNewProduct("big_notebook", ECategory.Notebooks, 6.9, 50);     //10000 
        addNewProduct("small notebook", ECategory.Notebooks, 4.9, 0);       ///10001
        addNewProduct("campuse notebook", ECategory.Notebooks, 5.9, 35);      //10002
        addNewProduct("chanan notebook", ECategory.Notebooks, 4.9, 63);       //10003
        addNewProduct("pilot", ECategory.Pens, 9.9, 12);                    //10004
        addNewProduct("stabilo pen", ECategory.Pens, 5.6, 20);              //10005
        addNewProduct("chanan pen", ECategory.Pens, 4, 10);                 //1006
        addNewProduct("blue diary", ECategory.Diaries, 12.5, 38);           //10007         
        addNewProduct("red diary", ECategory.Diaries, 17, 65);               //10008
        addNewProduct("paintbrush", ECategory.ArtMaterials, 7, 25);          //10009
        addNewProduct("Sudoku", ECategory.Games, 9.5, 41);                  //10010
        #endregion
        #region addNewOrderItem
        //  addNewOrder()
        addNewOrderItem(100000, 200001, 6.9, 3);
        addNewOrderItem(100002, 200001, 5.9, 1);
        addNewOrderItem(100009, 200001, 7, 7);
        addNewOrderItem(100010, 200001, 9.5, 2);

        addNewOrderItem(100000, 200002, 6.9, 3);
        addNewOrderItem(100002, 200002, 5.9, 1);
        addNewOrderItem(100003, 200002, 4.9, 1);
        addNewOrderItem(100004, 200002, 9.9, 2);

        addNewOrderItem(100007, 200003, 6.9, 3);
        addNewOrderItem(100002, 200003, 5.9, 1);
        addNewOrderItem(100005, 200003, 5.6, 3);
        addNewOrderItem(100008, 200003, 17, 2);

        addNewOrderItem(100000, 200004, 6.9, 3);
        addNewOrderItem(100002, 200004, 5.9, 1);
        addNewOrderItem(100009, 200004, 7, 7);
        addNewOrderItem(100010, 200004, 9.5, 2);

        addNewOrderItem(100000, 200005, 6.9, 3);
        addNewOrderItem(100002, 200005, 5.9, 1);
        addNewOrderItem(100003, 200005, 4.9, 1);
        addNewOrderItem(100004, 200005, 9.9, 2);

        addNewOrderItem(100007, 200006, 6.9, 3);
        addNewOrderItem(100002, 200006, 5.9, 1);
        addNewOrderItem(100005, 200006, 5.6, 3);
        addNewOrderItem(100008, 200006, 17, 2);

        addNewOrderItem(100000, 200007, 6.9, 3);
        addNewOrderItem(100002, 200007, 5.9, 1);
        addNewOrderItem(100009, 200007, 7, 7);
        addNewOrderItem(100010, 200007, 9.5, 2);

        addNewOrderItem(100000, 200008, 6.9, 3);
        addNewOrderItem(100002, 200008, 5.9, 1);
        addNewOrderItem(100003, 200008, 4.9, 1);
        addNewOrderItem(100004, 200008, 9.9, 2);

        addNewOrderItem(100007, 200009, 6.9, 3);
        addNewOrderItem(100002, 200009, 5.9, 1);
        addNewOrderItem(100005, 200009, 5.6, 3);
        addNewOrderItem(100008, 200009, 17, 2);

        addNewOrderItem(100000, 200010, 6.9, 3);
        addNewOrderItem(100002, 200010, 5.9, 1);
        addNewOrderItem(100009, 200010, 7, 7);
        addNewOrderItem(100010, 200010, 9.5, 2);

        #endregion
        #region addNewOrder

    //    int rnd = new Random();
        addNewOrder("David Levi", "david@gmail.com", "buksboim 12");
        addNewOrder("David Levi", "david@gmail.com", "buksboim 12",DateTime.Now.AddDays(-5),DateTime.Now,DateTime.MinValue);
        //
        #endregion
        TimeSpan
    }
}

