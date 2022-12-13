using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    sealed public class Bl:IBl
    {
        public IProduct Product=>new Product();
        public ICart Cart => new Cart();
        public IOrder Order => new Order();
    }
}
