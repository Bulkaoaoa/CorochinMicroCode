using CorochinMCWPF.Entites;
using CorochinMCWPF.Pages;
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

namespace CorochinMCWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppData.MainFrame = MainFrame;
            AppData.MainFrame.Navigate(new AuthPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (AppData.MainFrame.CanGoBack)
                AppData.MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (AppData.MainFrame.Content as Page is AuthPage)
                BtnBack.Visibility = Visibility.Hidden;
            else
                BtnBack.Visibility = Visibility.Visible;
        }
    }
}
