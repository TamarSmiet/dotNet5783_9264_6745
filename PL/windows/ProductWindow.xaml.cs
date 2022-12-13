using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace PL.windows
{
    /// <summary>
    /// Logique d'interaction pour AddProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = new Bl();

        public ProductWindow(string buttonName)
        {
            InitializeComponent();
            button.Content = buttonName;
            categorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.eCategory));
            
        }
        public ProductWindow(BO.ProductForList product)
        {
            
            InitializeComponent();
            
            button.Content = "Update";
            BoxForId.IsReadOnly=true;
            categorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.eCategory));
            BoxForId.Text = product.Id.ToString();
            BoxForName.Text = product.Name;
            BoxForPrice.Text = product.Price.ToString();
            categorySelector.SelectedItem = product.Category;
            BoxForAmount.Text = bl.Product.GetProduct(product.Id).AmountInStock.ToString();


            //categorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.eCategory));
        }
        private void BoxForPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            BO.Product product = new BO.Product
            {
                Id= Convert.ToInt32(BoxForId.Text),
                Name= BoxForName.Text,
                Price= Convert.ToDouble(BoxForPrice.Text),
                Category= (BO.Enums.eCategory)categorySelector.SelectedItem,
                AmountInStock= Convert.ToInt32(BoxForAmount.Text)
            };
            if(button.Content=="Add")
            {
                try
                {
                    
                    bl.Product.AddProduct(product);
                    
                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.ToString()); 
                }
            }
                
            else if(button.Content=="Update")
            {
                try
                {
                    bl.Product.UpdateProduct(product);
                    
                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.ToString());
                }
            }
            this.Close();
            new ProductListWindow().ShowDialog();

        }
    }
}
