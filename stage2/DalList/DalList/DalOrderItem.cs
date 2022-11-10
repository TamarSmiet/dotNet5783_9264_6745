
using DO;
using static Dal.DataSource;
namespace Dal
{
    public struct DalOrderItem
    {
        public void addOrderItem(OrderItem orderItem)
        {
            //orderItem._id = Config.IdOrder;
            for(int i=0;i<Config.indexOrderItems;i++)
            {
                if(orderItem._id== orderItemsArr[i]._id)
                {
                    throw new Exception ("product allready exists");
                }
            }
            orderItemsArr[Config.indexOrderItems++] = orderItem;

        }

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

        public void deleteOrderItem(int id)
        {
            for (int i = 0; i < Config.indexOrderItems; i++)
            {
                if (id == orderItemsArr[i]._id)
                {
                    orderItemsArr[i] = orderItemsArr[Config.indexOrderItems - 1];
                    Config.indexOrderItems--;
                    break; 
                }
            }
        }

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

        public OrderItem[] getOrderItemByOrder(int orderId)
        {
            int index = 0;
            int count = 0;
            for (int i = 0; i < Config.indexOrderItems; i++)
            {
                if(orderId== orderItemsArr[i]._orderId)
                    count++;     
            }
            OrderItem[] _orderItemsByOrderArr=new OrderItem[count];
            for (int i = 0; i < Config.indexOrderItems; i++)
            {
                if(orderId== orderItemsArr[i]._orderId)
                {
                    _orderItemsByOrderArr[index] = orderItemsArr[i];
                    index++;
                }
            }
            return _orderItemsByOrderArr;
        }

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
