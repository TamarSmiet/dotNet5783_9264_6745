using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO;

public class OrderTracking
{
    public int Id {set;get;}
    public StatusOrder? Status { set; get; }
    public List<DescriptionStatusDate?>? DescriptionStatus { set; get; }
    public override string ToString() => $@"
    Order tracking ID={Id}
    Status:{Status}
    
";
}
