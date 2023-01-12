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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        static BlApi.IBl? bl = BlApi.Factory.Get();

        public IEnumerable< OrderForList?>orderList
        {
            get { return (IEnumerable<OrderForList?>)GetValue(orderListProperty); }
            set { SetValue(orderListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderListProperty =
            DependencyProperty.Register("orderList", typeof(IEnumerable<OrderForList?>), typeof(OrderListWindow));

        public OrderForList selectedOrder { get; set; }
        public OrderListWindow()
        {
            
            InitializeComponent();
            orderList = bl.Order.GetOrders();
        }
       
        private void updateOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new OrderUpdateWindow(selectedOrder.Id).Show();
        }

    }
}
