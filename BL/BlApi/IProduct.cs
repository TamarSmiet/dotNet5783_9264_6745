using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IProduct
    {
        public IEnumerable<BO.ProductForList?> GetProducts();
        public Product GetProduct(int id);
        public IEnumerable<BO.ProductForList?> GetProductByCategory(Enums.eCategory category);
        public ProductItem GetProductItem(int id,Cart customerCart);
        public void AddProduct (Product product);
        public void RemoveProduct (int id);
        public void UpdateProduct(Product product);


    }
}
