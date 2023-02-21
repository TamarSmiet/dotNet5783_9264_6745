using DalApi;
using DO;
using System.Runtime.CompilerServices;
using static Dal.DataSource;
namespace Dal
{
    internal class DalOrderItem : IOrderItem
    {
        /// <summary>
        /// adding order item
        /// </summary>
        /// <param name="orderItem"></param>
        /// <exception cref="Exception">in case this order item allready exist </exception>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public int Add(OrderItem orderItem)
        {
            if (orderItemsList.Contains(orderItem))
            {
                throw new Exceptions.ItemAlreadyExistsException("order allready exists") { ItemAlreadyExists = orderItem.ToString() };

            }
            orderItemsList.Add(orderItem);
            return Config.idOrder;

        }

        /// <summary>
        /// update order item
        /// </summary>
        /// <param name="orderItem">get the new details to updatein an object orderItem</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(OrderItem orderItem)
        {
            for (int i = 0; i < orderItemsList.Count; i++)
            {
                if (orderItem._id == orderItemsList[i]?._id)
                {
                    orderItemsList[i] = orderItem;
                }
            }
        }

        /// <summary>
        /// delete order item of the array
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        /// get a specific order item by its id
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns>return the object</returns>
        /// <exception cref="Exception">in case he didn't found this id in the array</exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        /// get all the order items
        /// </summary>
        /// <returns>array whitch contents there items</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? predict = null)
        {
            List<OrderItem?> orderItemsListCopy = new List<OrderItem?>();
            if (predict == null)
            {
                orderItemsListCopy = (from OrderItem? orderI in orderItemsList
                                      select orderI).ToList();

            }
            else
            {


                orderItemsListCopy = (from OrderItem? orderI in orderItemsList
                                      where predict(orderI)
                                      select orderI).ToList();
            }
            return orderItemsListCopy;
        }



    }
}

