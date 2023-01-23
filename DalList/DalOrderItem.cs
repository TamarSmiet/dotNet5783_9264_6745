using DalApi;
using DO;

using static Dal.DataSource;
namespace Dal
{
    internal class DalOrderItem:IOrderItem
    {
        /// <summary>
        /// adding order item
        /// </summary>
        /// <param name="orderItem"></param>
        /// <exception cref="Exception">in case this order item allready exist </exception>
        public int Add(OrderItem orderItem)
        {
            if (orderItemsList.Contains(orderItem))
            {
                throw new Exceptions.ItemAlreadyExistsException("order allready exists") { ItemAlreadyExists = orderItem.ToString() };

            }
            orderItemsList.Add( orderItem);
            return Config.idOrder;

        }

        /// <summary>
        /// update order item
        /// </summary>
        /// <param name="orderItem">get the new details to updatein an object orderItem</param>
        public void Update(OrderItem orderItem)
        {
            for(int i=0;i< orderItemsList.Count; i++)
            {
                if(orderItem._id== orderItemsList[i]?._id)
                {
                    orderItemsList[i] = orderItem;
                }
            }
        }

        /// <summary>
        /// delete order item of the array
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            for (int i = 0; i < orderItemsList.Count; i++)
            {
                if (id == orderItemsList[i]?._id)
                {
                    orderItemsList.Remove(orderItemsList[i]);
                    return; 
                }
            }
            throw new Exceptions.RequestedItemNotFoundException("item not found") { RequestedItemNotFound = id.ToString() };

        }

        /// <summary>
        /// get the details of an order item by its order id and product id
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <returns>return the object witch has there ids</returns>
        /// <exception cref="Exception">in case he didn't found</exception>
        //public OrderItem getOrderItemByOandP(int orderId ,int productId)
        //{
        //    for (int i = 0; i < orderItemsList.Count; i++)
        //    {
        //        if ((orderId == orderItemsList[i]?._orderId) && (productId == orderItemsList[i]?._productId))
        //        {
        //            return new OrderItem() {_id= orderItemsList[i]?._id };
        //        }
                
 
        //    }
        //    throw new Exceptions.RequestedItemNotFoundException("order item with there ids does not exist!") { RequestedItemNotFound = orderId.ToString() + productId .ToString()};

     

        /// <summary>
        /// get a specific order item by its id
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns>return the object</returns>
        /// <exception cref="Exception">in case he didn't found this id in the array</exception>
        public OrderItem Get(Func<OrderItem?, bool>? predict = null)
        {

            List<OrderItem?> orderItemListCopy = new List<OrderItem?>();
            for (int i = 0; i < orderItemsList.Count; i++)
            {
                orderItemListCopy.Add(orderItemsList[i]);
            }

            return (from OrderItem? orderItem in orderItemListCopy
                   where predict != null && predict(orderItem)
                   select (OrderItem)orderItem!).FirstOrDefault();

           
            throw new Exceptions.RequestedItemNotFoundException("order item with there ids does not exist!") { };
        }


        /// <summary>
        /// get all the order items that are in the order of this id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>array witch contents all the order items watches on the condition of the id</returns>
        /// <exception cref="Exception">in case there are no order item with this order id</exception>
        //public IEnumerable<OrderItem> getOrderItemByOrder(int orderId)
        //{
           
        //    int count = 0;
        //    for (int i = 0; i < orderItemsList.Count; i++)
        //    {
        //        if(orderId== orderItemsList[i]._orderId)
        //            count++;   //counting how much items there are who have this order id  
        //    }
        //    if(count> 0)
        //    {
        //        List<OrderItem> _orderItemsByOrderList = new ();
        //        for (int i = 0; i < orderItemsList.Count; i++)
        //        {
        //            if (orderId == orderItemsList[i]._orderId)
        //            {
        //                _orderItemsByOrderList.Add(orderItemsList[i])  ;
                        
        //            }
        //        }
        //        return _orderItemsByOrderList;
        //    }
        //    throw new Exceptions.RequestedItemNotFoundException("order item with there ids does not exist!") { RequestedItemNotFound = orderId.ToString() };

        //}


        /// <summary>
        /// get all the order items
        /// </summary>
        /// <returns>array whitch contents there items</returns>
        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? predict = null)
        {
            List<OrderItem?> orderItemsListCopy = new List<OrderItem?>();
            if (predict == null)
            {
                orderItemsListCopy =(from OrderItem? orderI in orderItemsList
                                     select orderI).ToList();
              
            }
            else
            {
                
              
                orderItemsListCopy =(from OrderItem? orderI in orderItemsList
                                     where predict(orderI)
                                     select orderI).ToList();
            }
            return orderItemsListCopy;
        }



    }
}

