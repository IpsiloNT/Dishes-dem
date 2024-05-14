using Dishes.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dishes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new PAuth());
        }

        private void BTN_Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Back_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if(MainFrame.CanGoBack)
            {
                BTN_Back.Visibility = Visibility.Visible;
            }
            else
            {
                BTN_Back.Visibility = Visibility.Collapsed;
            }
        }
    }
}
