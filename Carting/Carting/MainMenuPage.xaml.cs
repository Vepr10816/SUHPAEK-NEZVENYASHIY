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
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public MainMenuPage(MainWindow Mw)
        {
            InitializeComponent();

            Mv = Mw;
        }

        private void Race_reg(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new BecomeRacerPage(Mv));
            Mv.Back.Visibility = Visibility.Visible;
        }

        private void Spons_reg(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new RacerSponsorPage(Mv));
        }

        private void Info (object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new EventInfoMenuPage(Mv));
        }

        private void Sing_In(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new AuthorizationPage(Mv));
        }

       
    }
}
