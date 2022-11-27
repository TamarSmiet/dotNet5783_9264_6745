using Dal;
using DalApi;
using BlImplementation;
using BlApi;
using System.Runtime.CompilerServices;
using BO;
using DO;
using Enums = BO.Enums;
using System.Net.NetworkInformation;

namespace BlTest
{
    public class Program
    {
        private static readonly IBl blVariable = new Bl();
        private static IBl blVariableFromMethod = new Bl();
        static Cart cart = new Cart() { Orders = new List<BO.OrderItem>() };//Creating an instance of a cart
        static private Product product = new Product();
        static private Order order = new Order();
        static private Order orderFromMethod = new Order();
        static private OrderTracking orderTrackingFromMethod = new OrderTracking();
        //static private Cart cart = new Cart();
        public static void Main()
        {
            int choice;
            Console.WriteLine("Enter 1 for product, 2 - cart, 3 - order, 0 to exit:");
            int.TryParse(Console.ReadLine(), out choice);


            while (choice != 0)
            {

                switch (choice)
                {
                    case 1://product
                        productMethod();
                        break;

                    case 2://cart
                        cartMethod();
                        break;

                    case 3://order
                        orderMethod();
                        break;
                }
            }
        }

        static void productMethod()
        {
            int choiceForProduct;
            int parse;
            Console.WriteLine("Enter 1 to get all products " +
                "2 - to get a product by id" +
                "3 - to add a product" +
                "4 - to remove a product" +
                "5 - to update a product " );
            double parse2;
            int.TryParse(Console.ReadLine(), out parse);
            choiceForProduct = parse;



            switch (choiceForProduct)
            {
                    
                case 1://get all products
                    IEnumerable<ProductForList> listFromMethod= blVariable.Product.GetProducts();
                    foreach (ProductForList productForList in listFromMethod)
                        Console.WriteLine(productForList);
                    break;

                case 2://get single product by id
                    {
                        Console.WriteLine("Enter an Id of product:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product.Id = parse;

                        try
                        {
                            Console.WriteLine(blVariable.Product.GetProduct(product.Id));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }

                case 3://add
                    {
                        Console.WriteLine("Enter product details:");
                        Console.WriteLine("Name:");
                        product.Name = Console.ReadLine();
                        Console.WriteLine("Price:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product.Price = parse;
                        Console.WriteLine("type 0 for category kitchen, 1 - house, 2 - cellular, 3 - hair");
                        int c = int.Parse(Console.ReadLine());
                   
                        switch (c)
                        {
                            case 0:
                                product.Category = Enums.eCategory.cellular;
                                break;
                            case 1:
                                product.Category = Enums.eCategory.house;
                                break;
                            case 2:
                                product.Category = Enums.eCategory.cellular;
                                break;
                            case 3:
                                product.Category = Enums.eCategory.hair;
                                break;

                        }
                        Console.WriteLine("Amount in stock:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product.AmountInStock = parse;
                        try
                        {
                            blVariable.Product.AddProduct(product);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }

                case 4://delete
                    {
                        Console.WriteLine("Enter an Id of product:");
                        int id = int.Parse(Console.ReadLine());
                        try
                        {
                            blVariable.Product.RemoveProduct(id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }

                case 5://update product
                    {

                    
                        Console.WriteLine("Enter an Id of product:");
                        int Id = int.Parse(Console.ReadLine());
                        try
                        {
                            product = blVariable.Product.GetProduct(Id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        Console.WriteLine("the product to update is" + product);

                        Console.WriteLine("Enter the new details of the product:");

                        Console.WriteLine("name:");
                        product.Name = Console.ReadLine();
                        Console.WriteLine("type 0 for category kitchen, 1 - house, 2 - cellular, 3 - hair");
                        int category = int.Parse(Console.ReadLine());
                        switch (category)
                        {
                            case 0:
                                product.Category = Enums.eCategory.kitchen;
                                break;
                            case 1:
                                product.Category = Enums.eCategory.house;
                                break;
                            case 2:
                                product.Category = Enums.eCategory.cellular;
                                break;
                            case 3:
                                product.Category = Enums.eCategory.hair;
                                break;

                        }
                        Console.WriteLine("price:");
                        double.TryParse(Console.ReadLine(), out parse2);
                        product.Price = parse2;
                        Console.WriteLine("amount:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product.AmountInStock = parse;

                        blVariable.Product.UpdateProduct(product);

                        break;
                    }

                case 6:
                    {

                        break;
                    }
                   



            }
        }

        static void cartMethod()
        {
            Cart cart = new Cart();
            Product product = new Product();
            int choiceForCart;
            int parse;
            Console.WriteLine("Enter 1 to add product to the cart " +
               "2 - to update amount of product in the cart" +
               "3 - to place the order ");
            int.TryParse(Console.ReadLine(), out parse);
            choiceForCart = parse;
            switch (choiceForCart)
            {
                case 1://add product to cart
                    {

                        Console.WriteLine("enter product ID");
                        int id = int.Parse(Console.ReadLine());

                        Cart cartTmp = blVariable.Cart.AddProductToCart(cart, id);
                        Console.WriteLine(cartTmp);
                        
                        break;
                    }
                case 2://update amount of item in cart
                    {
                        Console.WriteLine("enter the product id");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter the new amount");
                        int newAmount = int.Parse(Console.ReadLine());
                        
                        Cart cartTmp = blVariable.Cart.UpdateProductInCart(cart,id , newAmount);
                        Console.WriteLine("the update cart is: ");
                        Console.WriteLine(cartTmp);
                        
                        
                        break;
                    }
                case 3://place order
                    {

                        Console.WriteLine("enter customer name");
                        cart.CustomerName = Console.ReadLine();
                        Console.WriteLine("enter customer address");
                        cart.Address = Console.ReadLine();
                        Console.WriteLine("enter customer email");
                        cart.Email = Console.ReadLine();
                       
                        blVariable.Cart.PlaceOrder(cart);
                        cart = new Cart { Orders = new List<BO.OrderItem>() };
                        

                        break;
                    }

            }

        }

        static void orderMethod()
        {
            int choiceForOrder;
            int parse;
            Console.WriteLine("Enter 1 to get all orders " +
                "2 - to get an order by id" +
                "3 - to update the shipping date of an order" +
                "4 - to update the delivery date of an order" +
                "5 - to track an order " +
                "or 0 to exit:");
           
            int.TryParse(Console.ReadLine(), out parse);
            choiceForOrder = parse;
            switch (choiceForOrder)
            {
                case 1://get all orders
                    {
                        IEnumerable<OrderForList> listFromMethod = blVariable.Order.GetOrders();
                        foreach (OrderForList orderForList in listFromMethod)
                            Console.WriteLine(orderForList);
                        break;
                    }
                case 2://get a single order by id
                    {
                        Console.WriteLine("Enter an Id of order:");
                        int.TryParse(Console.ReadLine(), out parse);
                        order.Id = parse;

                        try
                        {
                            IEnumerable<BO.OrderItem> listFromMethod = blVariable.Order.GetOrder(order.Id).ItemList;
                            Console.WriteLine(blVariable.Order.GetOrder(order.Id));
                            foreach (BO.OrderItem item in listFromMethod)
                                Console.WriteLine(item);
                           
                           

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }
                case 3://update shipping order
                    {
                        Console.WriteLine("Enter an Id of order:");
                        int.TryParse(Console.ReadLine(), out parse);
                        order.Id = parse;
                        try
                        {
                            orderFromMethod = blVariable.Order.UpdateShippingOrder(parse);
                            Console.WriteLine($@"you update the shipping date to {orderFromMethod.ExpeditionDate}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break; 
                    }
                case 4:// update delivery order
                    {

                        Console.WriteLine("Enter an Id of order:");
                        int.TryParse(Console.ReadLine(), out parse);
                        order.Id = parse;
                        try
                        {
                            orderFromMethod=blVariable.Order.UpdateDeliveryOrder(parse);
                            Console.WriteLine($@"you update the delivery date to {orderFromMethod.DeliveryDate}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }
                case 5://tracking order
                    {
                        
                        Console.WriteLine("Enter an Id of order:");
                        int.TryParse(Console.ReadLine(), out parse);
                        order.Id = parse;
                        try
                        {
                            
                            orderTrackingFromMethod = blVariable.Order.trackingOrder(parse);
                            List<DescriptionStatusDate> listFromMethod = orderTrackingFromMethod.DescriptionStatus;
                            foreach (DescriptionStatusDate item in listFromMethod)
                            {
                                Console.WriteLine(item) ;
                               
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

        
   
}