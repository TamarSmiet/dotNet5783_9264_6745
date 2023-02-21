using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowManagerWindow_Click(object sender, RoutedEventArgs e) => new ManagerWindow().Show();

        private void ShowNewOrderWindow_Click(object sender, RoutedEventArgs e) => new CatalogWindow().Show();

        private void ShowTrackOrderWindow_Click(object sender, RoutedEventArgs e) => new OrderTrackingWindow().Show();

        private void ShowSimulatorWindow_Click(object sender, RoutedEventArgs e) => new SimulatorWindow().Show();
       
    }
}
