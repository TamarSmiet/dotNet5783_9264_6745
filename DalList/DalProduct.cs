
using DO;
using DalApi;
using static Dal.DataSource;
using System.Security.Cryptography;
using System;

namespace Dal
{

    internal class DalProduct : IProducts
    {

        /// <summary>
        /// adding product
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="Exception"></exception>
        public int Add(Products product)
        {
            if (productsList.Contains(product))
            {
                throw new Exceptions.ItemAlreadyExistsException("order allready exists") { ItemAlreadyExists = product.ToString() };
            }
            product._productId = Config.IdProduct;
            productsList.Add(product);
            return product._productId;
        }

        /// <summary>
        /// delete product
        /// </summary>
        /// <param name="pId"></param>
        public void Delete(int pId)
        {

            for (int i = 0; i < productsList.Count; i++)
            {
                if (pId == productsList[i]?._productId)
                {
                    productsList.Remove(productsList[i]);
                    return;
                }

            }
            throw new Exceptions.RequestedItemNotFoundException("product with there ids does not exist!") { RequestedItemNotFound = pId.ToString() };

        }


        /// <summary>
        /// updade the details of a product
        /// </summary>
        /// <param name="product">get the new details to update in an object product</param>
        public void Update(Products product)
        {
            for (int i = 0; i < productsList.Count; i++)
            {
                if (product._productId == productsList[i]?._productId)
                {
                    productsList[i] = product;
                }
            }
        }


        /// <summary>
        /// display a specific product
        /// </summary>
        /// <param name="pId">look for this id product in the products array</param>
        /// <returns>the product that has this id</returns>
        /// <exception cref="Exception">in case he didn't found the product</exception>
        public Products Get(Func<Products?, bool>? predict = null)
        {
            
            List<Products?> productListCopy = new List<Products?>();
            for (int i = 0; i < productsList.Count; i++)
            {
                productListCopy.Add(productsList[i]);
            }

            foreach (Products? product in productListCopy)
            {
                if (predict!=null&&predict(product))
                {
        
                    return(Products)product!;
                }

            }
            throw new Exceptions.RequestedItemNotFoundException("product with there ids does not exist!");

        }


        /// <summary>
        /// get all the product of the array
        /// </summary>
        /// <returns>all the product</returns>
        public IEnumerable<Products?> GetAll(Func<Products?, bool>? predict = null)
        {
            List<Products?> productsListCopy = new List<Products?>();
            if (predict == null)
            {
                foreach (Products? product in productsList)
                {
                    productsListCopy.Add(product);
                }
            }
            else
            {
                foreach (Products? product in productsList)
                {
                    if (predict(product))
                        productsListCopy.Add(product);
                }
            }
            return productsListCopy;
        }


    }
}



