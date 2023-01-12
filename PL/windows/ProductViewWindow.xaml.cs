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
    /// Interaction logic for ProductViewWindow.xaml
    /// </summary>
    /// 

    public partial class ProductViewWindow : Window
    {
        static BlApi.IBl? bl = BlApi.Factory.Get();
        public ProductItem productToView { get; set; }
        public Cart MyCart { get; set; }
        Action<ProductItem> action;

        public ProductViewWindow(ProductItem productToView, Cart cart,Action<ProductItem> action)
        {
            this.productToView = productToView;
            MyCart = cart;
            this.action = action;
            InitializeComponent();

        }

        private void AddProductToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.AddProductToCart(MyCart, productToView.Id);
                productToView.AmountItemInCart++;
                action(productToView);
                this.Close();
            }
            catch(InvalidValueException ex)
            {
                MessageBox.Show(ex.Message);
            }          
           

        }
    }
}
