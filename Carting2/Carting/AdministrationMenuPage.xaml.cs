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
        /// <summary>
        /// Инициализация определение видимости кнопок
        /// </summary>
        /// <param name="MW">переменная передающаяся от MainWindow отвечающая за определение текущего фрейма</param>
        public AdministrationMenuPage(MainWindow MW)
        {
            InitializeComponent();
            MW.Exit.Visibility = Visibility.Visible;
            MW.Back.Visibility = Visibility.Hidden;
            Mv = MW;
        }
        //Осуществляет переход на страницу управления пользователями
        private void Users_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new UsersManagment(Mv));
        }
        //Осуществляет переход на страницу управления волонтерами
        private void Volonter_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new VoluenreersManagment(Mv));
        }
        //Осуществляет переход на страницу управления благотворительным организациями
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new CharityManagment(Mv));
        }
    }
}
