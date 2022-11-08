
using DO;
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
                    //אמור להיות פו את זה לא יודעת מה הבעיה שלו מיבחינת התחביר 
                    throw new Exception("order item allready exists");
                }
            }
            ordersArr[Config.indexOrders++] = order;
        }

        public void deleteOrder(int oId)
        {
            for (int i = 0; i != Config.indexOrders; i++)
            {
                if (oId == ordersArr[i]._orderId)
                {
                    ordersArr[i] = ordersArr[Config.indexOrders - 1];
                    Config.indexOrders--;
                }
            }
        }

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

        public Orders getOrder(int oId)
        {
            for (int i = 0; i != Config.indexOrders; i++)
            {
                if ( ordersArr[i]._orderId== oId)
                {
                    return ordersArr[i];
                }
            }
            return null;
        }
    }
}
