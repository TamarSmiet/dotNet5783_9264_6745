﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList?> GetOrders();
    public Order GetOrder(int id);
    public Order UpdateShippingOrder(int id);
    public Order UpdateDeliveryOrder(int id);
  
    public OrderTracking trackingOrder(int id);

    public int? selectOrderToTreatment();
    //bonnus
    //public Order UpdateOrder(string id);

}
