using BlApi;
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
using BlImplementation;
using System.CodeDom;
using static BO.Enums;

namespace PL.windows
{
    /// <summary>
    /// Logique d'interaction pour ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl = new Bl();
        public enum clear { getAllCategories };
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListview.ItemsSource = bl.Product.GetProducts();

            categorySelector.Items.Add(BO.Enums.eCategory.cellular);
            categorySelector.Items.Add(BO.Enums.eCategory.kitchen);
            categorySelector.Items.Add(BO.Enums.eCategory.hair);
            categorySelector.Items.Add(BO.Enums.eCategory.house);
            categorySelector.Items.Add("getAllCategories");
            
        }

   
        
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            BO.Enums.eCategory category;
            if (categorySelector.SelectedItem.ToString() == "getAllCategories")
                ProductListview.ItemsSource = bl.Product.GetProducts();
            else
            {
                ProductListview.ItemsSource = bl.Product.GetProductByCategory((BO.Enums.eCategory)categorySelector.SelectedItem);

            }
            
        }

        private void ProductListWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string button = "Add";
            new ProductWindow(button).Show();
            
        }

        private void updateProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)   
        {
            BO.ProductForList product = (BO.ProductForList)ProductListview.SelectedValue;
            new ProductWindow(product).Show();
           
        }

        private void GridViewColumn_GotMouseCapture(object sender, MouseEventArgs e)
        {

        }
    }
}


