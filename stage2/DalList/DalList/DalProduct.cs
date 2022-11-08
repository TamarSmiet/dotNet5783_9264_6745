
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
                {//אמור להיות פו את זה לא יודעת מה הבעיה שלו מיבחינת התחביר 
                    throw new Exception("product allready exists");
                }
            }
            productsArr[Config.indexProducts++] = product;
        }

        public void deleteProduct(int pId)
        {
            for (int i = 0; i != Config.indexProducts; i++)
            {
                if (pId == productsArr[i]._productId)
                {
                    productsArr[i] = productsArr[Config.indexProducts - 1];
                    Config.indexProducts--;
                }
            }
        }

 
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
            return null;
            
        }
//חסרה מתודה של קריאת כל האובייקטים צריך להעתיק למערך חדש התחלתי טיפה לעשות ונתקעתי 
        public void printAllProducts()
        {
          
        }


    }
}
        
    

