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
    /// Interaction logic for ItemInCartToUpdateWindow.xaml
    /// </summary>
    public partial class ItemInCartToUpdateWindow : Window
    {

        static BlApi.IBl? bl = BlApi.Factory.Get();
        public OrderItem ItemInCartToUpdate
        {
            get { return (OrderItem)GetValue(ItemInCartToUpdateProperty); }
            set { SetValue(ItemInCartToUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemInCartToUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemInCartToUpdateProperty =
            DependencyProperty.Register("ItemInCartToUpdate", typeof(OrderItem), typeof(ItemInCartToUpdateWindow));



        public int amountOfItemsToAdd
        {
            get { return (int)GetValue(amountOfItemsToAddProperty); }
            set { SetValue(amountOfItemsToAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for amountOfItemsToAdd.  This enables animation, styling, binding, etc...
        public static DependencyProperty amountOfItemsToAddProperty =
            DependencyProperty.Register("amountOfItemsToAdd", typeof(int), typeof(ItemInCartToUpdateWindow));

        public Action<OrderItem> action1;
        public Action<ProductItem> action2;
        public static Cart MyCart { get; set; }

        public ItemInCartToUpdateWindow(Cart myCart, OrderItem itemInCartToUpdate, Action<OrderItem> action1, Action<ProductItem> action2)
        {
            ItemInCartToUpdate = itemInCartToUpdate;
            amountOfItemsToAdd = new int();
            amountOfItemsToAdd = itemInCartToUpdate.AmountItems;
            MyCart = myCart;
            InitializeComponent();
            this.action1 = action1;
            this.action2 = action2;
        }



        private void Plus_Click(object sender, RoutedEventArgs e)
        {

            amountOfItemsToAdd++;
        }
        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            if (amountOfItemsToAdd > 0)
            {
                amountOfItemsToAdd--;
            }

        }

        private void UpdateItemCart_Click(object sender, RoutedEventArgs e)
        {
            MyCart = bl.Cart.UpdateProductInCart(MyCart, ItemInCartToUpdate.Id, amountOfItemsToAdd);
            action1(ItemInCartToUpdate);
            Product product= bl.Product.GetProduct(ItemInCartToUpdate.Id);
            ProductItem productItem = new ProductItem()
            {
                Id = ItemInCartToUpdate.Id,
                Name = ItemInCartToUpdate.Name,
                category = product.Category,
                AmountItemInCart = amountOfItemsToAdd,
                price = product.Price,
                IsInStock = true ? product.AmountInStock > 0 : false,

            };
            action2(productItem);
            this.Close();
            
        }

    }
}
