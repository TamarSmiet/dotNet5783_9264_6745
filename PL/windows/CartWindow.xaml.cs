using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public Cart MyCart { get; set; }

        static BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<OrderItem> ItemListInMyCart
        {
            get { return (ObservableCollection<OrderItem>)GetValue(ItemListInMyCartProperty); }
            set { SetValue(ItemListInMyCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static DependencyProperty ItemListInMyCartProperty =
            DependencyProperty.Register("ItemListInMyCart", typeof(ObservableCollection<OrderItem>), typeof(CartWindow));

        Action<ProductItem> action;
        public OrderItem selectedItemToUpdate { get; set; } 
       
        public CartWindow(Cart myCart, Action<ProductItem> action)
        {
            
            MyCart = myCart; 
            this.action= action;
            InitializeComponent();
            ItemListInMyCart = new ObservableCollection<OrderItem>(myCart.Orders.Cast<OrderItem>());
        }

        public void updateItemInCart(OrderItem itemToUpdate )
        {
            var item = ItemListInMyCart.FirstOrDefault(item => item.Id == itemToUpdate.Id);
            int index = ItemListInMyCart.IndexOf(item);
            OrderItem oi = new OrderItem()
            {
                Id = itemToUpdate.Id,
                IdOrder = itemToUpdate.IdOrder,
                Name = itemToUpdate.Name,
                AmountItems = itemToUpdate.AmountItems,
                Price = itemToUpdate.Price,
                TotalPriceItem = itemToUpdate.TotalPriceItem
            };
            ItemListInMyCart[index] = oi;

            ProductItem productItem = new ProductItem()
            {
                Id = itemToUpdate.Id,
                Name = itemToUpdate.Name,
                price = itemToUpdate.Price,
                AmountItemInCart = itemToUpdate.AmountItems,

                //             public int Id { get; set; }
                //public string? Name { get; set; }
                //public double price { get; set; }
                //public eCategory? category { get; set; }
                //public bool IsInStock { get; set; }
                //public int AmountItemInCart { get; set; }

            };
           

        }

        //private void updateAmountInCart(OrderItem orderItem)
        //{
        //    List<ProductItem> productItemList = new List<ProductItem?>(bl.Product.GetAllProductItems());
        //    var item = productItemList.FirstOrDefault(item => item.Id == productItem.Id);
        //    int index = productItemList.IndexOf(item);
        //    ProductItem updateProduct = new ProductItem()
        //    {
        //        Id = productItem.Id,
        //        Name = productItem.Name,
        //        category = productItem.category,
        //        price = productItem.price,
        //        AmountItemInCart = productItem.AmountItemInCart,
        //        IsInStock = productItem.IsInStock
        //    };
        //    productItemList[index] = updateProduct;
        //}
        private void UpdateItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ItemInCartToUpdateWindow(MyCart,selectedItemToUpdate,updateItemInCart, action).Show();
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            new PlaceOrderWindow(MyCart).Show();
        }
    }
}
