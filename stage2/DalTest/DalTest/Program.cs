// See https://aka.ms/new-console-template for more information
using DO;
using System.Linq.Expressions;
using Dal;
using System.Net.Http.Headers;

namespace DalTest
{
    public class Programm
    {
        private Products product = new Products();
        private Orders order = new Orders();
        private OrderItem orderItem = new OrderItem();

        void main()
        {
            Console.WriteLine("Enter a number 1-3 or 0 to exit:");
            int choice = Console.Read();
            switch (choice)
            {
                case 0:
                {
                    break;  
                }  
                case 1://product
                {
                   Console.WriteLine("Enter your choice:");
                   int choiceForProduct=Console.Read();
                   DalProduct p = new DalProduct();
                    switch (choiceForProduct)
                    {
                        case 1://add
                        {
                            Console.WriteLine("Enter product details:");
                            product._productId=Console.Read();
                            product._productName = Console.ReadLine();
                            product._price=Console.Read();
                            //product._category = Console.Read();
                            product._amountInStock=Console.Read();
                            p.addProduct(product);
                            break;
                        }
                        case 2:
                        {
                            Console.WriteLine("Enter an Id of product:");
                            Console.WriteLine(p.getproduct(Console.Read()));
                            break;
                        }
                        case 3:
                            break;
                        case 4://delete
                            Console.WriteLine("Enter an Id of product:");
                            p.deleteProduct(Console.Read());
                            break;
                        case 5://update
                        {
                                Console.WriteLine("Enter an Id of product:");
                                product = p.getproduct(Console.Read());
                                Console.WriteLine("the product to update is" + product);
                                Console.WriteLine("Enter the new details of the product:");
                                if(Console.ReadLine()!="")
                                {
                                    product._productName = Console.ReadLine();
                                    product._price = Console.Read();    
                                    product._amountInStock= Console.Read();
                                    p.updateProduct(product);
                                }
                                break;
                        }

                    }
                    break;
                }
                case 2://order
                    Console.WriteLine("Enter your choice:");
                    int choiceForOrder = Console.Read();
                    DalOrders o = new Dal.DalOrders();
                    switch (choiceForOrder)
                    {
                        case 1://add
                            Console.WriteLine("Enter order details:");
                            order._orderId = Console.Read();
                            order._customerName=Console.ReadLine();
                            order._email=Console.ReadLine();
                            order._address=Console.ReadLine();
                            //לבדוק למה זה עושה שגיאה...
                            //DateTime.TryParse(Console.ReadLine(), out date);
                            //order._orderDate=date;
                            //DateTime.TryParse(Console.ReadLine(), out date);
                            //order._shippingDate=date;
                            //DateTime.TryParse(Console.ReadLine(), out date);
                            //order._deliveryDate=date;
                            o.addOrder(order);
                            break;

                         case 2:
                            Console.WriteLine("Enter an Id of Order:");
                            Console.WriteLine(o.getOrder(Console.Read()));
                            break;

                        case 3:

                            break;

                        case 4://delete
                            Console.WriteLine("Enter an Id of order:");
                            o.deleteOrder(Console.Read());
                            break;

                        case 5://update
                            Console.WriteLine("Enter an Id of order:");
                            order = o.getOrder(Console.Read());
                            Console.WriteLine("the order to update is" + order);
                            Console.WriteLine("Enter the new details of the order:");
                            if (Console.ReadLine() != "")
                            {
                                order._customerName = Console.ReadLine();
                                order._email = Console.ReadLine();
                                order._address = Console.ReadLine();
                                //לבדוק למה זה עושה שגיאה...
                                //DateTime.TryParse(Console.ReadLine(), out date);
                                //order._orderDate=date;
                                //DateTime.TryParse(Console.ReadLine(), out date);
                                //order._shippingDate=date;
                                //DateTime.TryParse(Console.ReadLine(), out date);
                                //order._deliveryDate=date;
                                o.updateOrder(order);
                            }
                            break;

                    }
                    break;

                case 3://order item
                    Console.WriteLine("Enter order details:");
                    int choiceOrderItem = Console.Read();
                    DalOrderItem OI = new DalOrderItem();
                    switch (choiceOrderItem)
                    {
                        case 1://add
                            Console.WriteLine("Enter order item details:");
                            orderItem._id=Console.Read();
                            orderItem._orderId=Console.Read();
                            orderItem._productId=Console.Read();
                            orderItem._pricePerUnit=Console.Read();
                            orderItem._quantity=Console.Read();
                            OI.addOrderItem(orderItem);
                            break;

                        case 2:
                            Console.WriteLine("Enter an Id of an order item:");
                            Console.WriteLine(OI.getOrderItem(Console.Read()));
                            break;

                        case 3:
                           
                            break;

                        case 4://delete
                            Console.WriteLine("Enter an Id of an order item:");
                            OI.deleteOrderItem(Console.Read());
                            break;

                        case 5://update
                            Console.WriteLine("Enter an Id of an order item to update:");
                            orderItem = OI.getOrderItem(Console.Read());
                            Console.WriteLine("the order item to update is" + orderItem);
                            Console.WriteLine("Enter the new details of the order item:");
                            if (Console.ReadLine() != "")
                            {
                                orderItem._orderId = Console.Read();
                                orderItem._productId = Console.Read();
                                orderItem._pricePerUnit = Console.Read();
                                orderItem._quantity = Console.Read();
                                OI.updateOrderItem(orderItem);
                            }
                            break;

                        case 6:


                            break;

                        case 7:

                            break;
        
                    }
                    break;

            }
        }
    }
}
