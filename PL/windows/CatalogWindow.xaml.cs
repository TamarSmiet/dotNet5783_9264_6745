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
using static BO.Enums;

namespace PL.windows
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {

        static BlApi.IBl? bl = BlApi.Factory.Get();

        public ObservableCollection<ProductItem?> productItemList
        {
            get { return (ObservableCollection<ProductItem?>)GetValue(productItemListProperty); }
            set { SetValue(productItemListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productItemListProperty =
            DependencyProperty.Register("productItemList", typeof(ObservableCollection<ProductItem?>), typeof(CatalogWindow));

        public ProductItem selectedProduct { get; set; }
        public Cart myCart { get; set; } = new Cart();
        public System.Array categoriesForCatalog { get; set; } = Enum.GetValues(typeof(eCategory));
        public eCategory selectedCategoryForCatalog { get; set; }

        public CatalogWindow()
        {
            
            InitializeComponent();
            productItemList =new ObservableCollection<ProductItem?>(bl.Product.GetAllProductItems().Cast<ProductItem>());
        }
       
        private void updateAmountInCart(ProductItem productItem)
        {
            var item=productItemList.FirstOrDefault(item=>item.Id==productItem.Id);
            int index=productItemList.IndexOf(item);
            ProductItem updateProduct = new ProductItem()
            {
                Id = productItem.Id,
                Name = productItem.Name,
                category = productItem.category,
                price = productItem.price,
                AmountItemInCart = productItem.AmountItemInCart,
                IsInStock = productItem.IsInStock
            };
            productItemList[index]=updateProduct;
        }
        private void ProductView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ProductViewWindow(selectedProduct, myCart, updateAmountInCart).Show();
        }

        private void ViewCart_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(myCart, updateAmountInCart).Show();
        }
        private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productItemList = new ObservableCollection<ProductItem?>(bl.Product.GetProductItemByCategory(selectedCategoryForCatalog).Cast<ProductItem?>());

        }

        private void GetAllGrouping_Click(object sender, RoutedEventArgs e)
        {
            productItemList= new ObservableCollection<ProductItem?>(bl.Product.GetAllProductItemsGroupingByCategorie());
        }
    }
}
