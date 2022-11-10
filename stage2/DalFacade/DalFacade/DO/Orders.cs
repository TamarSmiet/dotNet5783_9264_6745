using System.Security.Cryptography;

namespace DO;

public struct Orders
{
    public int _orderId { get; set; }
    public string _customerName { get; set; }
    public string _email { get; set; }
    public string _address { get; set; }
    public DateTime _orderDate { get; set; }
    public DateTime _shippingDate { get; set; }
    public DateTime _deliveryDate { get; set; }


public override string ToString() => $@"
order ID : {_orderId}
customer name: {_customerName}
email: {_email}
order date: {_orderDate} 
shipping date: {_shippingDate} 
delivery date: {_deliveryDate}
";
}
