﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi;

public interface IDal
{
    public IProducts product { get; }
    public IOrders order { get; }
    public IOrderItem orderItem { get; }
}
