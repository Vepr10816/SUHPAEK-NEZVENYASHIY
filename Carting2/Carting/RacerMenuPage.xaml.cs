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
        /// <summary>
        /// управление навигацией
        /// </summary>
        /// <param name="MW">переменная для навигации</param>
        /// <param name="IDRacer">код гонщика</param>
        public RacerMenuPage(MainWindow MW, int IDRacer)
        {
            InitializeComponent();
            MW.Back.Visibility = Visibility.Hidden;
            MW.Exit.Visibility = Visibility.Visible;
            Mv = MW;
            RacerID = IDRacer;
        }
        /// <summary>
        /// обработчик кнопки контакты
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void ContactsButton_Click(object sender, RoutedEventArgs e)
        {
            ContactInfoWindow contactInfoWindow = new ContactInfoWindow();
            contactInfoWindow.Show();
        }
        /// <summary>
        /// обработчик кнопки регистрация
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new EventRegisterPage(Mv, RacerID));
        }
        /// <summary>
        /// обработчик кнопки результаты
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void Results_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new MyResults(Mv, RacerID));
            
        }
        /// <summary>
        /// Обработчик кнопки редактирование профлия
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void Red_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new RacerDataChangePage(Mv, RacerID));
        }
        /// <summary>
        /// Обработчик кнопки спонсоры
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void Sponsor_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new MySponsor(Mv, RacerID));
        }
    }
}
