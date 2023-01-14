using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class DescriptionStatusDate
{
    public DateTime? Date { get; set; }
    public string? Description { get; set; }

    public override string ToString() => $@"
    {Date} - {Description}
";
}
