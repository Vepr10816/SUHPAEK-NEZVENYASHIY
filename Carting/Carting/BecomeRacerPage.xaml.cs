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
    /// Логика взаимодействия для BecomeRacerPage.xaml
    /// </summary>
    public partial class BecomeRacerPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public BecomeRacerPage(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
        }

        private void RunnerRegistration_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new RacerRegistrationPage(Mv));
        }

        private void Authorize_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new AuthorizationPage(Mv));
        }
    }
}
