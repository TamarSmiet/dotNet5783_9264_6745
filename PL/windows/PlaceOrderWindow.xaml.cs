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
    /// Interaction logic for PlaceOrderWindow.xaml
    /// </summary>
    public partial class PlaceOrderWindow : Window
    {
        static BlApi.IBl? bl = BlApi.Factory.Get();
        public Cart MyCart
        {
            get { return (Cart)GetValue(MyCartProperty); }
            set { SetValue(MyCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyCart.  This enables animation, styling, binding, etc...
        public static DependencyProperty MyCartProperty =
            DependencyProperty.Register("MyCart", typeof(Cart), typeof(PlaceOrderWindow));

        public PlaceOrderWindow(Cart myCart)
        {
            MyCart = myCart;
            InitializeComponent();
        }


        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.PlaceOrder(MyCart);
                this.Close();
                new ThanksForYourOrderWindow(MyCart.CustomerName).Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
