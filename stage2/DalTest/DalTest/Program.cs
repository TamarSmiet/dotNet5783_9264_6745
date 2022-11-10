// See https://aka.ms/new-console-template for more information
using DO;
using System.Linq.Expressions;
using Dal;

using System.Net.Http.Headers;

namespace DalTest
{
    public class Programm
    {
        static private Products product = new Products();
        static private Orders order = new Orders();
        static private OrderItem orderItem = new OrderItem();

        public static void Main()
        {
            DataSource.startDataSource();
            int choice;
            Console.WriteLine("Enter a number 1-3 or 0 to exit:");
            int.TryParse(Console.ReadLine(), out choice);


            while (choice != 0)
            {

                switch (choice)
                {
                    //    DateTime.TryParse(Console.ReadLine(), out date);
                    //order._shippingDate = date;
                    case 1://product
                           // Console.WriteLine("Enter your choice:");
                        productMethod();
                        break;

                    case 2://order
                        orderMethod();
                        break;

                    case 3://order item
                        orderItemMethod();
                        break;
                }
            }
        }

        static void productMethod()
        {

            Console.WriteLine("Enter your choice :");
            int choiceForProduct;
            DalProduct p = new DalProduct();
            int parse;
            double parse2;
            int.TryParse(Console.ReadLine(), out parse);
            choiceForProduct = parse;



            switch (choiceForProduct)
            {

                case 1://add
                    {
                        Console.WriteLine("Enter product details:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product._productId = parse;
                        product._productName = Console.ReadLine();
                        int.TryParse(Console.ReadLine(), out parse);
                        product._price = parse;
                        //product._category = Console.Read();
                        int.TryParse(Console.ReadLine(), out parse);
                        product._amountInStock = parse;
                        try
                        {
                            p.addProduct(product);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Enter an Id of product:");
                        //צריך פו את זה 
                        int.TryParse(Console.ReadLine(), out parse);
                        product._productId = parse;

                        try
                        {
                            Console.WriteLine(p.getproduct(product._productId));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }
                case 3:
                    foreach (Products myProduct in p.getAllProducts())
                    {
                        Console.WriteLine(myProduct);//אולי צריך את toString??
                    }
                    break;
                case 4://delete
                    Console.WriteLine("Enter an Id of product:");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        p.deleteProduct(id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
                case 5://update
                    {

                        Console.WriteLine("Enter an Id of product:");
                        int Id = int.Parse(Console.ReadLine());
                        string name;
                        double price;
                        int amountInStock;

                        try
                        {
                            product = p.getproduct(Id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        Console.WriteLine("the product to update is" + product);
                        Console.WriteLine("Enter the new details of the product:");
                        //if (Console.ReadLine() != "")
                        //{
                        Console.WriteLine("name:");
                        name = Console.ReadLine();
                        Console.WriteLine("price:");
                        double.TryParse(Console.ReadLine(), out parse2);
                        price = parse2;
                        Console.WriteLine("amount");
                        int.TryParse(Console.ReadLine(), out parse);
                        amountInStock = parse;

                        p.updateProduct(Id, name, price, amountInStock);
                        //}
                        break;
                    }


            }

        }

        static void orderMethod()
        {
            int parse;
            Console.WriteLine("Enter your choice:");
            int choiceForOrder;
            int.TryParse(Console.ReadLine(), out parse);
            choiceForOrder = parse;

            DalOrders o = new Dal.DalOrders();
            switch (choiceForOrder)
            {
                case 1://add
                    DateTime date;
                    Console.WriteLine("Enter order details:");
                    int.TryParse(Console.ReadLine(), out parse);
                    order._orderId = parse;
                    order._customerName = Console.ReadLine();
                    order._email = Console.ReadLine();
                    order._address = Console.ReadLine();
                    DateTime.TryParse(Console.ReadLine(), out date);
                    order._orderDate = date;
                    DateTime.TryParse(Console.ReadLine(), out date);
                    order._shippingDate = date;
                    DateTime.TryParse(Console.ReadLine(), out date);
                    order._deliveryDate = date;
                    o.addOrder(order);
                    break;

                case 2:
                    Console.WriteLine("Enter an Id of Order:");
                    int Id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(o.getOrder(Id));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;

                case 3:
                    foreach (Orders myOrder in o.getOrdersArr())
                    {
                        Console.WriteLine(myOrder);
                    }
                    break;

                case 4://delete
                    Console.WriteLine("Enter an Id of order:");
                    int IdToDelete = int.Parse(Console.ReadLine());
                    try
                    {
                        o.deleteOrder(IdToDelete);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;

                case 5://update
                    Console.WriteLine("Enter an Id of order:");
                    int idToUpdate = int.Parse(Console.ReadLine());
                    string name;
                    string email;
                    string address;
                    DateTime orderDate, shippingDate, deliveryDate;
                    try
                    {
                        order = o.getOrder(idToUpdate);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    Console.WriteLine("the order to update is" + order);
                    Console.WriteLine("Enter the new details of the order:");
                    Console.WriteLine("customer name:");
                    name = Console.ReadLine();
                    Console.WriteLine("email:");
                    email = Console.ReadLine();
                    Console.WriteLine("address:");
                    address = Console.ReadLine();
                    DateTime.TryParse(Console.ReadLine(), out date);
                    orderDate = date;
                    DateTime.TryParse(Console.ReadLine(), out date);
                    shippingDate = date;
                    DateTime.TryParse(Console.ReadLine(), out date);
                    deliveryDate = date;
                    o.updateOrder(idToUpdate, name, email, address, orderDate, shippingDate, deliveryDate);

                    break;
            }

        }
    

        static void orderItemMethod()
        {
            Console.WriteLine("Enter your choice:");
            int parse;
            float parse3;
            int choiceOrderItem;
            int.TryParse(Console.ReadLine(), out parse);
            choiceOrderItem = parse;
            DalOrderItem OI = new DalOrderItem();
            switch (choiceOrderItem)
            {
                case 1://add
                    Console.WriteLine("Enter order item details:");
                    int.TryParse(Console.ReadLine(), out parse);
                    orderItem._id = parse;
                    int.TryParse(Console.ReadLine(), out parse);
                    orderItem._orderId = parse;
                    int.TryParse(Console.ReadLine(), out parse);
                    orderItem._productId = parse;
                    float.TryParse(Console.ReadLine(), out parse3);
                    orderItem._pricePerUnit = parse3;
                    int.TryParse(Console.ReadLine(), out parse);
                    orderItem._quantity = parse;
                    OI.addOrderItem(orderItem);
                    break;

                case 2:
                    Console.WriteLine("Enter an Id of an order item:");
                    int Id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(OI.getOrderItem(Id));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;

                case 3:
                    foreach (OrderItem myOrderItem in OI.getOrderItemsArr())
                    {
                        Console.WriteLine(myOrderItem);//אולי צריך את toString??
                    }
                    break;

                case 4://delete
                    Console.WriteLine("Enter an Id of an order item:");
                    int IdToDelete = int.Parse(Console.ReadLine());
                    try
                    {
                        OI.deleteOrderItem(IdToDelete);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;

                case 5://update
                    Console.WriteLine("Enter an Id of an order item to update:");
                    int idToUpdate = int.Parse(Console.ReadLine());
                    try
                    {
                        orderItem = OI.getOrderItem(idToUpdate);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    Console.WriteLine("the order item to update is" + orderItem);
                    Console.WriteLine("Enter the new details of the order item:");
                    int orderId = int.Parse(Console.ReadLine());
                    orderItem._orderId = orderId;
                    int productId = int.Parse(Console.ReadLine());
                    orderItem._productId = productId;
                    int pricePerUnit = int.Parse(Console.ReadLine());
                    orderItem._pricePerUnit = pricePerUnit;
                    int quantity = int.Parse(Console.ReadLine());
                    orderItem._quantity = quantity;
                    OI.updateOrderItem(orderItem);
                    break;

                case 6:
                    Console.WriteLine("Enter Id of an order and a product:");
                    int OrderId = int.Parse(Console.ReadLine());
                    int ProductId = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(OI.getOrderItemByOandP(OrderId, ProductId));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                    }
                    break;

                case 7:
                    Console.WriteLine("Enter Id of an order:");
                    int IdOrder = int.Parse(Console.ReadLine());
                    try
                    {
                        foreach (OrderItem myOrderItem in OI.getOrderItemByOrder(IdOrder))
                        {
                            Console.WriteLine(myOrderItem);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;

            }
        }




    }
}

