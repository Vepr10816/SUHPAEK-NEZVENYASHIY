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
    /// Логика взаимодействия для LoadingVolunteers.xaml
    /// </summary>
    public partial class LoadingVolunteers : Page
    {
        //Передача переменной, которая хранит в себе информацию предыдущей странице, также позволяет переместиться на следующую страницу
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public LoadingVolunteers(MainWindow MW)
        {
            InitializeComponent();
            //присваивание MainFrame - объект, позволяющий перемещаться между СТРАНИЦАМИ, не окнами
            Mv = MW;
        }

        private void LoadPhotoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLoading_Click(object sender, RoutedEventArgs e)
        {
            //видимость того что работает загрузка волонтеров
            MessageBox.Show("Волонтеры загружены");
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRefresh_Click_1(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new AdministrationMenuPage(Mv));
        }
    }
}
