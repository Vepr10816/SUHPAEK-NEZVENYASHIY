﻿using System;
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
    /// Логика взаимодействия для CoordinatorMenuPage.xaml
    /// </summary>
    public partial class CoordinatorMenuPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        //Инициализация, определение видимости кнопки Выход
        public CoordinatorMenuPage(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
            MW.Exit.Visibility = Visibility;
        }
        //переход на страницу управления гонщиками
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new RacerControl(Mv));
        }
        //переход на страницу просмотра спонсоров
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new ViewSponsors(Mv));
        }
    }
}
