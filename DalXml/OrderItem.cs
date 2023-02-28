using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DO.Exceptions;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

namespace Dal
{
    internal class OrderItem : IOrderItem
    {
        string orderItemPath = @"OrderItem.xml"; //XElement
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.OrderItem Get(Func<DO.OrderItem?, bool>? predict = null)
        {
            List<DO.OrderItem?> ListOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);
            DO.OrderItem? orderItem = ListOrderItems.Find(oi => predict(oi));
            if (orderItem != null)
            {
                return (DO.OrderItem)orderItem;
            }
            else
                throw new Exceptions.RequestedItemNotFoundException("order item with this id does not exist!");



        }

        private DO.OrderItem? _convertFromXMLToProduct(XElement x)
        {
           
            DO.OrderItem orderItem = new()
            {
                 
                _id = int.Parse(x.Element("_id")!.Value.ToString()),
                _orderId = int.Parse(x.Element("_orderId").Value.ToString()),
                _productId=int.Parse(x.Element("_productId").Value.ToString()),
                _pricePerUnit=double.Parse(x.Element("_pricePerUnit").Value.ToString()),
                _quantity =int.Parse(x.Element("_quantity").Value.ToString())
            };
            return (DO.OrderItem?)orderItem;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? predict = null)
        {
            if (predict == null)
            {
                List<DO.OrderItem?> ProductData = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);
                if (ProductData == null) { throw new(); }
                return ProductData;
            }
               else
            {
                XElement ProductData = XMLTools.LoadListFromXMLElement(orderItemPath);
                IEnumerable<DO.OrderItem?> orderItems =(( ProductData.Elements()
                                              .Where(x => x != null )
                                              .Select(x => _convertFromXMLToProduct(x)))
                                              .Where(x=> predict(x))).ToList();
                                              

                
                if (orderItems == null) { throw new(); }
                return orderItems;
               
            }

            
        }
        private static int getOrderIemId()
        {
            XElement config = XMLTools.LoadListFromXMLElement(@"config.xml");
            int id = (int)config.Element("idOrderItem");
            id++;
            config.Element("idOrderItem")!.SetValue(id);
            XMLTools.SaveListToXMLElement(config, @"config.xml");
            return id;

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(DO.OrderItem orderItem)
        {
            
            XElement orderItemRootElem = XMLTools.LoadListFromXMLElement(orderItemPath);
            XElement orderItemElement = new XElement("OrderItem", new XElement("_id", getOrderIemId()),
                                    new XElement("_orderId", orderItem._orderId),
                                    new XElement("_productId", orderItem._productId),
                                    new XElement("_pricePerUnit", orderItem._pricePerUnit),
                                    new XElement("_quantity", orderItem._quantity));
            orderItemRootElem.Add(orderItemElement);

            XMLTools.SaveListToXMLElement(orderItemRootElem, orderItemPath);
            
            return orderItem._id;

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(DO.OrderItem orderItem)
        {
            XElement orderItemRootElem = XMLTools.LoadListFromXMLElement(orderItemPath);

            XElement orderItemToUpdate = (from oi in orderItemRootElem.Elements()
                                          where int.Parse(oi.Element("ID").Value) == orderItem._id
                                          select oi).FirstOrDefault();

            if (orderItemToUpdate != null)
            {
                orderItemToUpdate.Element("ID").Value = orderItem._id.ToString();
                orderItemToUpdate.Element("OrderID").Value = orderItem._orderId.ToString();
                orderItemToUpdate.Element("ProductID").Value = orderItem._productId.ToString();
                orderItemToUpdate.Element("Price").Value = orderItem._pricePerUnit.ToString();
                orderItemToUpdate.Element("Amount").Value = orderItem._quantity.ToString();

                XMLTools.SaveListToXMLElement(orderItemRootElem, orderItemPath);
            }
            else
                throw new RequestedItemNotFoundException($"bad order item id: {orderItem._id}");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)
        {


            XElement orderItemRootElem = XMLTools.LoadListFromXMLElement(orderItemPath);
            XElement orderItemToDelete = (from product in orderItemRootElem.Elements()
                                          where int.Parse(product.Element("ID").Value) == id
                                          select product).FirstOrDefault();

            if (orderItemToDelete != null)
            {
                orderItemToDelete.Remove();
                XMLTools.SaveListToXMLElement(orderItemRootElem, orderItemPath);
            }
            else
                throw new RequestedItemNotFoundException("order item with this id does not exist!") { RequestedItemNotFound = id.ToString() };
        }

       

        
    }
}
