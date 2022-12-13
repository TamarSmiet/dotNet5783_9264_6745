using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    public int IdOrderItem { get; set; }
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public int AmountItems { get; set; }
    public double TotalPriceItem { get; set; }

    public override string ToString() => $@"
    item number={Id}
    Order Item ID={IdOrderItem}
    name={Name}:
    Price: {Price}
    Amount : {AmountItems}
    sum item={TotalPriceItem}
";

}
