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
    /// Логика взаимодействия для UsersManagment.xaml
    /// </summary>
    public partial class UsersManagment : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        /// <summary>
        /// инициализация окна и связь с бд
        /// </summary>
        /// <param name="MW">переменная навигации</param>
        public UsersManagment(MainWindow MW)
        {
            Mv = MW;
            MW.Back.Visibility = Visibility.Visible;
            InitializeComponent();
            connect.Open();
            cmbFiltrPoRolyam.Items.Add("Все роли");
            cmbFiltrPoRolyam.Items.Add("Administrator");
            cmbFiltrPoRolyam.Items.Add("Coordinator");
            cmbFiltrPoRolyam.Items.Add("Racer");
            cmbFiltrPoRolyam.SelectedIndex = 0;
            cmbOtsortir.Items.Add("Имя");
            cmbOtsortir.Items.Add("Фамилия");
            cmbOtsortir.Items.Add("Email");
            cmbOtsortir.Items.Add("Роль");
            cmbOtsortir.SelectedIndex = 0;
            string sql = $@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', role_name as'Роль' from user join Role on user.ID_role = role.ID_Role";
            MySqlCommand command = new MySqlCommand(sql, connect);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            gridMyResult.ItemsSource = dt.DefaultView;
            CharityOrganisations.Content = gridMyResult.Items.Count.ToString();

            connect.Close();

        }
        /// <summary>
        /// переход 
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)gridMyResult.SelectedItem;
            string em = row[2].ToString();


            //MessageBox.Show(b);
            Mv.MainFrame.NavigationService.Navigate(new EditUsers(Mv,em));
        }
        /// <summary>
        /// обработчик кнопки обновить
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();

            MySqlCommand command = new MySqlCommand("", connect);

            if (cmbOtsortir.SelectedItem.ToString() == "Фамилия")
                command = new MySqlCommand("select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', role_name as'Роль' from user join Role on user.ID_role = role.ID_Role ORDER BY user.Last_name ASC", connect);

            if (cmbOtsortir.SelectedItem.ToString().Contains("Фамилия") && !cmbFiltrPoRolyam.SelectedItem.ToString().Contains("Все роли"))
                command = new MySqlCommand($@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', role_name as'Роль' from user join Role on user.ID_role = role.ID_Role WHERE role_name = '{cmbFiltrPoRolyam.SelectedItem.ToString()}' ORDER BY user.Last_name ASC", connect);

            if (cmbOtsortir.SelectedItem.ToString() == "Имя")
                command = new MySqlCommand($@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', role_name as'Роль' from user join Role on user.ID_role = role.ID_Role ORDER BY user.First_name ASC", connect);

            if (cmbOtsortir.SelectedItem.ToString() == "Имя" && !cmbFiltrPoRolyam.SelectedItem.ToString().Contains("Все роли"))
                command = new MySqlCommand($@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', role_name as'Роль' from user join Role on user.ID_role = role.ID_Role WHERE role_name = '{cmbFiltrPoRolyam.SelectedItem.ToString()}' ORDER BY user.First_name ASC", connect);

            if (cmbOtsortir.SelectedItem.ToString() == "Email")
                command = new MySqlCommand($@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', role_name as'Роль' from user join Role on user.ID_role = role.ID_Role ORDER BY Email ASC", connect);

            if (cmbOtsortir.SelectedItem.ToString() == "Email" && !cmbFiltrPoRolyam.SelectedItem.ToString().Contains("Все роли"))
                command = new MySqlCommand($@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', role_name as'Роль' from user join Role on user.ID_role =
role.ID_Role WHERE role_name = '{cmbFiltrPoRolyam.SelectedItem.ToString()}' ORDER BY Email ASC", connect);

            if (cmbOtsortir.SelectedItem.ToString() == "Роль")
                command = new MySqlCommand($@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', role_name as'Роль' from user join Role on user.ID_role = role.ID_Role ORDER BY role_name ASC", connect);

            if (cmbOtsortir.SelectedItem.ToString() == "Роль" && !cmbFiltrPoRolyam.SelectedItem.ToString().Contains("Все роли"))
                command = new MySqlCommand($@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', role_name as'Роль' from user join Role on user.ID_role = role.ID_Role WHERE role_name = '{cmbFiltrPoRolyam.SelectedItem.ToString()}' ORDER BY role_name ASC", connect);




            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            gridMyResult.ItemsSource = dt.DefaultView;
            CharityOrganisations.Content = gridMyResult.Items.Count.ToString();
            connect.Close();
        }
        /// <summary>
        /// обработчик поиска
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void TBPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            connect.Open();

            MySqlCommand command = new MySqlCommand($@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', role_name as'Роль' from user join Role on user.ID_role = role.ID_Role WHERE user.First_Name LIKE '%" + TBPoisk.Text + "%' OR user.Last_Name LIKE '%" + TBPoisk.Text + "%' OR user.Email LIKE '%" + TBPoisk.Text + "%' ", connect);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            gridMyResult.ItemsSource = dt.DefaultView;
            CharityOrganisations.Content = gridMyResult.Items.Count.ToString();
            connect.Close();
        }
        /// <summary>
        /// обработчик перехода
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new AddNewUser(Mv));
        }
    }
}