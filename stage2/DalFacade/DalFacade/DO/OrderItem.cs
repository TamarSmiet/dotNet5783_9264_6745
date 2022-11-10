using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
//using static DalList.DataSource;
using DO;

namespace DO;


public struct OrderItem
{
    public int _id { get; set; }
    public int _orderId { get; set; }
    public int _productId { get; set; }
    public double _pricePerUnit { get; set; }
    public int _quantity { get; set; }

    public override string ToString() => $@"
ID = {_id}
Order ID:{_orderId}
Product ID: {_productId}
Price: {_pricePerUnit}
Amount of product: {_quantity} 
";
}
