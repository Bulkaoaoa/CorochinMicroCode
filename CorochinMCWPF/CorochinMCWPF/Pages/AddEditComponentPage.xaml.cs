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
    /// Interaction logic for AddEditComponent.xaml
    /// </summary>
    public partial class AddEditComponent : Page
    {
        private Component _currComponent;
        public AddEditComponent()
        {
            InitializeComponent();
            BtnSave.Content = "Добавить";
        }
        public AddEditComponent(Component currComponent)
        {
            InitializeComponent();
            BtnSave.Content = "Сохранить";
            _currComponent = currComponent;
            this.DataContext = currComponent;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";
            decimal price = 0;
            int count = 0;
            if (string.IsNullOrWhiteSpace(TxtBoxName.Text)) errors += "Вы не ввели название\r\n";
            if (string.IsNullOrWhiteSpace(TxtBoxPrice.Text)) errors += "Вы не ввели цену\r\n";
            if (string.IsNullOrWhiteSpace(TxtBoxCount.Text)) errors += "Вы не ввели количество комплектующих\r\n";
            try { price = decimal.Parse(TxtBoxPrice.Text); } catch { errors += "Вы ввели неккоректное значение цены\r\n"; }
            try { count = int.Parse(TxtBoxCount.Text); } catch { errors += "Вы ввели неккоректное значение количества\r\n"; }
            if (price <= 0) errors += "Вы не можете ввести отрицательное или нулевое значение в цену\r\n";
            if (count <= 0) errors += "Вы не можете ввести отрицательное или нулевое значение в количество\r\n";

            if (errors.Length == 0)
            {
                if (_currComponent == null)
                {
                    var newComp = new Component()
                    {
                        Name = TxtBoxName.Text,
                        Price = price,
                        Description = TxtBoxDescription.Text == "" ? null : TxtBoxDescription.Text,
                        Count = count,
                        IsArchived = false,
                    };
                    AppData.Context.Component.Add(newComp);
                    AppData.Context.SaveChanges();
                    MessageBox.Show("Вы успешно добавили компонент", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _currComponent.Name = TxtBoxName.Text;
                    _currComponent.Price = price;
                    _currComponent.Description = TxtBoxDescription.Text == "" ? null : TxtBoxDescription.Text;
                    _currComponent.Count = count;
                    _currComponent.IsArchived = false;
                    AppData.Context.SaveChanges();
                    MessageBox.Show("Вы успешно обновлили компонент", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                    AppData.MainFrame.GoBack();
            }
            else
                MessageBox.Show(errors, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
