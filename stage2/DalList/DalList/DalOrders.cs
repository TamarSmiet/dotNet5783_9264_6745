
using DO;
using System.Net;
using System.Xml.Linq;
using static Dal.DataSource;

namespace Dal
{
    public struct DalOrders
    {
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

        public void deleteOrder(int oId)
        {
            for (int i = 0; i < Config.indexOrders; i++)
            {
                if (oId == ordersArr[i]._orderId)
                {
                    ordersArr[i] = ordersArr[Config.indexOrders - 1];
                    Config.indexOrders--;
                    break;
                }
            }
        }
       
        public void updateOrder(int idToUpdate, string name, string email,string address,DateTime orderDate, DateTime shippingDate,DateTime deliveryDate)
        {
            for (int i = 0; i != Config.indexOrders; i++)
            {
                if (idToUpdate == ordersArr[i]._orderId)
                {
                    ordersArr[i]._customerName = name;
                    ordersArr[i]._email = email;
                    ordersArr[i]._address = address;
                    ordersArr[i]._deliveryDate = deliveryDate;
                    ordersArr[i]._shippingDate=shippingDate;
                    ordersArr[i]._deliveryDate=deliveryDate;
                }
            }
        }

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
