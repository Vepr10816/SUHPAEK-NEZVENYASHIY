using Microsoft.Win32;
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
using static Carting.Extensions.ImageExtension;

namespace Carting
{
    /// <summary>
    /// Логика взаимодействия для RacerRegistrationPage.xaml
    /// </summary>
    public partial class RacerRegistrationPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);

        private string selectedImage;
        private string SelectedImage
        {
            get => selectedImage;
            set
            {
                selectedImage = value;
                if (selectedImage != null)
                {
                    DefaultImage.Visibility = Visibility.Collapsed;
                    RunnerPhoto.Visibility = Visibility.Visible;
                }
            }
        }
        private string SelectedImageName { get; set; }
        public RacerRegistrationPage(MainWindow MW)
        {
            InitializeComponent();
            MW.Back.Visibility = Visibility.Hidden;
            Mv = MW;
            connect.Open();
            MySqlDataReader dataReader = null;
            string sql = $@"SELECT * FROM Gender";
            MySqlCommand command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                GenderCB.Items.Add(dataReader[$@"Gender_Name"].ToString());
            }
            dataReader.Close();


            sql = $@"SELECT * FROM Country";
            command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                CountryCB.Items.Add(dataReader[$@"Country_Name"].ToString());
            }
            dataReader.Close();
            connect.Close();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidEmail(EmailTB.Text) == false)
                MessageBox.Show("Некорректно введён Email");
            else if (Password1PB.Password != Password2PB.Password)
                MessageBox.Show("Пароли не совпадают");
            else if (BirthDateDP.Text.Contains("_"))
                MessageBox.Show("Некорректно введена дата!");
            else if (IsValidPassword(Password1PB.Password) == false)
                MessageBox.Show("Пароль должен содержать: Минимум 6 символов, Минимум 1 прописная буква, Минимум 1 цифра, По крайней мере один из следующих символов: ! @ # $ % ^");
            else if (FirstNameTB.Text.Replace(" ", "") == "" || LastNameTB.Text.Replace(" ", "") == "" || BirthDateDP.Text.Replace(" ", "") == "" || FileNameTB.Text.Replace(" ", "") == "" || CountryCB.SelectedIndex == -1 || GenderCB.SelectedIndex == -1 || Password1PB.Password.Replace(" ", "") == "" || Password2PB.Password.Replace(" ", "") == "")
                MessageBox.Show("Все поля должны быть заполнены! Все значения должны быть выбраны!");
            else
            {
                string GenderID = "";
                string CountryID = "";
                connect.Open();
                MySqlDataReader dataReader = null;
                string sql = $@"SELECT * FROM Gender where Gender_Name = '{GenderCB.SelectedItem.ToString()}'";
                MySqlCommand command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    GenderID = dataReader[$@"ID_Gender"].ToString();
                }
                dataReader.Close();

                sql = $@"SELECT * FROM Country where Country_Name = '{CountryCB.SelectedItem.ToString()}'";
                command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    CountryID = dataReader[$@"ID_Country"].ToString();
                }
                dataReader.Close();

                sql = $@"select count(*) from User where Email = '{EmailTB.Text}'";
                command = new MySqlCommand(sql, connect);
                if (command.ExecuteScalar().ToString() == "1")
                    MessageBox.Show("Такой Email уже зарегестрирован!");
                else
                {
                    sql = $@"INSERT INTO Racer (`First_Name`, `Last_Name`, `Gender`, `DateOfBirth`, `ID_Country`, `Photo`) VALUES ('{FirstNameTB.Text}', '{LastNameTB.Text}', '{GenderID}', '{BirthDateDP.Text}', '{CountryID}', '{SelectedImage.Replace($@"\", "-")}')";
                    command = new MySqlCommand(sql, connect);
                    command.ExecuteNonQuery();
                    sql = $@"INSERT INTO `user` (`Email`, `Password`, `First_Name`, `Last_Name`, `ID_Role`) VALUES ('{EmailTB.Text}', '{Password1PB.Password}', '{FirstNameTB.Text}', '{LastNameTB.Text}', 'R')";
                    command = new MySqlCommand(sql, connect);
                    command.ExecuteNonQuery();
                }

                int IDRacer = 0;
                sql = $@"SELECT * FROM Racer";
                command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    IDRacer = Convert.ToInt32(dataReader[$@"ID_Racer"].ToString());
                }
                dataReader.Close();
                connect.Close();
                Mv.MainFrame.NavigationService.Navigate(new EventRegisterPage(Mv,IDRacer));
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new MainMenuPage(Mv));
        }

        private void LoadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fd = new OpenFileDialog();
                fd.Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp";//|All files (*.*)|*.*";
                fd.ShowDialog();
                RunnerPhoto.SetSource(fd.FileName);
                SelectedImage = fd.FileName;
                //MessageBox.Show(SelectedImage);
                SelectedImageName = System.IO.Path.GetFileName(SelectedImage);
                FileNameTB.Text = SelectedImageName;
                /*MessageBox.Show(SelectedImage.Replace($@"\", "-"));*/
            }
            catch (NotSupportedException)
            {
                MessageBox.Show("Ошибка загрузки файла.");
            }
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

        bool IsAngl(char c)
        {
            if (c >= '0' && c <= '9')
                return true;
            if (c >= 'a' && c <= 'z')
                return true;
            if (c >= 'A' && c <= 'Z')
                return true;
            if (c >= '!' && c <= '=')
                return true;
            if (c >= '_' && c <= '_')
                return true;
            if (c >= '@' && c <= '@')
                return true;
            return false;
        }

        bool IsRus(char c)
        {
            //if (c >= '0' && c <= '9')
            // return true;
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
        bool Isnum(char c)
        {
            if (c >= '0' && c <= '9')
                return true;
            if (c >= '.' && c <= '.')
                return true;

            return false;
        }

        private void tb1_PreviewTextNum(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !e.Text.All(Isnum);
        }

        private void tb1_PastingNum(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(Isnum))
                e.CancelCommand();
        }


        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsRus);
            //MessageBox.Show(sender.ToString() + " " + e.ToString());
        }

        private void tb1_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsRus))
                e.CancelCommand();
        }

        private void tb1_PreviewTextEmail(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsAngl);
        }

        private void Email_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsAngl))
                e.CancelCommand();
        }

        public static bool IsValidPassword(string pswd)
        {
            //•	Минимум 6 символов
            //•	Минимум 1 прописная буква
            //•	Минимум 1 цифра
            //•	По крайней мере один из следующих символов: ! @ # $ % ^

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
    }
}
