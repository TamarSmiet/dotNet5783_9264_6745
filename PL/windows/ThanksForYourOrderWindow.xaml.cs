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
    /// Interaction logic for ThanksForYourOrderWindow.xaml
    /// </summary>
    public partial class ThanksForYourOrderWindow : Window
    {
        //public  string CustomerName { get; set; }
        public string Thanks { get; set; }
        public ThanksForYourOrderWindow(string name)
        {
            //CustomerName= name;
            Thanks = $@"{name}, thanks for your order!";
            InitializeComponent();
           
        }
    }
}
