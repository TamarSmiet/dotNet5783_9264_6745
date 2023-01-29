using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    sealed internal class DalList : IDal
    {
        private DalList() { }
        public static IDal Instance { get; } = new DalList();

        public IProducts product => new DalProduct();


        public IOrders order => new DalOrder();


        public IOrderItem orderItem => new DalOrderItem();

    }
}
