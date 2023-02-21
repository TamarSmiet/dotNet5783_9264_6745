using DalApi;
using DO;
using Dal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Xml.Linq;
using static DO.Exceptions;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using static DO.Enums;
using System.Runtime.CompilerServices;

namespace Dal
{
    internal class Products : IProducts
    {
        string productPath = @"Products.xml"; //XElement
        DO.Products ICrud<DO.Products>.Get(Func<DO.Products?, bool>? predict)
        {
            List<DO.Products?> ListProduct = XMLTools.LoadListFromXMLSerializer<DO.Products?>(productPath);
            DO.Products? product = ListProduct.Find(p => predict != null && predict(p));
            if (product != null)
                return (DO.Products)product;
            else
                throw new RequestedItemNotFoundException("product with this id does not exist!");

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Products?> GetAll(Func<DO.Products?, bool>? predict = null)
        {
            List<DO.Products?> productRootElem = XMLTools.LoadListFromXMLSerializer<DO.Products?>(productPath);
            if (predict == null)
            {
                return (from product in productRootElem
                        select product).ToList();

            }

            else
            {
                return (from product in productRootElem
                        where predict(product)
                        select product).ToList();
       
        
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(DO.Products product)
        {
            
            XElement productRootElem = XMLTools.LoadListFromXMLElement(productPath);
            XElement productToUpdate = (from p in productRootElem.Elements()
                                        where int.Parse(p.Element("_productId").Value) == product._productId
                                        select p).FirstOrDefault();

            if (productToUpdate != null)
            {
                productToUpdate.Element("_productId").Value = product._productId.ToString();
                productToUpdate.Element("_productName").Value = product._productName;
                productToUpdate.Element("_price").Value = product._price.ToString();
                productToUpdate.Element("_category").Value = product._category.ToString();
                productToUpdate.Element("_amountInStock").Value = product._amountInStock.ToString();
       
                XMLTools.SaveListToXMLElement(productRootElem, productPath);
            }
            else
                throw new RequestedItemNotFoundException($"bad product id: {product._productId}");

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int pId)
        {
            XElement productRootElem = XMLTools.LoadListFromXMLElement(productPath);
            XElement productToDelete = (from product in productRootElem.Elements()
                                        where int.Parse(product.Element("ID").Value) == pId
                                        select product).FirstOrDefault();

            if (productToDelete != null)
            {
                productToDelete.Remove();
                XMLTools.SaveListToXMLElement(productRootElem, productPath);
            }
            else
                throw new RequestedItemNotFoundException("product with this id does not exist!") { RequestedItemNotFound = pId.ToString() };
        }
        private static int getProductId()
        {
            XElement config = XMLTools.LoadListFromXMLElement(@"config.xml");
            int id = (int)config.Element("idProduct");
            id++;
            config.Element("idProduct")!.SetValue(id);
            XMLTools.SaveListToXMLElement(config, "config.xml");
            return id;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(DO.Products product)
        {
            XElement productRootElem = XMLTools.LoadListFromXMLElement(productPath);


            XElement productToAdd = (from p in productRootElem.Elements()
                                     where int.Parse(p.Element("_productId").Value) == product._productId
                                     select p).FirstOrDefault();

            if (productToAdd != null)
                throw new ItemAlreadyExistsException("order allready exists") { ItemAlreadyExists = product.ToString() };

            Regex regex1 = new Regex("^[a-zA-Z]+$");
            Regex regex2 = new Regex("^[0-9]+$");
            string productPrice = product._price.ToString();
            string productAmount = product._amountInStock.ToString();
            bool hasAlpha = regex1.IsMatch(productPrice) || regex1.IsMatch(productAmount);
            bool hasNum = regex2.IsMatch(product._productName);
            if (hasAlpha == true)
            {
                throw new InputNotValidException("price and amount must have only numbers");
            }
            if (hasNum == true)
            {
                throw new InputNotValidException("name must have only letters");
            }
            if (product._category == null)
            {
                throw new InputNotValidException("you must fill the category");
            }
            product._productId = getProductId();
            XElement productElement = new XElement("Products", new XElement("_productId", product._productId),
                                    new XElement("_productName", product._productName),
                                    new XElement("_price", product._price),
                                    new XElement("_category", product._category),
                                    new XElement("_amountInStock", product._amountInStock));
            productRootElem.Add(productElement);

            XMLTools.SaveListToXMLElement(productRootElem, productPath);
            return product._productId;
        }

    }
}