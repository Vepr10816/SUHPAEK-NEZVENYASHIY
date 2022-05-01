using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для VolunteersManagement.xaml
    /// </summary>
    public partial class VoluenreersManagment : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        /// <summary>
        /// инициализация окна и связь с бд
        /// </summary>
        /// <param name="MW">переменная перехода</param>
        public VoluenreersManagment(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
            MW.Back.Visibility = Visibility.Visible;
            connect.Open();
            Filtration.Items.Add("Фамилия");
            Filtration.Items.Add("Имя");
            Filtration.Items.Add("Страна");
            Filtration.Items.Add("Пол");
            string sql = $@"select Last_Name as'Фамилия', First_Name as 'Имя', country_name as'Страна', gender_name as'Пол' from volunteer join Gender on Gender_ID = ID_Gender join country on volunteer.ID_Country = country.ID_country";
            MySqlCommand command = new MySqlCommand(sql, connect);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            gridMyResult.ItemsSource = dt.DefaultView;



            connect.Close();
        }
        /// <summary>
        /// обработчик кнопки фильтрация
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void btnFiltration_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();
            MySqlCommand command = new MySqlCommand("", connect);
            if (Filtration.Text == "")
            {
                MessageBox.Show("Веберите параметр для сортировки");
                connect.Close();
                return;
            }
            if (Filtration.SelectedItem.ToString().Contains("Фамилия"))
            {
                command = new MySqlCommand("select Last_Name as'Фамилия', First_Name as 'Имя', country_name as'Страна', gender_name as'Пол' from volunteer join Gender on Gender_ID = ID_Gender join country on volunteer.ID_Country = country.ID_country ORDER BY Last_name ASC", connect);
            }
            if (Filtration.SelectedItem.ToString() == "Имя")
            {
                command = new MySqlCommand("select Last_Name as'Фамилия', First_Name as 'Имя', country_name as'Страна', gender_name as'Пол' from volunteer join Gender on Gender_ID = ID_Gender join country on volunteer.ID_Country = country.ID_country ORDER BY First_name ASC", connect);
            }
            if (Filtration.SelectedItem.ToString() == "Страна")
            {
                command = new MySqlCommand("select Last_Name as'Фамилия', First_Name as 'Имя', country_name as'Страна', gender_name as'Пол' from volunteer join Gender on Gender_ID = ID_Gender join country on volunteer.ID_Country = country.ID_country ORDER BY volunteer.ID_country ASC", connect);
            }
            if (Filtration.SelectedItem.ToString() == "Пол")
            {
                command = new MySqlCommand("select Last_Name as'Фамилия', First_Name as 'Имя', country_name as'Страна', gender_name as'Пол' from volunteer join Gender on Gender_ID = ID_Gender join country on volunteer.ID_Country = country.ID_country ORDER BY volunteer.Gender_ID ASC", connect);
            }

            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            gridMyResult.ItemsSource = dt.DefaultView;
            connect.Close();

        }
        /// <summary>
        /// обработчик перехода
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void btnZagruzka_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new LoadingVolunteers(Mv));
        }

    }
}