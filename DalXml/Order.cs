using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
using static DO.Exceptions;

namespace Dal
{
    internal class Order : IOrders
    {
        string orderPath = @"Order.xml"; //XElement
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Orders Get(Func<Orders?, bool>? predict = null)
        {
   
            List<Orders?> ListOrder = XMLTools.LoadListFromXMLSerializer<Orders?>(orderPath);
            Orders? order = ListOrder.Find(o => predict != null && predict(o));
            if (order != null)
                return (Orders) order;
            else
                throw new RequestedItemNotFoundException("order with this id does not exist!");

        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<DO.Orders?> GetAll(Func<DO.Orders?, bool>? predict = null)
        {

            

            List<Orders?> ListOrders = XMLTools.LoadListFromXMLSerializer<Orders?>(orderPath);
            if (predict == null)
            {
                return (from order in ListOrders
                        select order).ToList();

            }
            else
            {
                return (from order in ListOrders
                        where predict(order)
                        select order).ToList();

            }

        }
        private static int getOrderId()
        {
            XElement config = XMLTools.LoadListFromXMLElement(@"config.xml");
            int id = (int)config.Element("idOrder"); 
            id++;
            config.Element("idOrder")!.SetValue(id);
            XMLTools.SaveListToXMLElement(config,@"config.xml");
            return id;

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(DO.Orders order)
        {
            List<Orders?> ListOrders = XMLTools.LoadListFromXMLSerializer<Orders?>(orderPath);
            if (ListOrders.FirstOrDefault(o => o.Value._orderId == order._orderId) != null)
                throw new ItemAlreadyExistsException("order already exists") { ItemAlreadyExists = order.ToString() };


            order._orderId = getOrderId();
            order._orderDate = DateTime.Now;
            order._shippingDate = DateTime.MinValue;
            order._deliveryDate = DateTime.MinValue;
            ListOrders.Add(order);
            XMLTools.SaveListToXMLSerializer(ListOrders, orderPath);
            return order._orderId;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(Orders order)
        {
            List<Orders?> ListOrders = XMLTools.LoadListFromXMLSerializer<Orders?>(orderPath);
            Orders? orderToUpdate = ListOrders.Find(o => o.Value._orderId == order._orderId);
            if (orderToUpdate != null)
            {
                ListOrders.Remove(orderToUpdate);
                ListOrders.Add(order);
            }
            else
                throw new Exceptions.RequestedItemNotFoundException("not found order!");
            XMLTools.SaveListToXMLSerializer(ListOrders, orderPath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int oId)
        {

            List<Orders?> ListOrders = XMLTools.LoadListFromXMLSerializer<Orders?>(orderPath);
            Orders? order = ListOrders.Find(o => o.Value._orderId == oId);
            if (order != null)
                ListOrders.Remove(order);
            else
                throw new RequestedItemNotFoundException("item not found") { RequestedItemNotFound = oId.ToString() };
            XMLTools.SaveListToXMLSerializer(ListOrders, orderPath);
            
        }

    }
}
