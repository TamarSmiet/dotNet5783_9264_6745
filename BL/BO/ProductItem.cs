using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO;

public class ProductItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double price { get; set; }
    public eCategory? category { get; set; }
    public  bool IsInStock  { get; set; }
    public int AmountItemInCart { get; set; }

    public override string ToString() => $@"
    Product item ID={Id}: {Name}, 
    category - {category}
    Price: {price}
    in stock: {IsInStock}
    Amout in your cart:{AmountItemInCart}
";

}
