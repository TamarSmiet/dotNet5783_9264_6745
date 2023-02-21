using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        public Orders Get(Func<Orders?, bool>? predict = null)
        {
   
            List<Orders?> ListOrder = XMLTools.LoadListFromXMLSerializer<Orders?>(orderPath);
        //Orders order = new Orders();
        Orders? order = ListOrder.Find(o => predict != null && predict(o));
            if (order != null)
                return (Orders) order;
            else
                throw new RequestedItemNotFoundException("order with this id does not exist!");

        ////for (int i = 0; i < ordersList.Count; i++)
        ////{
        ////    ordersListCopy.Add(ordersList[i]);
        ////}
        //if (predict != null)
        //{
        //    try
        //    {
        //        order = productRootElem
        //                .Where(order => predict(order))
        //                .Select(order => (Orders)order!).First();
        //    }
        //    catch
        //    {
        //        throw new RequestedItemNotFoundException("order with this id does not exist!");
        //    }
        //}

        //return order;

    }

        //private DO.Orders? _convertFromXMLToProduct(XElement x)
        //{
        //    //Enum.TryParse<DO.Enums.eCategory>(x.Element("Category")!.Value, out DO.Enums.eCategory category);
        //    DO.Orders order = new()
        //    {
        //        _orderId = int.Parse(x.Element("_orderId")!.Value.ToString()),
        //        _customerName = x.Element("_customerName")!.Value.ToString(),
        //        _email = x.Element("_email")!.Value.ToString(),
        //        _address= x.Element("_address")!.Value.ToString(),
        //        _orderDate = DateTime.Parse(x.Element("_orderDate").Value),
        //        _shippingDate = DateTime.Parse(x.Element("_shippingDate").Value),
        //        _deliveryDate = DateTime.Parse(x.Element("_deliveryDate").Value)
        //        //_deliveryDate = DateTime.ParseExact(x.Element("_deliveryDate").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                
        //    };
        //    return (DO.Orders?)order;
        //}
        public IEnumerable<DO.Orders?> GetAll(Func<DO.Orders?, bool>? predict = null)
        {

            //XElement orderData = XMLTools.LoadListFromXMLElement(orderPath);
            //IEnumerable<DO.Orders?> p = orderData.Elements().Where(x => x != null).Select(x => _convertFromXMLToProduct(x)
            //).Where(x => predict == null || predict(x));
            //IEnumerable<DO.Orders?> tmpProducts = (IEnumerable<DO.Orders?>)p.ToList();
            //if (p == null) { throw new(); }
            //return tmpProducts;

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
            //if (ordersList.Contains(order))
            //{
            //    throw new Exceptions.ItemAlreadyExistsException("order allready exists") { ItemAlreadyExists = order.ToString() };

            //}

            //order._orderId = Config.IdOrder;
            //order._orderDate = DateTime.Now;
            //order._shippingDate = DateTime.MinValue;
            //order._deliveryDate = DateTime.MinValue;
            //ordersList.Add(order);
            return order._orderId;
        }

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

            //for (int i = 0; i < ordersList.Count; i++)
            //{
            //    if (order._orderId == ordersList[i]?._orderId)
            //    {
            //        ordersList[i] = order;
            //    }
            //}
        }

        public void Delete(int oId)
        {

            List<Orders?> ListOrders = XMLTools.LoadListFromXMLSerializer<Orders?>(orderPath);
            Orders? order = ListOrders.Find(o => o.Value._orderId == oId);
            if (order != null)
                ListOrders.Remove(order);
            else
                throw new RequestedItemNotFoundException("item not found") { RequestedItemNotFound = oId.ToString() };
            XMLTools.SaveListToXMLSerializer(ListOrders, orderPath);
            //for (int i = 0; i < ordersList.Count; i++)
            //{
            //    if (oId == ordersList[i]?._orderId)
            //    {
            //        ordersList.Remove(ordersList[i]);
            //        return;
            //    }
            //}
            //throw new RequestedItemNotFoundException("item not found") { RequestedItemNotFound = oId.ToString() };
        }

    }
}
