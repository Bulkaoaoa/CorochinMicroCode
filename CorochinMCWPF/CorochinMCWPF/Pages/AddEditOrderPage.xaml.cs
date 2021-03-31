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
    /// Interaction logic for AddEditOrderPage.xaml
    /// </summary>
    public partial class AddEditOrderPage : Page
    {
        private List<Component> _componentsInOrder = new List<Component>();
        private bool _isEdit = false;
        private Order _currOrder;

        public AddEditOrderPage()
        {
            InitializeComponent();
            DataGrdComponents.ItemsSource = AppData.Context.Component.ToList().Where(p => p.IsArchived == false).ToList();
            _isEdit = false;
            BtnAddEditOrder.Content = "Добавить";
        }
        public AddEditOrderPage(Order currOrder)
        {
            InitializeComponent();
            DataGrdComponents.ItemsSource = AppData.Context.Component.ToList().Where(p => p.IsArchived == false).ToList();
            _isEdit = true;
            BtnAddEditOrder.Content = "Сохранить";
            _currOrder = currOrder;
            TxtBoxClientFirstName.Text = currOrder.FirstNameClient;
            TxtBoxClientLastName.Text = currOrder.LastNameClient;
        }

        private void BtnAddEditOrder_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";
            if (string.IsNullOrWhiteSpace(TxtBoxClientFirstName.Text)) errors += "Вы не ввели имя клиента\r\n";
            if (string.IsNullOrWhiteSpace(TxtBoxClientLastName.Text)) errors += "Вы не ввели фамилию клиента\r\n";
            if (_componentsInOrder.Count == 0) errors += "Вы не выбрали товар\r\n";

            if (errors.Length == 0)
            {
                if (_isEdit == false)
                {
                    var currOrder = new Order()
                    {
                        FirstNameClient = TxtBoxClientFirstName.Text,
                        LastNameClient = TxtBoxClientLastName.Text,
                        UserId = AppData.CurrUser.Id,
                        OrderStatusId = 2,
                        DateOfCreation = DateTime.Now
                    };
                    AppData.Context.Order.Add(currOrder);
                    AppData.Context.SaveChanges();

                    var errorsInCount = "";
                    foreach (var item in _componentsInOrder.ToList())
                    {
                        var newCompOfOrder = new ComponentOfOrder()
                        {
                            ComponentId = item.Id,
                            OrderId = currOrder.Id,
                            Count = item.CountInOrder
                        };

                        var currComponentInContext = AppData.Context.Component.ToList().Where(p => p.Id == item.Id).FirstOrDefault();
                        //Логика на изменение количества в бд
                        if (currComponentInContext != null)
                        {
                            var newCount = currComponentInContext.Count - newCompOfOrder.Count;
                            if (newCount < 0)
                            {
                                errorsInCount += $"Вы превысили количество {item.Name}, доступно на складе: {currComponentInContext.Count}\r\n";
                                break;
                            }
                            currComponentInContext.Count = newCount;
                            AppData.Context.SaveChanges();
                        }
                        AppData.Context.ComponentOfOrder.Add(newCompOfOrder);
                        AppData.Context.SaveChanges();
                    }
                    if (errorsInCount.Length != 0)
                        MessageBox.Show(errorsInCount, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        MessageBox.Show("Вы успешно добавили заказ", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppData.MainFrame.GoBack();
                    }
                }
                else
                {
                    foreach (var item in AppData.Context.ComponentOfOrder.ToList().Where(p => p.OrderId == _currOrder.Id).ToList())
                    {
                        AppData.Context.ComponentOfOrder.Remove(item);
                    }
                    AppData.Context.SaveChanges();

                    _currOrder.FirstNameClient = TxtBoxClientFirstName.Text;
                    _currOrder.LastNameClient = TxtBoxClientLastName.Text;
                    AppData.Context.SaveChanges();

                    var errorsInCount = "";
                    foreach (var item in _componentsInOrder.ToList())
                    {
                        var newCompOfOrder = new ComponentOfOrder()
                        {
                            ComponentId = item.Id,
                            OrderId = _currOrder.Id,
                            Count = item.CountInOrder
                        };

                        var currComponentInContext = AppData.Context.Component.ToList().Where(p => p.Id == item.Id).FirstOrDefault();
                        //Логика на изменение количества в бд
                        if (currComponentInContext != null)
                        {
                            var newCount = currComponentInContext.Count - newCompOfOrder.Count; 
                            if (newCount < 0)
                            {
                                errorsInCount += $"Вы превысили количество {item.Name}, доступно на складе: {currComponentInContext.Count}\r\n";
                                break;
                            }
                            currComponentInContext.Count = newCount;
                            AppData.Context.SaveChanges();
                        }
                        AppData.Context.ComponentOfOrder.Add(newCompOfOrder);
                        AppData.Context.SaveChanges();
                    }
                    if (errorsInCount.Length != 0)
                        MessageBox.Show(errorsInCount, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    { 
                    MessageBox.Show("Вы успешно изменили заказ", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppData.MainFrame.GoBack();
                    }
                }
            }
            else
                MessageBox.Show(errors, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void TxtBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currComponent = (sender as TextBox).DataContext as Component;
            var currTextBox = sender as TextBox;

            if (currTextBox.Text.Length > 0)
            {
                int newCount = 0;
                try { newCount = int.Parse(currTextBox.Text); }
                catch
                {
                    MessageBox.Show("Вы ввели неккоректное значение", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    currTextBox.Text = "0";
                }

                var currCompInList = _componentsInOrder.Where(p => p.Id == currComponent.Id).FirstOrDefault();
                if (newCount > 0)
                {
                    if (currCompInList == null)
                    {
                        currComponent.CountInOrder = newCount;
                        _componentsInOrder.Add(currComponent);
                    }
                    else
                    {
                        _componentsInOrder.Remove(currCompInList);
                        currCompInList.CountInOrder = newCount;
                        _componentsInOrder.Add(currCompInList);
                    }
                }
                else
                {
                    if (currCompInList != null)
                        _componentsInOrder.Remove(currCompInList);
                }

            }
        }
    }
}
