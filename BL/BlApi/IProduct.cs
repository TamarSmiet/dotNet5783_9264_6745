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
        public IEnumerable<BO.ProductForList> GetProducts();
        public IEnumerable<BO.ProductItem> GetAllProductItems();
        public IEnumerable<BO.ProductItem> GetAllProductItemsGroupingByCategorie();
        public Product GetProduct(int id);
        public IEnumerable<BO.ProductForList> GetProductForListByCategory(Enums.eCategory category);
        public IEnumerable<BO.ProductItem> GetProductItemByCategory(BO.Enums.eCategory category);
        public ProductItem GetProductItem(int id,Cart customerCart);
        public int AddProduct (Product product);
        public BO.ProductForList GetProductForList(int id);
        public void RemoveProduct (int id);
        public void UpdateProduct(Product product);


    }
}
