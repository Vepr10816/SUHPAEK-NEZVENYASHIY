using Microsoft.Win32;
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
using static Carting.Extensions.ImageExtension;

namespace Carting
{
    /// <summary>
    /// Логика взаимодействия для Sponsor_RacerDataChangePage.xaml
    /// </summary>
    public partial class Sponsor_RacerDataChangePage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        string RacerID = "";
        string idracer = "";
        /// <summary>
        /// обработчик окна и связь с бд
        /// </summary>
        /// <param name="MW">переменная навигации</param>
        /// <param name="em">эмейл</param>
        /// <param name="IDRacer">код гонщика</param>
        public Sponsor_RacerDataChangePage(MainWindow MW, string em,string IDRacer)
        {
            InitializeComponent();
            Mv = MW;
            MW.Back.Visibility = Visibility.Hidden;
            RacerID = IDRacer;
            idracer = IDRacer;
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

            sql = $@"SELECT * FROM Registration_status";
            command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Oplata.Items.Add(dataReader[$@"Statu_Name"].ToString());
            }
            dataReader.Close();


            string Gender = "";
            string Country = "";
            sql = $@"SELECT * FROM Racer where ID_Racer = {IDRacer}";
            command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                FirstNameTB.Text = dataReader[$@"First_Name"].ToString();
                LastNameTB.Text = dataReader[$@"Last_Name"].ToString();
                Gender = dataReader[$@"Gender"].ToString();
                SelectedImage = dataReader[$@"Photo"].ToString().Replace("-", $@"\");
                RunnerPhoto.SetSource(dataReader[$@"Photo"].ToString().Replace("-", $@"\"));
                SelectedImageName = System.IO.Path.GetFileName(dataReader[$@"Photo"].ToString().Replace("-", $@"\"));
                FileNameTB.Text = SelectedImageName;
                BirthDateDP.Text = dataReader[$@"DateOfBirth"].ToString();
                Country = dataReader[$@"ID_Country"].ToString();
                BirthDateDP.Text = dataReader[$@"DateOfBirth"].ToString().Replace(" 0:00:00", "");
            }
            dataReader.Close();
            if (Gender == "M")
                GenderCB.SelectedItem = "Male";
            else
                GenderCB.SelectedItem = "Female";
            command = new MySqlCommand($@"SELECT Country_Name FROM Country where ID_Country = '{Country}'", connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                CountryCB.SelectedItem = dataReader[$@"Country_Name"].ToString();
            }
            dataReader.Close();


            command = new MySqlCommand($@"SELECT * FROM User where First_Name = '{FirstNameTB.Text}' and Last_Name = '{LastNameTB.Text}'", connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Password1PB.Text = dataReader[$@"Password"].ToString();
                Password2PB.Text = dataReader[$@"Password"].ToString();
                EmailTB.Content = dataReader[$@"Email"].ToString();
            }
            dataReader.Close();
            connect.Close();
            string str = BirthDateDP.Text;
            string str1 = BirthDateDP.Text;
            string str2 = BirthDateDP.Text;
            str1 = str1.Substring(6, 4);
            str = str.Substring(0, 2);
            str2 = str2.Substring(2, 4);
            BirthDateDP.Text = str1 + str2 + str;
            BirthDateDP.Text = BirthDateDP.Text.Replace(".", "-");
        }
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
        /// <summary>
        /// название картинки
        /// </summary>
        private string SelectedImageName { get; set; }

        /// <summary>
        /// обработчик кнопки регистрации
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (Password1PB.Text != Password2PB.Text)
                MessageBox.Show("Пароли не совпадают");
            else if (BirthDateDP.Text.Contains("_"))
                MessageBox.Show("Некорректно введена дата!");
            else if (IsValidPassword(Password1PB.Text) == false)
                MessageBox.Show("Пароль должен содержать: Минимум 6 символов, Минимум 1 прописная буква, Минимум 1 цифра, По крайней мере один из следующих символов: ! @ # $ % ^");
            else if (FirstNameTB.Text.Replace(" ", "") == "" || LastNameTB.Text.Replace(" ", "") == "" || BirthDateDP.Text.Replace(" ", "") == "" || FileNameTB.Text.Replace(" ", "") == "" || CountryCB.SelectedIndex == -1 || GenderCB.SelectedIndex == -1 || Password1PB.Text.Replace(" ", "") == "" || Password2PB.Text.Replace(" ", "") == "")
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
                sql = $@"update Racer set First_Name = '{FirstNameTB.Text}', Last_Name = '{LastNameTB.Text}', Gender = '{GenderID}', DateOfBirth = '{BirthDateDP.Text}', ID_Country = '{CountryID}', Photo = '{SelectedImage.Replace($@"\", "-")}' where  ID_Racer = {idracer}";
                command = new MySqlCommand(sql, connect);
                command.ExecuteNonQuery();
                if(Oplata.SelectedIndex == 0)
                    sql = $@"update Registration set ID_Registration_Status = 1 where ID_Racer = {idracer}";
                if (Oplata.SelectedIndex == 1)
                    sql = $@"update Registration set ID_Registration_Status = 2 where ID_Racer = {idracer}";
                if (Oplata.SelectedIndex == 2)
                    sql = $@"update Registration set ID_Registration_Status = 4 where ID_Racer = {idracer}";
                command = new MySqlCommand(sql, connect);
                command.ExecuteNonQuery();

                string idreg = "";
                sql = $@"SELECT * FROM Registration where ID_Racer = '{idracer}'";
                command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    idreg = dataReader[$@"ID_Registration"].ToString();
                }
                dataReader.Close();

                sql = $@"update result set RaceTime = '00:08:07' where ID_Registration = {idreg} and RaceTime = '00:00:00'";
                command = new MySqlCommand(sql, connect);
                command.ExecuteNonQuery();
                /*
                sql = $@"INSERT INTO Racer (`First_Name`, `Last_Name`, `Gender`, `DateOfBirth`, `ID_Country`, `Photo`) VALUES ('{FirstNameTB.Text}', '{LastNameTB.Text}', '{GenderID}', '{BirthDateDP.Text}', '{CountryID}', '{SelectedImage}')";
                command = new MySqlCommand(sql, connect);
                command.ExecuteNonQuery();
                sql = $@"INSERT INTO `user` (`Email`, `Password`, `First_Name`, `Last_Name`, `ID_Role`) VALUES ('{EmailTB.Text}', '{Password1PB.Password}', '{FirstNameTB.Text}', '{LastNameTB.Text}', 'R')";
                command = new MySqlCommand(sql, connect);
                command.ExecuteNonQuery();*/

                Mv.MainFrame.NavigationService.Navigate(new CoordinatorMenuPage(Mv));
            }
        }


