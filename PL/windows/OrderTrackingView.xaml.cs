using BO;
using DO;
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
    /// Interaction logic for OrderTrackingView.xaml
    /// </summary>
    public partial class OrderTrackingView : Window
    {
        static BlApi.IBl? bl = BlApi.Factory.Get();

        public int orderId { get; set; }
        public OrderTracking CustomerOrderTracking
        {
            get { return (OrderTracking)GetValue(CustomerOrderTrackingProperty); }
            set { SetValue(CustomerOrderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderTracking orderTracking.  This enables animation, styling, binding, etc...
        public DependencyProperty CustomerOrderTrackingProperty =
            DependencyProperty.Register("CustomerOrderTracking", typeof(OrderTracking), typeof(OrderUpdateWindow));

        public OrderTrackingView(int id)
        {
            orderId= id;
            try
            {
                MessageBox.Show("blbbbblgmrjibmfkl");
                CustomerOrderTracking = bl.Order.trackingOrder(orderId);
            }
            catch(BO.InvalidValueException ex)
            {
                MessageBox.Show(ex.Message);
            }   
            InitializeComponent();
        }
    }
}
