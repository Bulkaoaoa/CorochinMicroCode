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
            DataGrdOrders.ItemsSource = AppData.Context.Order.ToList().Where(p => p.FirstNameClient.ToLower().Trim().Contains(TxtBoxSearch.Text.ToLower().Trim())
            || p.LastNameClient.ToLower().Trim().Contains(TxtBoxSearch.Text.ToLower().Trim())).ToList();
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            var listOfComps = AppData.Context.Component.ToList();
            foreach (var item in listOfComps.ToList())
            {
                item.CountInOrder = 0;
            }
            AppData.MainFrame.Navigate(new Pages.AddEditOrderPage());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //Вот тут надо будет добавлять CountOnOrder для компонентов как в приложении лунева
            var currOrder = (sender as Button).DataContext as Order;
            foreach (var itemInOrder in AppData.Context.ComponentOfOrder.ToList().Where(p => p.OrderId == currOrder.Id).ToList())
            {
                var currComponent = itemInOrder.Component;

                foreach (var itemInContext in AppData.Context.Component.ToList())
                {
                    if (currComponent.Id == itemInContext.Id)
                        itemInContext.CountInOrder = itemInOrder.Count;
                }
            }
            AppData.MainFrame.Navigate(new Pages.AddEditOrderPage(currOrder));
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите отменить заказ?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
            var currOrder = (sender as Button).DataContext as Order;
            if (currOrder.OrderStatusId == 4)
                MessageBox.Show("Этот заказ уже отменен", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var itemInOrder in AppData.Context.ComponentOfOrder.ToList().Where(p => p.OrderId == currOrder.Id).ToList())
                    {
                        foreach (var itemComponent in AppData.Context.Component.ToList())
                        {
                            if (itemComponent.Id == itemInOrder.ComponentId)
                            {
                                itemInOrder.Count += itemInOrder.Count;
                                AppData.Context.SaveChanges();
                            }
                        }
                        currOrder.OrderStatusId = 4;
                        AppData.Context.SaveChanges();
                    }
                }
                DataGrdOrders.ItemsSource = AppData.Context.Order.ToList();
            }

        }
    }
}
