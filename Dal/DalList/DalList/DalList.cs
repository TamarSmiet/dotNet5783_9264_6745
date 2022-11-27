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
        

        //לא יודעת האם זה כאן......?
        public IProducts product => new DalProduct();


        public IOrders order => new DalOrder();


        public IOrderItem orderItem => new DalOrderItem();

    }
}
