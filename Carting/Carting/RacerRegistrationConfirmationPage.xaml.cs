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
    /// Логика взаимодействия для RacerRegistrationConfirmationPage.xaml
    /// </summary>
    public partial class RacerRegistrationConfirmationPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        int RacerID = 0;
        public RacerRegistrationConfirmationPage(MainWindow MW,int IDRacer)
        {
            Mv = MW;
            RacerID = IDRacer;
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new RacerMenuPage(Mv, RacerID));
        }
    }
}
