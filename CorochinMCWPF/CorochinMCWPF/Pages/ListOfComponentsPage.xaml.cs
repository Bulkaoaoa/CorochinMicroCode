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
    /// Interaction logic for ListOfComponentsPage.xaml
    /// </summary>
    public partial class ListOfComponentsPage : Page
    {
        public ListOfComponentsPage()
        {
            InitializeComponent();
            DataGrdComponents.ItemsSource = AppData.Context.Component.ToList().OrderByDescending(p => p.Count);
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataGrdComponents.ItemsSource = AppData.Context.Component.ToList().Where(p => p.Name.ToLower().Trim()
            .Contains(TxtBoxSearch.Text.Trim().ToLower())).OrderByDescending(p => p.Count);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new AddEditComponent((sender as Button).DataContext as Component));
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var currComponent = (sender as Button).DataContext as Component;
            if (currComponent.IsArchived == false)
            {

                var result = MessageBox.Show($"Вы уверены, что хотите перенести {currComponent.Name} в архив?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var listOfOrders = AppData.Context.Order.ToList().Where(p => p.OrderStatusId == 1 || p.OrderStatusId == 2).ToList();
                    bool IsInOrder = false;
                    foreach (var item in listOfOrders.ToList())
                    {
                        if (item.ListOfComponents.Contains(currComponent))
                        {
                            IsInOrder = true;
                            break;
                        }
                    }

                    if (IsInOrder)
                        MessageBox.Show("Вы не можете удалить этот компонент, так как заказ/заказы с ним ещё не выполнены", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        currComponent.IsArchived = true;
                        AppData.Context.SaveChanges();
                    }
                    DataGrdComponents.ItemsSource = AppData.Context.Component.ToList().OrderByDescending(p => p.Count);
                }
            }
            else
            {
                var result = MessageBox.Show($"Вы уверены, что хотите перенести {currComponent.Name} из архива?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    currComponent.IsArchived = false;
                    AppData.Context.SaveChanges();
                    DataGrdComponents.ItemsSource = AppData.Context.Component.ToList().OrderByDescending(p => p.Count);

                }
            }
        }

        private void DataGrdComponents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Код на открытие окна с полной инфой о компоненте
            var currComp = DataGrdComponents.SelectedItem as Component;
            if (currComp != null)
            {
                Windows.ComponentWindow componentWindow = new Windows.ComponentWindow(currComp);
                componentWindow.Owner = App.Current.MainWindow;
                componentWindow.ShowDialog();
            }
            else
                MessageBox.Show("Вы не выбрали элемент для просмотра", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); ;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrdComponents.ItemsSource = AppData.Context.Component.ToList().OrderByDescending(p => p.Count);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new AddEditComponent());

        }
    }
}
