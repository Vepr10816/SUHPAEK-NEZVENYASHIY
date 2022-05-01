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
    /// Логика взаимодействия для EventInfoMenuPage.xaml
    /// </summary>
    public partial class EventInfoMenuPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        //Инициализация, определение видимости кнопки Назад
        public EventInfoMenuPage(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
            MW.Back.Visibility = Visibility.Visible;
        }
        //переход на страницу c картой
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new InteractivMap(Mv));
        }
        //переход на страницу cо спонсорами
        private void CharityListButton_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new CharityListPage(Mv));
        }
        //переход на страницу c прошлыми результатами гонок
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new PastRaceResults(Mv,0));
        }
    }
}
