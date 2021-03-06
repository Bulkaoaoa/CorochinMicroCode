using CorochinMCWPF.Entites;
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

namespace CorochinMCWPF.Pages
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void BtnWareHouse_Click(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new Pages.ListOfComponentsPage());
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new Pages.ListOfOrdersPage());
        }

        private void BtnWorkPlace_Click(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new Pages.ListOfWorkPlacePage());
        }
    }
}
