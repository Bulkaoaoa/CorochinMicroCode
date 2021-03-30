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
    /// Interaction logic for ListOfOrdersPage.xaml
    /// </summary>
    public partial class ListOfOrdersPage : Page
    {
        public ListOfOrdersPage()
        {
            InitializeComponent();
            DataGrdOrders.ItemsSource = AppData.Context.Order.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrdOrders.ItemsSource = AppData.Context.Order.ToList();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataGrdOrders.ItemsSource = AppData.Context.Order.ToList().Where(p=>p.Id.ToString().Contains(TxtBoxSearch.Text.ToLower().Trim())).ToList();
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            var listOfComps = AppData.Context.Component.ToList();
            foreach (var item in listOfComps.ToList())
            {
                item.CountInOrder = 0;
            }
            AppData.MainFrame.Navigate(new Pages.AddEdit());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //Вот тут надо будет добавлять CountOnOrder для компонентов как в приложении лунева
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
