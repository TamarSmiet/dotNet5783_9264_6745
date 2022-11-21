// See https://aka.ms/new-console-template for more information
using DO;
using System.Linq.Expressions;
using Dal;

using System.Collections.Generic;
using System.Net.Http.Headers;
using DalApi;

namespace DalTest
{
    public class Programm
    {
        static private Products product = new Products();
        static private Orders order = new Orders();
        static private OrderItem orderItem = new OrderItem();
        IDal IDalVariable = new DalList();
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
                    case 1://product
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
            //DalProduct p = new DalProduct();
            //Products p2=new Products();
            IDal IDalVariable = new DalList();
            int parse;
            double parse2;
            int.TryParse(Console.ReadLine(), out parse);
            choiceForProduct = parse;



            switch (choiceForProduct)
            {

                case 1://add
                    {
                        
                        Console.WriteLine("Enter product details:");
                        Console.WriteLine("Id:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product._productId = parse;
                        Console.WriteLine("Name:");
                        product._productName = Console.ReadLine();
                        Console.WriteLine("Price:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product._price = parse;
                        Console.WriteLine("type 0 for category kitchen, 1 - house, 2 - cellular, 3 - hair");
                        int category = int.Parse(Console.ReadLine());
                        switch (category)
                        {
                            case 0:
                                product._category = Enums.eCategory.kitchen;
                                break;
                            case 1:
                                product._category = Enums.eCategory.house;
                                break;
                            case 2:
                                product._category = Enums.eCategory.cellular;
                                break;
                            case 3:
                                product._category = Enums.eCategory.hair;
                                break;

                        }
                        Console.WriteLine("Amount in stock:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product._amountInStock = parse;
                        try
                        {
                            IDalVariable.product.Add(product);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;

                        
                    }
                case 2://display
                    {
                        Console.WriteLine("Enter an Id of product:");
                        //צריך פו את זה 
                        int.TryParse(Console.ReadLine(), out parse);
                        product._productId = parse;

                        try
                        {
                            Console.WriteLine(IDalVariable.product.Get(product._productId));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }
                case 3:
                    foreach (IEnumerable<Products> myProduct in IDalVariable.product.GetAll())
                    {
                        Console.WriteLine(myProduct);
                    }
                    break;
                case 4://delete
                    Console.WriteLine("Enter an Id of product:");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        IDalVariable.product.Delete(id);
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
                        try
                        {
                            product = IDalVariable.product.Get(Id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        Console.WriteLine("the product to update is" + product);
                        Console.WriteLine("Enter the new details of the product:");
                       
                        Console.WriteLine("name:");
                        product._productName = Console.ReadLine();
                        Console.WriteLine("type 0 for category kitchen, 1 - house, 2 - cellular, 3 - hair");
                        int category = int.Parse(Console.ReadLine());
                        switch (category)
                        {
                            case 0:
                                product._category = Enums.eCategory.kitchen;
                                break;
                            case 1:
                                product._category = Enums.eCategory.house;
                                break;
                            case 2:
                                product._category = Enums.eCategory.cellular;
                                break;
                            case 3:
                                product._category = Enums.eCategory.hair;
                                break;

                        }
                        Console.WriteLine("price:");
                        double.TryParse(Console.ReadLine(), out parse2);
                        product._price= parse2;
                        Console.WriteLine("amount:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product._amountInStock = parse;

                        IDalVariable.product.Update(product);
                   
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

            IDal IDalVariable = new DalList();
            //DalOrders o = new Dal.DalOrders();
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
                    //DateTime.TryParse(Console.ReadLine(), out date);
                    //order._orderDate = date;
                    //DateTime.TryParse(Console.ReadLine(), out date);
                    //order._shippingDate = date;
                    //DateTime.TryParse(Console.ReadLine(), out date);
                    //order._deliveryDate = date;
                    IDalVariable.order.Add(order);
                    break;

                case 2://display
                    Console.WriteLine("Enter an Id of Order:");
                    int Id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(IDalVariable.order.Get(Id));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;

                case 3:
                    foreach (IEnumerable<Orders> myOrder in IDalVariable.order.GetAll())
                    {
                        Console.WriteLine(myOrder);
                    }
                    break;

                case 4://delete
                    Console.WriteLine("Enter an Id of order:");
                    int IdToDelete = int.Parse(Console.ReadLine());
                    try
                    {
                        IDalVariable.order.Delete(IdToDelete);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;

                case 5://update
                    Console.WriteLine("Enter an Id of order:");
                    int idToUpdate = int.Parse(Console.ReadLine());
                    try
                    {
                        order = IDalVariable.order.Get(idToUpdate);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    Console.WriteLine("the order to update is" + order);
                    Console.WriteLine("Enter the new details of the order:");
                    Console.WriteLine("customer name:");
                    order._customerName = Console.ReadLine();
                    Console.WriteLine("email:");
                    order._email = Console.ReadLine();
                    Console.WriteLine("address:");
                    order._address = Console.ReadLine();
                    DateTime.TryParse(Console.ReadLine(), out date);
                    order._orderDate = date;
                    DateTime.TryParse(Console.ReadLine(), out date);
                    order._shippingDate = date;
                    DateTime.TryParse(Console.ReadLine(), out date);
                    order._deliveryDate = date;
                    IDalVariable.order.Update(order);

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
            IDal IDalVariable = new DalList();
           // DalOrderItem OI = new DalOrderItem();
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
                    IDalVariable.orderItem.Add(orderItem);
                    break;

                case 2://display
                    Console.WriteLine("Enter an Id of an order item:");
                    int Id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(IDalVariable.orderItem.Get(Id));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;

                case 3:
                    foreach (OrderItem myOrderItem in IDalVariable.orderItem.GetAll())
                    {
                        Console.WriteLine(myOrderItem);
                    }
                    break;

                case 4://delete
                    Console.WriteLine("Enter an Id of an order item:");
                    int IdToDelete = int.Parse(Console.ReadLine());
                    try
                    {
                        IDalVariable.orderItem.Delete(IdToDelete);
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
                        orderItem = IDalVariable.orderItem.Get(idToUpdate);
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
                    IDalVariable.orderItem.Update(orderItem);
                    break;

                case 6:
                    Console.WriteLine("Enter Id of an order and a product:");
                    int OrderId = int.Parse(Console.ReadLine());
                    int ProductId = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(IDalVariable.orderItem.getOrderItemByOandP(OrderId, ProductId));
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
                        foreach (OrderItem myOrderItem in IDalVariable.orderItem.getOrderItemByOrder(IdOrder))
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

