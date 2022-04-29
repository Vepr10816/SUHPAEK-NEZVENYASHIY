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

namespace Carting
{
    /// <summary>
    /// Логика взаимодействия для AdministrationMenuPage.xaml
    /// </summary>
    public partial class AdministrationMenuPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public AdministrationMenuPage(MainWindow MW)
        {
            InitializeComponent();
            MW.Exit.Visibility = Visibility.Visible;
            MW.Back.Visibility = Visibility.Hidden;
            Mv = MW;
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new UsersManagment(Mv));
        }

        private void Volonter_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new VoluenreersManagment(Mv));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new CharityManagment(Mv));
        }
    }
}
