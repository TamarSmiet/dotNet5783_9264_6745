using System.Diagnostics;
using static DO.Enums;

namespace DO;

/// <summary>
/// 
/// </summary>
public struct Products
{
    public int _productId { get; set; }
    public string _productName { get; set; }
    public double _price { get; set; }
    public eCategory _category { get; set; }
    public int _amountInStock { get; set; }


  
public override string ToString() => $@";
Product ID = {_productId}: {_productName},
category - {_category}
Price: {_price}
Amount in stock: {_amountInStock}
"; 
}
