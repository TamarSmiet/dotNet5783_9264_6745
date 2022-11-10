
using DO;
using static Dal.DataSource;
namespace Dal
{
    public struct DalOrderItem
    {
        /// <summary>
        /// adding order item
        /// </summary>
        /// <param name="orderItem"></param>
        /// <exception cref="Exception">in case this order item allready exist </exception>
        public void addOrderItem(OrderItem orderItem)
        {
            for(int i=0;i<Config.indexOrderItems;i++)
            {
                if(orderItem._id== orderItemsArr[i]._id)
                {
                    throw new Exception ("order item allready exists");
                }
            }
            orderItemsArr[Config.indexOrderItems++] = orderItem;

        }

        /// <summary>
        /// update order item
        /// </summary>
        /// <param name="orderItem">get the new details to updatein an object orderItem</param>
        public void updateOrderItem(OrderItem orderItem)
        {
            for(int i=0;i!=Config.indexOrderItems;i++)
            {
                if(orderItem._id== orderItemsArr[i]._id)
                {
                    orderItemsArr[i] = orderItem;
                }
            }
        }

        /// <summary>
        /// delete order item of the array
        /// </summary>
        /// <param name="id"></param>
        public void deleteOrderItem(int id)
        {
            for (int i = 0; i < Config.indexOrderItems; i++)
            {
                if (id == orderItemsArr[i]._id)
                {
                    orderItemsArr[i] = orderItemsArr[Config.indexOrderItems - 1];//moves the last order item of the array to the place of the order item to delete
                    Config.indexOrderItems--;
                    break; 
                }
            }
        }

        /// <summary>
        /// get the details of an order item by its order id and product id
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <returns>return the object witch has there ids</returns>
        /// <exception cref="Exception">in case he didn't found</exception>
        public OrderItem getOrderItemByOandP(int orderId ,int productId)
        {
            for (int i = 0; i < Config.indexOrderItems; i++)
            {
                if ((orderId == orderItemsArr[i]._orderId) && (productId == orderItemsArr[i]._productId))
                {
                    return orderItemsArr[i];
                }
                
 
            }
            throw new Exception("order item with there ids does not exist!");
        }

        /// <summary>
        /// get a specific order item by its id
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns>return the object</returns>
        /// <exception cref="Exception">in case he didn't found this id in the array</exception>
        public OrderItem getOrderItem(int orderItemId)
        {
            for (int i = 0; i != Config.indexOrderItems; i++)
            {
                if (orderItemId == orderItemsArr[i]._orderId)
                {
                    return orderItemsArr[i];
                }

            }
           throw new Exception("order item with this id does not exist!");
        }


        /// <summary>
        /// get all the order items that are in the order of this id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>array witch contents all the order items watches on the condition of the id</returns>
        /// <exception cref="Exception">in case there are no order item with this order id</exception>
        public OrderItem[] getOrderItemByOrder(int orderId)
        {
            int index = 0;
            int count = 0;
            for (int i = 0; i < Config.indexOrderItems; i++)
            {
                if(orderId== orderItemsArr[i]._orderId)
                    count++;   //counting how much items there are who have this order id  
            }
            if(count> 0)
            {
                OrderItem[] _orderItemsByOrderArr = new OrderItem[count];
                for (int i = 0; i < Config.indexOrderItems; i++)
                {
                    if (orderId == orderItemsArr[i]._orderId)
                    {
                        _orderItemsByOrderArr[index] = orderItemsArr[i];
                        index++;
                    }
                }
                return _orderItemsByOrderArr;
            }
            throw new Exception("there are no order with this id!");
            
        }


        /// <summary>
        /// get all the order items
        /// </summary>
        /// <returns>array whitch contents there items</returns>
        public OrderItem[] getOrderItemsArr()
        {
            OrderItem[] orderItemsArrToReturn = new OrderItem[Config.indexOrderItems];
            for(int i = 0; i != Config.indexOrderItems; i++)
            {
                orderItemsArrToReturn[i] = orderItemsArr[i];
            }
            return orderItemsArrToReturn;
        }

    }
}
