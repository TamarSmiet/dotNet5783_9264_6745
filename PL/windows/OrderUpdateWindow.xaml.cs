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
        public static readonly DependencyProperty orderProperty =
            DependencyProperty.Register("order", typeof(Order), typeof(OrderUpdateWindow));

        public static DateTime? expeditionDate= new();
        public static DateTime? deliveryDate=new();


        public OrderTracking orderTracking
        {
            get { return (OrderTracking)GetValue( orderTrackingProperty); }
            set { SetValue( orderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderTracking orderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty  orderTrackingProperty =
            DependencyProperty.Register("OrderTracking orderTracking", typeof(OrderTracking), typeof(OrderUpdateWindow));


        //public static OrderTracking orderTracking { get; set; }=new OrderTracking();
        public OrderUpdateWindow(int id)
        {
            order = bl.Order.GetOrder(id);
            expeditionDate = order.ExpeditionDate;
            deliveryDate = order.DeliveryDate;
            //orderTracking = new OrderTracking();
            orderTracking = bl.Order.trackingOrder(id);
            InitializeComponent();
            


        }

        private void UpdateOrder_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (order.ExpeditionDate != expeditionDate)
            {
                bl.Order.UpdateShippingOrder(order.Id);
                new OrderListWindow().ShowDialog();
            }
               

            if(order.DeliveryDate!=deliveryDate)
            {
                bl.Order.UpdateDeliveryOrder(order.Id);
                new OrderListWindow().ShowDialog();
            }
                

            
        }

        private void UpdateShippingDate_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            bl.Order.UpdateShippingOrder(order.Id);
            new OrderListWindow().ShowDialog();
        }

        private void UpdateDeliveryDate_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            bl.Order.UpdateDeliveryOrder(order.Id);
            new OrderListWindow().ShowDialog();
        }
    }
}
