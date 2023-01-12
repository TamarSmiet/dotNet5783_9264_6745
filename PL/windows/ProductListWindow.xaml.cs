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
using BL;
using BO;
using System.CodeDom;
using static BO.Enums;
using System.Collections.ObjectModel;

namespace PL.windows
{
    /// <summary>
    /// Logique d'interaction pour ProductListWindow.xaml
    /// </summary>
    ///
    public partial class ProductListWindow : Window
    {
        static BlApi.IBl? bl = BlApi.Factory.Get();
       
        public  ObservableCollection<ProductForList?> productForLists
        {
            get { return (ObservableCollection<ProductForList?>)GetValue(productForListsProperty); }
            set { SetValue(productForListsProperty, value); }
        }

        public static readonly DependencyProperty productForListsProperty =
            DependencyProperty.Register("productForLists", typeof(ObservableCollection<ProductForList?>), typeof(ProductListWindow));

        public System.Array categories{ get; set; }= Enum.GetValues(typeof(eCategory));
        public eCategory selectedCategory { get; set; }
        public ProductForList selectedProduct { get; set; }
        public ProductListWindow()
        {
            InitializeComponent();
            productForLists = new ObservableCollection<ProductForList?>(bl.Product.GetProducts().Cast<ProductForList?>());


        }
        private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productForLists = new ObservableCollection<ProductForList?>( bl.Product.GetProductForListByCategory(selectedCategory).Cast<ProductForList?>());
            
        }
        private void getAllCategories_Click(object sender, RoutedEventArgs e)
        {
            productForLists = new ObservableCollection<ProductForList?>(bl.Product.GetProducts().Cast<ProductForList?>());

        }



        // private void addProduct(ProductForList product) => productForLists.ToList().Add(product);
        private void addProduct(ProductForList product) => productForLists.Insert(productForLists.Count, product);
        private void updateProduct(ProductForList product)
        {
            var item= productForLists.FirstOrDefault(item=>item.Id==product.Id);
            int index = productForLists.IndexOf(item);
            productForLists[index] = product;
        }
        private void ProductListWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string button = "Add";
            new ProductWindow(button, addProduct).Show();
        }

        private void updateProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList product = selectedProduct;
            new ProductWindow(product.Id, updateProduct).Show();
        }

        private void GridViewColumn_GotMouseCapture(object sender, MouseEventArgs e)
        {

        }

       
    }
}