        /// <summary>
        /// обработчик кнопки отмена
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new CoordinatorMenuPage(Mv));
        }
        /// <summary>
        /// обработчик кнопки загрузить фото
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void LoadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fd = new OpenFileDialog();
                fd.Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp";//|All files (*.*)|*.*";
                fd.ShowDialog();
                RunnerPhoto.SetSource(fd.FileName);
                SelectedImage = fd.FileName;
                MessageBox.Show(SelectedImage);
                SelectedImageName = System.IO.Path.GetFileName(SelectedImage);
                FileNameTB.Text = SelectedImageName;
            }
            catch (NotSupportedException)
            {
                MessageBox.Show("Ошибка загрузки файла.");
            }
        }
        /// <summary>
        /// проверка на латиницу
        /// </summary>
        /// <param name="c">символ</param>
        /// <returns>true если соответствует, false Если нет</returns>
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
        /// <summary>
        /// проверка на кириллицу
        /// </summary>
        /// <param name="c">символ</param>
        /// <returns>true если соответствует, false Если нет</returns>
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
        /// <summary>
        /// проверка на число
        /// </summary>
        /// <param name="c">символ</param>
        /// <returns>true если соответствует, false Если нет</returns>
        bool Isnum(char c)
        {
            if (c >= '0' && c <= '9')
                return true;
            if (c >= '.' && c <= '.')
                return true;

            return false;
        }
        /// <summary>
        /// проверка на проверка ввода цифр
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void tb1_PreviewTextNum(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !e.Text.All(Isnum);
        }
        /// <summary>
        /// обработчик вставки
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void tb1_PastingNum(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(Isnum))
                e.CancelCommand();
        }

        /// <summary>
        /// проверка ввода кириллицы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsRus);
            //MessageBox.Show(sender.ToString() + " " + e.ToString());
        }
        /// <summary>
        /// обработчик вставки
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void tb1_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsRus))
                e.CancelCommand();
        }
        /// <summary>
        /// проверка ввода латиницы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void tb1_PreviewTextEmail(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsAngl);
        }

        /// <summary>
        /// проверка на формат пароля
        /// </summary>
        /// <param name="pswd">пароль</param>
        /// <returns>true если соответствует, false Если нет</returns>
        public static bool IsValidPassword(string pswd)
        {
            //•	Минимум 6 символов
            //•	Минимум 1 прописная буква
            //•	Минимум 1 цифра
            //•	По крайней мере один из следующих символов: ! @ # $ % ^

            /// <summary>
            /// проверка на регистр
            /// </summary>
            /// <param name="c">символ</param>
            /// <returns>true если соответствует, false Если нет</returns>
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
