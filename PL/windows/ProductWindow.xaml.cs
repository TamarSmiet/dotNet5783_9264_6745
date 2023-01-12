using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        BlApi.IBl? bl = BlApi.Factory.Get();
        public Product product
        {
            get { return (Product)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        } 
        public static DependencyProperty productProperty =
            DependencyProperty.Register("product", typeof(Product), typeof(ProductWindow));
        public System.Array categories { get; set; } = Enum.GetValues(typeof(BO.Enums.eCategory));
        public string buttonContent { get; set; }
        private Action<ProductForList> action;
        
        public ProductWindow(string buttonName, Action<ProductForList> action)
        {
            buttonContent = buttonName;
            product=new Product();
            InitializeComponent();
            this.action= action;
        }
        public ProductWindow(int id, Action<ProductForList> action)
        {
            buttonContent = "Update";
            product = bl.Product.GetProduct(id);
            InitializeComponent();
            this.action= action;
        }
        private void BoxForPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            bool isAddedOrUpdated = true;
            
            
                if(buttonContent=="Add")
                { 
                    
                    try
                    {
                        int id = bl.Product.AddProduct(product);
                        action(bl.Product.GetProductForList(id));
                    
                    }
                    catch (BO.InvalidValueException exeption)
                    {
                        MessageBox.Show("couldnt add!\n" + exeption.ToString());
                        isAddedOrUpdated=false;
                    }

                }
                
                else if(buttonContent =="Update")
                {

                    try
                    {
                        bl.Product.UpdateProduct(product);
                        action(bl.Product.GetProductForList(product.Id));
                    
                    }
                    catch (BO.InvalidValueException exeption)
                    {
                        MessageBox.Show("couldnt update!\n" + exeption.ToString());
                        isAddedOrUpdated=false;
                    }
                }
                if (isAddedOrUpdated)
                {
                    this.Close();
                }
           
            
               

        }

        
    }
}
