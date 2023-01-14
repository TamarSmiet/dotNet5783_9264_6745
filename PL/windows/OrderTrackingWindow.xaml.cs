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
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        static BlApi.IBl? bl = BlApi.Factory.Get();

        public int OrderID { get; set; }
        //public OrderTracking orderTracking { get; set; }


        public IEnumerable<DescriptionStatusDate> Tracking
        {
            get { return (IEnumerable<DescriptionStatusDate>)GetValue(TrackingProperty); }
            set { SetValue(TrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Tracking.  This enables animation, styling, binding, etc...
        public static DependencyProperty TrackingProperty =
            DependencyProperty.Register("Tracking", typeof(IEnumerable<DescriptionStatusDate>), typeof(OrderTrackingWindow));


        public OrderTracking orderTracking
        {
            get { return (OrderTracking)GetValue(orderTrackingProperty); }
            set { SetValue(orderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderTracking orderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderTrackingProperty =
            DependencyProperty.Register("OrderTracking orderTracking", typeof(OrderTracking), typeof(OrderUpdateWindow));

        public OrderTrackingWindow()
        {
            InitializeComponent();
        }

        private void TrackOrder_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {
                Tracking = bl.Order.trackingOrder(OrderID).DescriptionStatus;
            }
            catch(BO.InvalidValueException ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
