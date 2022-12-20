using System;
using System.Collections.Generic;
using System.Linq;
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
    public IEnumerable<BO.ProductForList> GetProducts()
    {
        IEnumerable<DO.Products?> productListFromDo = Dal.product.GetAll();
        List<BO.ProductForList> tempList = new List<BO.ProductForList>();
        
        foreach (DO.Products? product in productListFromDo)
        {
            BO.ProductForList temp = new BO.ProductForList();
            if (product != null)
            {
                temp.Id = product.Value._productId;
                temp.Price = product.Value._price;
                temp.Name = product.Value._productName;
                temp.Category = (BO.Enums.eCategory?)product.Value._category;
                tempList.Add(temp);
            }

            
        }
        return tempList;
    }

    public IEnumerable<BO.ProductForList> GetProductByCategory(BO.Enums.eCategory category)
    {
        IEnumerable<DO.Products?> productListFromDo = Dal.product.GetAll(item => (BO.Enums.eCategory)item!.Value._category! == category);
        List<BO.ProductForList> tempList = new List<BO.ProductForList>();
        
        foreach (DO.Products? product in productListFromDo)
        {
            BO.ProductForList temp = new BO.ProductForList();
            if (product != null)
            {
                temp.Id = product.Value._productId;
                temp.Price = product.Value._price;
                temp.Name = product.Value._productName;
                temp.Category = (BO.Enums.eCategory?)product.Value._category;
                tempList.Add(temp);
            }
        }
        return tempList;
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

   


    public void AddProduct(BO.Product p)
    {
        IEnumerable<DO.Products?> productListFromDo = Dal.product.GetAll();
        DO.Products newProduct = new DO.Products()
        {
            _productId = p.Id,
            _productName = p.Name,
            _price = p.Price,
            _category = (DO.Enums.eCategory?)p.Category,
            _amountInStock = p.AmountInStock
        };
        try
        {
            Dal.product.Add(newProduct);
        }
        catch
        {
            throw new TheOperationFailed("couldnt add product");
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
