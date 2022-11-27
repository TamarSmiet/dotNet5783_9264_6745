using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO;

public class OrderForList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public StatusOrder Status { get; set; }
    public int AmountProducts   { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    Order ID={Id}: 
    name: {Name}, 
    Status of order: {Status}
    amount of item: {AmountProducts}
    Total sum:{TotalPrice}
";

}
