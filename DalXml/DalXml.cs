﻿using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed internal class DalXml : IDal
    {
        private DalXml() { }
        public static IDal Instance { get; } = new DalXml();

        public IProducts product { get; } = new Dal.Products();
        public IOrders order { get; } = new Dal.Order();
        public IOrderItem orderItem { get; } = new Dal.OrderItem();
    }
}
