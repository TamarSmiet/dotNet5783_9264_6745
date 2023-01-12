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

    public override string ToString()
    {
        string item = "";
        if(Orders!=null)
        {
            foreach (OrderItem oi in Orders)
                item += oi;
                    
        }
        return 
        $@"
        Name: {CustomerName}, 
        Email - {Email}
        Adress: {Address}
        List of Item:{item}
        Total sum:{TotalPriceCart}
        ";
    }
    


}
