using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    sealed public class DalList:IDal
    {
        //לממש פרופרטי

        //internal IProducts products
        //{
        //    get { return products; }
        //}

        //לא יודעת האם זה כאן......?
        public IProducts Product => new DalProduct();
        public IOrders Order => new DalOrder();
        public IOrderItem OrderItem => new DalOrderItem();
    }
}
