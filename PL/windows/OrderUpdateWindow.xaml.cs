using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.windows
{
    /// <summary>
    /// Interaction logic for OrderUpdateWindow.xaml
    /// </summary>
    public partial class OrderUpdateWindow : Window
    {

        static BlApi.IBl? bl = BlApi.Factory.Get();
        public Order order
        {
            get { return (Order)GetValue(orderProperty); }
            set { SetValue(orderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for order.  This enables animation, styling, binding, etc...
        public static readonly  DependencyProperty orderProperty =
            DependencyProperty.Register("order", typeof(Order), typeof(OrderUpdateWindow));

        public DateTime? expeditionDate
        {
            get { return (DateTime?)GetValue(ExpeditionDateProperty); }
            set { SetValue(ExpeditionDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for expeditionDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpeditionDateProperty =
            DependencyProperty.Register("expeditionDate", typeof(DateTime?), typeof(OrderUpdateWindow));


        public DateTime? deliveryDate
        {
            get { return (DateTime?)GetValue(deliveryDateProperty); }
            set { SetValue(deliveryDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeliveryDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty deliveryDateProperty =
            DependencyProperty.Register("deliveryDate", typeof(DateTime?), typeof(OrderUpdateWindow));



        public OrderTracking orderTracking
        {
            get { return (OrderTracking)GetValue( orderTrackingProperty); }
            set { SetValue( orderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderTracking orderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty  orderTrackingProperty =
            DependencyProperty.Register("OrderTracking orderTracking", typeof(OrderTracking), typeof(OrderUpdateWindow));

        public OrderUpdateWindow(int id)
        {
            order = bl.Order.GetOrder(id); 
            expeditionDate = order.ExpeditionDate;
            deliveryDate = order.DeliveryDate;
            orderTracking = bl.Order.trackingOrder(id);
            InitializeComponent();
        }


        private void UpdateShippingDate_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Order updatedOrder = bl.Order.UpdateShippingOrder(order.Id);
                expeditionDate = updatedOrder.ExpeditionDate;
            }
            catch(BO.TheOperationFailed ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void UpdateDeliveryDate_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Order updatedOrder = bl.Order.UpdateDeliveryOrder(order.Id);
                deliveryDate = updatedOrder.DeliveryDate;
            }
            catch (BO.TheOperationFailed ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
