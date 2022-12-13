using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using Dal;


namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public  List<OrderItem?>? Orders { get; set; }=new List<OrderItem?>();
    public double TotalPriceCart { get; set; }

    public override string ToString() => $@"
    Name: {CustomerName}, 
    Email - {Email}
    Adress: {Address}
    List of Item:{Orders.ToString()}
    Total sum:{TotalPriceCart}
";

}
