using MySql.Data.MySqlClient;
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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=kartskills;password=;");
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public AuthorizationPage(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new MainMenuPage(Mv));
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();
            MySqlCommand command = new MySqlCommand($"select count(*) from user where Email = '{EmailTB.Text}' and Password = '{PasswordPB.Password.ToString()}'", connect);
            if (command.ExecuteScalar().ToString() == "1")
            {
                command = new MySqlCommand($"select ID_Role from user where Email ='{EmailTB.Text}' and Password = '{PasswordPB.Password.ToString()}'", connect);
                string role = command.ExecuteScalar().ToString();
                if (role.Contains("R"))
                {
                    string Fn = "";
                    string Ln = "";
                    int IDRacer = 0;
                    command = new MySqlCommand($"select * from user where Email ='{EmailTB.Text}' and Password = '{PasswordPB.Password.ToString()}'", connect);
                    MySqlDataReader dataReader = null;
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Fn = dataReader[$@"First_Name"].ToString();
                        Ln = dataReader[$@"Last_Name"].ToString();
                    }
                    dataReader.Close();
                    command = new MySqlCommand($"select * from Racer where First_Name ='{Fn}' and Last_Name = '{Ln}'", connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        IDRacer = Convert.ToInt32(dataReader[$@"ID_Racer"].ToString());
                    }
                    dataReader.Close();
                    Mv.MainFrame.NavigationService.Navigate(new RacerMenuPage(Mv,IDRacer));
                    connect.Close();
                }
                if (role.Contains("A"))
                {
                    Mv.MainFrame.NavigationService.Navigate(new AdministrationMenuPage(Mv));
                    connect.Close();
                }
                if (role.Contains("C"))
                {
                    Mv.MainFrame.NavigationService.Navigate(new CoordinatorMenuPage(Mv));
                    connect.Close();
                }
                connect.Close();
            }
            else
            {
                MessageBox.Show("Проверьте введенные данные");
                connect.Close();
            }
            

        }
    }
}
