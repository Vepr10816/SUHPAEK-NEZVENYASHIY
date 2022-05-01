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
    /// Логика взаимодействия для AddNewUser.xaml
    /// </summary>
    public partial class AddNewUser : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        public AddNewUser(MainWindow MW)
        {
            InitializeComponent();
            cmbPoisk.Items.Add("Administrator");
            cmbPoisk.Items.Add("Coordinator");
            cmbPoisk.Items.Add("Racer");
            cmbPoisk.SelectedIndex = 0;
            Mv = MW;
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            string em = cmbEmail.Text;
            string ab = pas.Text;
            string ba = pas2.Text;
            IsValidEmail(em);



            if (ab != ba)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            //IsValidPassword(ab);
            if (IsValidPassword(ab) != true)
            {
                MessageBox.Show("Введите другой пароль");
                return;
            }

            connect.Open();
            MySqlCommand command = new MySqlCommand("", connect);
            command = new MySqlCommand($"select count(*) from user where Email = '{cmbEmail.Text}'", connect);
            if (command.ExecuteScalar().ToString() == "1")
            {
                MessageBox.Show("Такой Email уже существует");
                return;
            }
            string sql = $@"insert into user values ('{cmbEmail.Text}', '{cmbFiltrPoRolyam.Text}', '{cmbOtsortir.Text}', '{pas2.Text}', '{cmbPoisk.SelectedItem.ToString()}')";
            string sql1 = $@"insert into Racer values('{cmbFiltrPoRolyam.Text}','{cmbOtsortir.Text}','M','2000-01-01','RUS','')";

            if (cmbPoisk.SelectedItem.ToString() == "Racer")
            {
                command = new MySqlCommand(sql, connect);
                command.ExecuteNonQuery();
                command = new MySqlCommand(sql1, connect);
                command.ExecuteNonQuery();
            }
            else
            {
                command = new MySqlCommand(sql, connect);
                command.ExecuteNonQuery();
            }
            connect.Close();
            Mv.MainFrame.NavigationService.Navigate(new AdministrationMenuPage(Mv));
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                MessageBox.Show("Некорректный Email!");
                return false;
            }

        }


        bool IsRus(char c)
        {

            if (c >= 'а' && c <= 'я')
                return true;
            if (c >= 'А' && c <= 'Я')
                return true;
            if (c >= 'a' && c <= 'z')
                return true;
            if (c >= 'A' && c <= 'Z')
                return true;
            return false;
        }

        private void cmbFiltrPoRolyam_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsRus);
        }

        private void cmbOtsortir_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsRus);
        }

        private void cmbFiltrPoRolyam_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsRus))
                e.CancelCommand();
        }

        private void cmbOtsortir_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsRus))
                e.CancelCommand();
        }
        public static bool IsValidPassword(string pswd)
        {
            //• Минимум 6 символов
            //• Минимум 1 прописная буква
            //• Минимум 1 цифра
            //• По крайней мере один из следующих символов: ! @ # $ % ^

            bool IsSmallLetter(char c)
            {
                if (Char.IsLetter(c))
                {
                    return c.ToString().ToLower() == c.ToString();
                }
                else return false;
            }

            bool b1 = pswd.Length >= 6;

            bool b2 = false;
            foreach (char c in pswd)
            {
                if (IsSmallLetter(c))
                {
                    b2 = true;
                    break;
                }
            }

            bool b3 = false;
            foreach (char c in pswd)
            {
                if (Char.IsDigit(c))
                {
                    b3 = true;
                    break;
                }
            }

            bool b4 = false;
            foreach (char c in pswd)
            {
                if (c == '!' || c == '@' || c == '#' || c == '$' || c == '%' || c == '^')
                {
                    b4 = true;
                    break;
                }
            }

            return b1 && b2 && b3 && b4;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new AdministrationMenuPage(Mv));
        }
    }
}

