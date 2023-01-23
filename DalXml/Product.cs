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

namespace Dal
{
    internal class Products : IProducts
    {
        string productPath = @"Products.xml"; //XElement



        public Products Get(Func<Products?, bool>? predict = null)
        {
            List<Products?> ListProduct = XMLTools.LoadListFromXMLSerializer<Products?>(productPath);
            //List<Products?> productListCopy = new List<Products?>();
            //  XElement productRootElem = XMLTools.LoadListFromXMLElement(productPath);
            Products? product = ListProduct.Find(p => predict != null && predict(p));
            if (product != null)
                return product;
            else
                throw new RequestedItemNotFoundException("product with this id does not exist!");

            //return ( from product in productRootElem
            //        where predict != null && predict(product)
            //        select product!).FirstOrDefault();

        }

        public IEnumerable<Products?> GetAll(Func<Products?, bool>? predict = null)
        {
            List<Products?> productRootElem = XMLTools.LoadListFromXMLSerializer<Products?>(productPath);
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

        public void Update(DO.Products product)
        {
            //List<Products?> productRootElem = XMLTools.LoadListFromXMLSerializer<Products?>(productPath);
            XElement productRootElem = XMLTools.LoadListFromXMLElement(productPath);
            XElement productToUpdate = (from p in productRootElem.Elements()
                                        where int.Parse(p.Element("ID").Value) == product._productId
                                        select p).FirstOrDefault();

            if (productToUpdate != null)
            {
                productToUpdate.Element("ID").Value = product._productId.ToString();
                productToUpdate.Element("Name").Value = product._productName;
                productToUpdate.Element("Price").Value = product._price.ToString();
                productToUpdate.Element("Category").Value = product._category.ToString();
                productToUpdate.Element("AmountInStock").Value = product._amountInStock.ToString();

                XMLTools.SaveListToXMLElement(productRootElem, productPath);
            }
            else
                throw new RequestedItemNotFoundException($"bad product id: {product._productId}");

        }

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

            //for (int i = 0; i < productsList.Count; i++)
            //{
            //    if (pId == productsList[i]?._productId)
            //    {
            //        productsList.Remove(productsList[i]);
            //        return;
            //    }

            //}
            //throw new Exceptions.RequestedItemNotFoundException("product with there ids does not exist!") { RequestedItemNotFound = pId.ToString() };

        }

        public int Add(DO.Products product)
        {
            XElement productRootElem = XMLTools.LoadListFromXMLElement(productPath);


            XElement productToAdd = (from p in productRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == product._productId
                             select p).FirstOrDefault();

            if(productToAdd!=null)
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
            product._productId = Config.IdProduct;
            XElement productElement = new XElement("Product", new XElement("ID", product._productId),
                                    new XElement("Name", product._productName),
                                    new XElement("Price", product._price),
                                    new XElement("Category", product._category),
                                    new XElement("AmountInStock", product._amountInStock));
            productRootElem.Add(productElement);

            XMLTools.SaveListToXMLElement(productRootElem, productPath);
            return product._productId;
        }

        public DO.Products Get(Func<DO.Products?, bool>? predict = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DO.Products?> GetAll(Func<DO.Products?, bool>? predict = null)
        {
            throw new NotImplementedException();
        }
    }
}