
using DO;
using DalApi;
using System.Net;
using System.Xml.Linq;
using static Dal.DataSource;

namespace Dal
{
    internal class DalOrder:IOrders
    {

        /// <summary>
        /// add an order
        /// </summary>
        /// <param name="order"></param>
        /// <exception cref="Exception">in case of the id allready is in the array</exception>
        public int Add(Orders order)
        {
           
            if(ordersList.Contains(order))
            {
                throw new Exception("order allready exists");
            }
            

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
        public void Delete(int oId)
        {
            for (int i = 0; i < ordersList.Count; i++)
            {
                if (oId == ordersList[i]._orderId)
                {
                    ordersList.Remove(ordersList[i]);
                    return;
                }
            }
            throw new Exception("vfgvfbvf");
        }
       
        /// <summary>
        /// update the details of an order
        /// </summary>
        /// <param name="order">get the new details for the order to update in an object</param>
        public void Update(Orders order)
        {
            for (int i = 0; i < ordersList.Count; i++)
            {
                if (order._orderId == ordersList[i]._orderId)
                {
                    ordersList[i] = order;
                }
            }
        }

        /// <summary>
        /// get a specific order by id
        /// </summary>
        /// <param name="oId">get the id of the order to return</param>
        /// <returns>return the order object</returns>
        /// <exception cref="Exception">in case that didn't found the id in the array</exception>
        public Orders Get(int oId)
        {
            for (int i = 0; i <ordersList.Count; i++)
            {
                if (ordersList[i]._orderId== oId)
                {
                    return ordersList[i];
                }
            }
            throw new Exception("order with this id does not exist!");
        }


        /// <summary>
        /// get all the orders of the array
        /// </summary>
        /// <returns>return an array with all the orders</returns>
        public IEnumerable<Orders> GetAll()
        {
            List<Orders> ordersListToReturn =new ();
            for(int i = 0; i < ordersList.Count; i++)
            {
                ordersListToReturn.Add(ordersList[i]);
            }
            return ordersListToReturn;

        }

    }
}
