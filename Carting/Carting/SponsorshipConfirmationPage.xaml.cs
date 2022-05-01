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
    /// Логика взаимодействия для SponsorshipConfirmationPage.xaml
    /// </summary>
    public partial class SponsorshipConfirmationPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public SponsorshipConfirmationPage(MainWindow MW, string racer, string Charity, string sum)
        {
            InitializeComponent();
            Mv = MW;
            RunnerInfoLabel.Content = racer;
            CharityNameLabel.Content = Charity;
            SumLabel.Content = sum;
            Mv.Back.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new MainMenuPage(Mv));
        }
    }
}
