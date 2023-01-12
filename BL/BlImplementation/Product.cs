using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using DO;

namespace BlImplementation;

internal class Product:IProduct
{
    DalApi.IDal? Dal = DalApi.Factory.Get();
    public IEnumerable<BO.ProductForList?> GetProducts()
    {
        IEnumerable<DO.Products?> productListFromDo = Dal.product.GetAll();

        var productsList = from DO.Products? product in productListFromDo
                           where product != null 
                           orderby product.Value._price ascending
                           select new BO.ProductForList()
                           {
                               Id = product.Value._productId,
                               Price = product.Value._price,
                               Name = product.Value._productName,
                               Category = (BO.Enums.eCategory?)product.Value._category
                           };
                     
                     
              
        return productsList.ToList();
       
    }

    public IEnumerable<BO.ProductItem?> GetAllProductItems()
    {
        IEnumerable<DO.Products?> productListFromDo = Dal.product.GetAll();
        var productsList = from DO.Products? product in productListFromDo
                           where product != null
                           orderby product.Value._price ascending
                           select new BO.ProductItem()
                           {
                               Id = product.Value._productId,
                               price = product.Value._price,
                               Name = product.Value._productName,
                               category = (BO.Enums.eCategory?)product.Value._category,
                               IsInStock = true ? product.Value._amountInStock > 0 : false,
                               AmountItemInCart = 0
                           };


        return productsList.ToList();
    }

    public IEnumerable<BO.ProductItem?> GetAllProductItemsGroupingByCategorie()
    {
        IEnumerable<DO.Products?> productListFromDo = Dal.product.GetAll();
        //var productsList = from DO.Products? product in productListFromDo
        //                   where product != null
        //                   orderby product.Value._price ascending
        //                   select new BO.ProductItem()
        //                   {
        //                       Id = product.Value._productId,
        //                       price = product.Value._price,
        //                       Name = product.Value._productName,
        //                       category = (BO.Enums.eCategory?)product.Value._category,
        //                       IsInStock = true ? product.Value._amountInStock > 0 : false,
        //                       AmountItemInCart = 0
        //                   };

        var GropupingProducts = (from DO.Products? product in productListFromDo
                                 group product by product.Value._category into catGroup
                                 from pr in catGroup
                                 select new BO.ProductItem()
                                 {
                                     Id = pr.Value._productId,
                                     price = pr.Value._price,
                                     Name = pr.Value._productName,
                                     category = (BO.Enums.eCategory?)pr.Value._category,
                                     IsInStock = true ? pr.Value._amountInStock > 0 : false,
                                     AmountItemInCart = 0
                                 });

        //productListFromDo = new(GropupingProducts);
        return GropupingProducts.ToList();

        //return productsList.ToList();
    }
    public IEnumerable<BO.ProductForList> GetProductForListByCategory(BO.Enums.eCategory category)
    {
        IEnumerable<DO.Products?> productListFromDo = Dal.product.GetAll(item => (BO.Enums.eCategory)item!.Value._category! == category);
        
        var productList =productListFromDo
                        .Where(product => product != null)
                        .Select (product => new BO.ProductForList()
                        {
                            Id = product.Value._productId,
                            Price = product.Value._price,
                            Name = product.Value._productName,
                            Category = (BO.Enums.eCategory?)product.Value._category
                        })
                        .OrderBy(product=>product.Price);

      
        return productList;
    }

    public IEnumerable<BO.ProductItem> GetProductItemByCategory(BO.Enums.eCategory category)
    {
        IEnumerable<DO.Products?> productListFromDo = Dal.product.GetAll(item => (BO.Enums.eCategory)item!.Value._category! == category);

        var productList = productListFromDo
                        .Where(product => product != null)
                        .Select(product => new BO.ProductItem()
                        {
                            Id = product.Value._productId,
                            price = product.Value._price,
                            Name = product.Value._productName,
                            category = (BO.Enums.eCategory?)product.Value._category,
                            IsInStock = true ? product.Value._amountInStock > 0 : false,
                            AmountItemInCart = 0
                        })
                        .OrderBy(product => product.price);

        return productList;
    //     public int IdOrder { get; set; }
    //public int Id { get; set; }
    //public string? Name { get; set; }
    //public double Price { get; set; }
    //public int AmountItems { get; set; }
    //public double TotalPriceItem { get; set; }

}

public BO.Product GetProduct(int id)
    {
        if (id<0)
            throw new BO.InvalidValueException("not valid id");
        DO.Products getP=new DO.Products();
        try
        {
            
            getP = Dal.product.Get(product => product!.Value._productId == id);
        }

        catch(DO.Exceptions.RequestedItemNotFoundException ex)
        {
            throw new BO.NotFound("product not found",ex) ;
        }
        BO.Product temp = new BO.Product()
        {
            Id=getP._productId,
            Name = getP._productName,
            Price=getP._price,
            Category = (BO.Enums.eCategory?)getP._category,
            AmountInStock=getP._amountInStock
        };
        return temp;
    }

