
using DO;
using static Dal.DataSource;

namespace Dal
{
    public struct DalProduct
    {
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

        public void deleteProduct(int pId)
        {
            for (int i = 0; i < Config.indexProducts; i++)
            {
                //Console.WriteLine(pId+"LLL");
                if (pId == productsArr[i]._productId)
                {
                    productsArr[i] = productsArr[Config.indexProducts-1];
                    Config.indexProducts--;
                    break;
                }
            }
        }


        public void updateProduct(int id, string name, double price,int amountInStock)
        {
            for (int i = 0; i != Config.indexProducts; i++)
            {
                if (id == productsArr[i]._productId)
                {
                    productsArr[i]._productName = name;
                    productsArr[i]._price = price;
                    productsArr[i]._amountInStock = amountInStock;
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
        
    

