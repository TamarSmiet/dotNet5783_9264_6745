
using DO;
using System.Net;
using System.Xml.Linq;
using static Dal.DataSource;

namespace Dal
{
    public struct DalOrders
    {

        /// <summary>
        /// add an order
        /// </summary>
        /// <param name="order"></param>
        /// <exception cref="Exception">in case of the id allready is in the array</exception>
        public void addOrder(Orders order)
        {
            for(int i=0;i!=Config.indexOrders;i++)
            {
                if(order._orderId == ordersArr[i]._orderId)
                {
                    throw new Exception("order item allready exists");
                }
            }
            ordersArr[Config.indexOrders++] = order;
        }


        /// <summary>
        /// delete an order
        /// </summary>
        /// <param name="oId"></param>
        public void deleteOrder(int oId)
        {
            for (int i = 0; i < Config.indexOrders; i++)
            {
                if (oId == ordersArr[i]._orderId)
                {
                    ordersArr[i] = ordersArr[Config.indexOrders - 1];//moves the last order of the array to the place of the order to delete
                    Config.indexOrders--;
                    break;
                }
            }
        }
       
        /// <summary>
        /// update the details of an order
        /// </summary>
        /// <param name="order">get the new details for the order to update in an object</param>
        public void updateOrder(Orders order)
        {
            for (int i = 0; i != Config.indexOrders; i++)
            {
                if (order._orderId == ordersArr[i]._orderId)
                {
                    ordersArr[i] = order;
                }
            }
        }

        /// <summary>
        /// get a specific order by id
        /// </summary>
        /// <param name="oId">get the id of the order to return</param>
        /// <returns>return the order object</returns>
        /// <exception cref="Exception">in case that didn't found the id in the array</exception>
        public Orders getOrder(int oId)
        {
            for (int i = 0; i != Config.indexOrders; i++)
            {
                if ( ordersArr[i]._orderId== oId)
                {
                    return ordersArr[i];
                }
            }
            throw new Exception("order with this id does not exist!");
        }


        /// <summary>
        /// get all the orders of the array
        /// </summary>
        /// <returns>return an array with all the orders</returns>
        public Orders[] getOrdersArr()
        {
            Orders[] ordersArrToReturn=new Orders[Config.indexOrders];
            for(int i = 0; i != Config.indexOrders; i++)
            {
                ordersArrToReturn[i] = ordersArr[i];
            }
            return ordersArrToReturn;

        }

    }
}
