using System;

namespace Simulator
{
    public static class Simulator
    {
        private static readonly BlApi.IBl? bl = BlApi.Factory.Get();

        private static readonly Random rand = new(DateTime.Now.Millisecond);

        private static volatile bool isActive = false;

        public delegate void updateDel(int x, int time, BO.Order o);
        public static event updateDel? ScreenUpdate;

        public delegate void noMoreOrders();
        public static event noMoreOrders? Wating;

        public static void Activate()
        {
            isActive = true;
            new Thread(() =>
            {
                while (isActive)
                {
                    int? id = bl?.Order.selectOrderToTreatment();

                    if (id != null)
                    {
                        BO.Order order = bl!.Order.GetOrder((int)id);
                        int time = rand.Next(3, 10);
                        ScreenUpdate!((int)id, time * 1000, order);
                        Thread.Sleep(1000 * time);
                        if (order.ExpeditionDate ==DateTime.MinValue)
                        {
                            bl?.Order.UpdateShippingOrder((int)id);
                        }
                        else if (order.DeliveryDate ==DateTime.MinValue)
                        {
                            bl?.Order.UpdateDeliveryOrder((int)id);
                        }
                    }
                    else//return null - no more order to work on, shout down simulator
                    {
                        Wating!();
                        Thread.Sleep(1000);
                    }

                }
            }).Start();
        }

        public static void DeAcitavet() => isActive = false;
    }
}