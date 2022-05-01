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
        /// <summary>
        /// Инициализация 
        /// </summary>
        /// <param name="MW">переменная передающаяся от MainWindow отвечающая за определение текущего фрейма</param>
        public BecomeRacerPage(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
        }
        //Если не учавстовали тогда переходит на страницу регистрации на гонщика
        private void RunnerRegistration_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new RacerRegistrationPage(Mv));
        }
        //Если учавстовали тогда переходит на страницу авторизации
        private void Authorize_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new AuthorizationPage(Mv));
        }
    }
}
