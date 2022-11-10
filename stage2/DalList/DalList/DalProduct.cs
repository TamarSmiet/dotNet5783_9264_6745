
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
                    productsArr[i] = productsArr[Config.indexProducts-1];//moves the last product to the index to delete
                    Config.indexProducts--;
                    break;
                }
            }
        }


        /// <summary>
        /// updade the details of a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="amountInStock"></param>
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
        
    

