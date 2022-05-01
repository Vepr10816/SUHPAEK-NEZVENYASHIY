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
    /// Логика взаимодействия для RacerMenuPage.xaml
    /// </summary>
    public partial class RacerMenuPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        int RacerID = 0;
        public RacerMenuPage(MainWindow MW, int IDRacer)
        {
            InitializeComponent();
            MW.Back.Visibility = Visibility.Hidden;
            MW.Exit.Visibility = Visibility.Visible;
            Mv = MW;
            RacerID = IDRacer;
        }

        private void ContactsButton_Click(object sender, RoutedEventArgs e)
        {
            ContactInfoWindow contactInfoWindow = new ContactInfoWindow();
            contactInfoWindow.Show();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new EventRegisterPage(Mv, RacerID));
        }

        private void Results_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new MyResults(Mv, RacerID));
            
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new RacerDataChangePage(Mv, RacerID));
        }

        private void Sponsor_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new MySponsor(Mv, RacerID));
        }
    }
}
