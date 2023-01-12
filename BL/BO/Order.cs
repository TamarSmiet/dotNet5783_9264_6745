using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO;

public class Order
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public StatusOrder? OrderStatus { get; set; }
    public DateTime? PlaceOrderDate { get; set; }
    public DateTime? ExpeditionDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public List<OrderItem?>? ItemList { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString()
    {
        string item = "";
        if (ItemList != null)
        {
            foreach (OrderItem? orderItem in ItemList)
            {
                item += orderItem.ToString();
            }
        }
        return
        $@"  Order ID={Id}: {CustomerName}, 
        Email - {Email}
        Adress: {Address}
        Status of order:{OrderStatus}
        Order Date: {PlaceOrderDate}
        Ship Date: {ExpeditionDate}
        Delivery Date: {DeliveryDate}
        List of Item:{item}
        Total sum:{TotalPrice}";
    }
    

}
