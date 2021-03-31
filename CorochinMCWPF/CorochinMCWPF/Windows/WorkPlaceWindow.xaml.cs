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
using System.Windows.Shapes;

namespace CorochinMCWPF.Windows
{
    /// <summary>
    /// Interaction logic for WorkPlaceWindow.xaml
    /// </summary>
    public partial class WorkPlaceWindow : Window
    {
        public WorkPlaceWindow(WorkPlace currWorpkPlace)
        {
            InitializeComponent();
            this.DataContext = currWorpkPlace;
            DataGrdWorkPlace.ItemsSource = AppData.Context.ComponentOfWorkPlace.ToList().Where(p => p.WorkPlaceId == currWorpkPlace.Id).ToList();
        }
    }
}
