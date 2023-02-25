
using DO;
using DalApi;
using System.Net;
using System.Xml.Linq;
using static Dal.DataSource;
using static DO.Exceptions;
using System.Runtime.CompilerServices;

namespace Dal
{
    internal class DalOrder : IOrders
    {

        /// <summary>
        /// add an order
        /// </summary>
        /// <param name="order"></param>
        /// <exception cref="Exception">in case of the id allready is in the array</exception>

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(Orders order)
        {

            if (ordersList.Contains(order))
            {
                throw new Exceptions.ItemAlreadyExistsException("order allready exists") { ItemAlreadyExists = order.ToString() };

            }

            order._orderId = Config.IdOrder;
            order._orderDate = DateTime.Now;
            order._shippingDate = DateTime.MinValue;
            order._deliveryDate = DateTime.MinValue;
            ordersList.Add(order);
            return order._orderId;
        }


        /// <summary>
        /// delete an order
        /// </summary>
        /// <param name="oId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int oId)
        {
            for (int i = 0; i < ordersList.Count; i++)
            {
                if (oId == ordersList[i]?._orderId)
                {
                    ordersList.Remove(ordersList[i]);
                    return;
                }
            }
            throw new Exceptions.RequestedItemNotFoundException("item not found") { RequestedItemNotFound = oId.ToString() };
        }

        /// <summary>
        /// update the details of an order
        /// </summary>
        /// <param name="order">get the new details for the order to update in an object</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(Orders order)
        {
            for (int i = 0; i < ordersList.Count; i++)
            {
                if (order._orderId == ordersList[i]?._orderId)
                {
                    ordersList[i] = order;
                }
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public Orders Get(Func<Orders?, bool>? predict = null)
        {

            List<Orders?> ordersListCopy = new List<Orders?>();
            Orders order = new Orders();
            for (int i = 0; i < ordersList.Count; i++)
            {
                ordersListCopy.Add(ordersList[i]);
            }
            if (predict != null)
            {
                try
                {
                    order = ordersListCopy
                            .Where(order => predict(order))
                            .Select(order => (Orders)order!).First();
                }
                catch
                {
                    throw new RequestedItemNotFoundException("order with this id does not exist!");
                }
            }

            return order;

        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Orders?> GetAll(Func<Orders?, bool>? predict = null)
        {
            List<Orders?> ordersListCopy = new List<Orders?>();
            if (predict == null)
            {
                ordersListCopy = (from Orders? order in ordersList
                                  select order).ToList();

            }
            else
            {
                ordersListCopy = (from Orders? order in ordersList
                                  where predict(order)
                                  select order).ToList();

            }
            return ordersListCopy;
        }
    }
}