    public BO.ProductForList GetProductForList(int id)
    {
        if (id < 0)
            throw new BO.InvalidValueException("not valid id");
        DO.Products getP = new DO.Products();
        try
        {

            getP = Dal.product.Get(product => product!.Value._productId == id);
        }

        catch (DO.Exceptions.RequestedItemNotFoundException ex)
        {
            throw new BO.NotFound("product not found", ex);
        }
        BO.ProductForList temp = new BO.ProductForList()
        {
            Id = getP._productId,
            Name = getP._productName,
            Price = getP._price,
            Category = (BO.Enums.eCategory?)getP._category,
        };
        return temp;
    }


    public int AddProduct(BO.Product p)
    {
        DO.Products newProduct;
        try
        {
            IEnumerable<DO.Products?> productListFromDo = Dal.product.GetAll();
            newProduct = new DO.Products()
            {
                _productId = p.Id,
                _productName = p.Name,
                _price = p.Price,
                _category = (DO.Enums.eCategory?)p.Category,
                _amountInStock = p.AmountInStock
            };
        }

        catch (DO.Exceptions.InputNotValidException ex)
        {
            throw new BO.InvalidValueException(ex.Message);
        }
       
        try
        {
            int id = Dal.product.Add(newProduct);
            return id;
        }
        catch(DO.Exceptions.InputNotValidException ex)
        {
            throw new BO.InvalidValueException(ex.Message);
        }
       
    }

    public void RemoveProduct(int id)
    {
        IEnumerable<DO.OrderItem?> orderItemListFromDo = Dal.orderItem.GetAll();
        if (id < 0)
            throw new BO.InvalidValueException("not valid id");

        foreach (DO.OrderItem? item in orderItemListFromDo)
        { 
            if(item!=null)
                if (item.Value._productId == id)
                    throw new Exception();
        }
        foreach (DO.Products? p in Dal.product.GetAll())
            if(p != null)
                if (p.Value._productId == id)
                {
                    try
                    {
                        Dal.product.Delete(id);
                    }
                    catch
                    {
                        throw new TheOperationFailed("couldnt delete product");
                    }
                }

    }

    public void UpdateProduct(BO.Product p)
    {
        if (p.Id < 0)
            throw new BO.InvalidValueException("not valid id");
        else if (p.Name == "")
            throw new BO.InvalidValueException("not valid name");
        else if (p.Price < 0)
            throw new BO.InvalidValueException("not valid price");
        else if (p.AmountInStock < 0)
            throw new BO.InvalidValueException("not valid amont in stock");
        DO.Products updatedProduct = new DO.Products()
        {
            _productId = p.Id,
            _productName = p.Name,
            _price = p.Price,
            _category = (DO.Enums.eCategory?)p.Category,
            _amountInStock = p.AmountInStock
        };
        try
        {
            Dal.product.Update(updatedProduct);
        }
        catch
        {
            throw new TheOperationFailed("couldnt update product");
        }
        

    }

    public BO.ProductItem GetProductItem(int id, BO.Cart customerCart)
    {
        if (id < 0)
            throw new BO.InvalidValueException("not valid id");
        DO.Products getP = new DO.Products();
        try
        {
            
            getP = Dal.product.Get(product => product!.Value._productId == id);
        }
        catch (DO.Exceptions.RequestedItemNotFoundException ex)
        {
            throw new BO.NotFound("product not found", ex);
        }
 
        bool inStock = false;
        int amountItems=0;



        if (customerCart.Orders != null)
            foreach (BO.OrderItem? orderItem in customerCart.Orders)
            {
                if (orderItem != null)
                    if(orderItem.Id==id)
                        amountItems = orderItem.AmountItems;
            }

        if (getP._amountInStock>0)
            inStock= true;

        BO.ProductItem productToReturn = new BO.ProductItem()
        {
            Id = id,
            Name = getP._productName,
            price = getP._price,
            category = (BO.Enums.eCategory?)getP._category,
            IsInStock = inStock,
            AmountItemInCart = amountItems
        };
        return productToReturn;
    }

    
}
