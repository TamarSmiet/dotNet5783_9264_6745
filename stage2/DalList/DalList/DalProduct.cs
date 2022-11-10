
using DO;
using static Dal.DataSource;

namespace Dal
{

    public struct DalProduct
    {

        /// <summary>
        /// adding product
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="Exception"></exception>
        public void addProduct(Products product)
        {
            for (int i = 0; i != Config.indexProducts; i++)
            {
                if (product._productId == productsArr[i]._productId)
                {
                    throw new Exception("product allready exists");
                }
            }
            productsArr[Config.indexProducts++] = product;
        }

        /// <summary>
        /// delete product
        /// </summary>
        /// <param name="pId"></param>
        public void deleteProduct(int pId)
        {
            for (int i = 0; i < Config.indexProducts; i++)
            {
                if (pId == productsArr[i]._productId)
                {
                    productsArr[i] = productsArr[Config.indexProducts-1];//moves the last product of the array to the place of the product to delete
                    Config.indexProducts--;
                    break;
                }
            }
        }


        /// <summary>
        /// updade the details of a product
        /// </summary>
        /// <param name="product">get the new details to update in an object product</param>
        public void updateProduct(Products product)
        {
            for (int i = 0; i != Config.indexProducts; i++)
            {
                if (product._productId == productsArr[i]._productId)
                {
                    productsArr[i] = product;
                }
            }
        }


        /// <summary>
        /// display a specific product
        /// </summary>
        /// <param name="pId">look for this id product in the products array</param>
        /// <returns>the product that has this id</returns>
        /// <exception cref="Exception">in case he didn't found the product</exception>
        public Products getproduct(int pId)
        {
            for (int i = 0; i != Config.indexProducts; i++)
            {
                if (pId == productsArr[i]._productId)
                {
                    return productsArr[i];
                }
            }
            throw new Exception("product with this id does not exist!");
            
        }


        /// <summary>
        /// get all the product of the array
        /// </summary>
        /// <returns>all the product</returns>
        public Products[] getAllProducts()
        {
            Products[] productArrToReturn = new Products[Config.indexProducts];
            for(int i = 0; i != Config.indexProducts; i++)
            {
                productArrToReturn[i]=productsArr[i];
            }
            return productArrToReturn;
        }


    }
}
        
    

