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
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUsers : Page
    {
        string GenderID;
        string BirthDateDP;
        string CountryID;
        string Photo;
        int idracer;
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public EditUsers(MainWindow MW,string em)
        {
            Mv = MW;
            MW.Back.Visibility = Visibility.Hidden;
            string em1 = em;
            InitializeComponent();
            connect.Open();
            cmbPoisk.Items.Add("Administrator");
            cmbPoisk.Items.Add("Coordinator");
            cmbPoisk.Items.Add("Racer");
            MySqlDataReader dataReader = null;
            string sql = $@"select user.First_Name, user.Last_Name, user.Email, Password, role_name from user join Role on user.ID_role = role.ID_Role WHERE user.Email ='{em1}'";
            // string sql1 = $@"SELECT select user.First_Name from user WHERE user.Email ='{em1}' ";
            MySqlCommand command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                cmbEmail.Text = dataReader[$@"Email"].ToString();
                cmbFiltrPoRolyam.Text = dataReader[$@"First_Name"].ToString();
                cmbOtsortir.Text = dataReader[$@"Last_Name"].ToString();
                pas.Text = dataReader[$@"Password"].ToString();
                pas2.Text = dataReader[$@"Password"].ToString();
                cmbPoisk.SelectedItem = dataReader[$@"Role_Name"].ToString();
            }
            dataReader.Close();
            string sql1 = $@"SELECT * from Racer where racer.First_Name ='{cmbFiltrPoRolyam.Text}' and racer.Last_Name ='{cmbOtsortir.Text}'";
            command = new MySqlCommand(sql1, connect);
            //command.ExecuteScalar
            // MessageBox.Show(command.ExecuteScalar().ToString());
            dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                GenderID = dataReader[$@"Gender"].ToString();
                BirthDateDP = dataReader[$@"DateOfBirth"].ToString();
                CountryID = dataReader[$@"ID_Country"].ToString();
                Photo = dataReader[$@"Photo"].ToString();
                idracer = Convert.ToInt32(dataReader[$@"ID_racer"]);
            }
            dataReader.Close();

            connect.Close();

        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            string ab = pas.Text;
            string ba = pas2.Text;
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
            string str = BirthDateDP;
            string str1 = BirthDateDP;
            string str2 = BirthDateDP;
            str1 = str1.Substring(6, 4);
            str = str.Substring(0, 2);
            str2 = str2.Substring(2, 4);
            BirthDateDP = str1 + str2 + str;
            string sql = $@"update User set First_Name = '{cmbFiltrPoRolyam.Text}', Last_Name = '{cmbOtsortir.Text}', Password = '{pas2.Text}', ID_role ='{cmbPoisk.SelectedItem.ToString()}' where Email = '{cmbEmail.Text}'";
            string sql1 = $@" update Racer set First_Name = '{cmbFiltrPoRolyam.Text}', Last_Name = '{cmbOtsortir.Text}', Gender = '{GenderID}', DateOfBirth = '{BirthDateDP}', ID_Country = '{CountryID}', Photo = '{Photo}' where ID_Racer = { idracer }";
            MySqlCommand command = new MySqlCommand("", connect);
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

        private void pas_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new AdministrationMenuPage(Mv));
        }
    }
}