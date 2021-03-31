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
    /// Interaction logic for ListOfWorkPlacePage.xaml
    /// </summary>
    public partial class ListOfWorkPlacePage : Page
    {
        public ListOfWorkPlacePage()
        {
            InitializeComponent();
            DataGrdWorkPlaces.ItemsSource = AppData.Context.WorkPlace.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrdWorkPlaces.ItemsSource = AppData.Context.WorkPlace.ToList();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataGrdWorkPlaces.ItemsSource = AppData.Context.WorkPlace.ToList().Where(p => p.Name.ToLower().Trim().Contains(TxtBoxSearch.Text.ToLower().Trim())).ToList();
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            AppData.Context.WorkPlace.Remove((sender as Button).DataContext as WorkPlace);
            AppData.Context.SaveChanges();
        }

        private void DataGrdWorkPlaces_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGrdWorkPlaces.SelectedItem != null)
            {
                var currItem = DataGrdWorkPlaces.SelectedItem as WorkPlace;
                Windows.WorkPlaceWindow workPlaceWindow = new Windows.WorkPlaceWindow(currItem);
                workPlaceWindow.Owner = App.Current.MainWindow;
                workPlaceWindow.ShowDialog();
            }
        }
    }
}
