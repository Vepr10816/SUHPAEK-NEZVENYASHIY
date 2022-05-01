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
    /// Логика взаимодействия для InteractivMap.xaml
    /// </summary>
    public partial class InteractivMap : Page
    {
        //Передача переменной, которая хранит в себе информацию предыдущей странице, также позволяет переместиться на следующую страницу
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public InteractivMap(MainWindow MW)
        {
            InitializeComponent();
            //присваивание MainFrame
            Mv = MW;
        }

        private void BtnMap_Click(object sender, RoutedEventArgs e)
        {
            //открытие новой страницы
            Mv.MainFrame.NavigationService.Navigate(new TrackMap());
        }
    }
}
