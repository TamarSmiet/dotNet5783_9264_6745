﻿using DalApi;
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

namespace Dal
{
    internal class OrderItem:IOrderItem
    {
        string orderItemPath = @"OrderItem.xml"; //XElement

        public OrderItem Get(Func<OrderItem?, bool>? predict = null)
        {
            List<OrderItem> ListOrderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(orderItemPath);
            OrderItem orderItem= ListOrderItems.Find(oi=>predict(oi));
            if(orderItem != null)
            {
                return orderItem;
            }
            else
                throw new Exceptions.RequestedItemNotFoundException("order item with this id does not exist!");

            

        }

        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? predict = null)
        {
            List<OrderItem> ListOrderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(orderItemPath);
            if (predict == null)
            {
                return from orderI in ListOrderItems
                       select orderI;
            }
            else
            {
                return from orderI in ListOrderItems
                       where predict(orderI)
                       select orderI;
            }
        }

        public int Add(DO.OrderItem orderItem)
        {
            //List<OrderItem> ListOrderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(orderItemPath);
            //if(ListOrderItems.FirstOrDefault(o => orderItem._id == o.)!=null)
            //    throw new Exceptions.ItemAlreadyExistsException("order allready exists") { ItemAlreadyExists = orderItem.ToString() };

            XElement orderItemRootElem = XMLTools.LoadListFromXMLElement(orderItemPath);

            XElement orderItemToAdd = (from oi in orderItemRootElem.Elements()
                                     where int.Parse(oi.Element("ID").Value) == orderItem._id
                                     select oi).FirstOrDefault();
            if (orderItemToAdd != null)
                throw new ItemAlreadyExistsException("order item allready exists") { ItemAlreadyExists = orderItem.ToString() };

            orderItemRootElem.Add(orderItemToAdd);
            XMLTools.SaveListToXMLElement(orderItemRootElem, orderItemPath);
            return orderItem._id;
            //return Config.idOrder;

        }

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

        public DO.OrderItem Get(Func<DO.OrderItem?, bool>? predict = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? predict = null)
        {
            throw new NotImplementedException();
        }
    }
}