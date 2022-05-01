using System;
using System.Collections.Generic;
using System.Globalization;
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
using Carting.Extensions;
using MySql.Data.MySqlClient;

namespace Carting
{
    /// <summary>
    /// Логика взаимодействия для EventRegisterPage.xaml
    /// </summary>
    public partial class EventRegisterPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);

        private double allSum;
        //определяет сколько пожертвований сделал гонщик
        private double AllSum
        {
            get => allSum;
            set
            {
                allSum = value;
                if (AllSumLabel != null)
                    AllSumLabel.Content = $"${allSum}";
            }
        }

        List<string> IDCharity = new List<string>();
        int RacerID = 0;
        //Инициализицая, определение видимости кнопок, заполнение ComboBox и листа
        public EventRegisterPage(MainWindow MW, int IDRacer)
        {
            Mv = MW;
            Mv.Exit.Visibility = Visibility.Visible;
            MW.Back.Visibility = Visibility.Hidden;
            InitializeComponent();
            connect.Open();
            MySqlDataReader dataReader = null;
            string sql = $@"SELECT * FROM Charity";
            MySqlCommand command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                CharityCB.Items.Add(dataReader[$@"Charity_Name"].ToString());
                IDCharity.Add(dataReader[$@"ID_Сharity"].ToString());
            }
            dataReader.Close();
            connect.Close();
            RacerID = IDRacer;
        }
        //метод, который позволяет занести пожертвования, которые введет гонщик в общую сумму пожертвований организации
        private void UpdateAllSum()
        {
            var sum = 0d;

            EventTypeCBs?.Children
                .OfType<CheckBox>()
                .ToList()
                .ForEach(x =>
                {
                    if (x.IsChecked == true)
                        sum += x.Tag.ToDouble();
                });

            ComplectsRBs?.Children
                .OfType<RadioButton>()
                .ToList()
                .ForEach(x =>
                {
                    if (x.IsChecked == true)
                        sum += x.Tag.ToDouble();
                });

            // sum += SumTB.Text.ToDouble();

            AllSum = sum;
            AllSumLabel.Content = "$" + AllSum.ToString();
        }
        //обновление общий суммы пожертвований
        private void CheckBox_CheckedUnchecked(object sender, RoutedEventArgs e) => UpdateAllSum();

        //осуществляет регистрацию гонщика, а также проверку на заполненность полей и сумму взноса, после чего вносит даннные в БД, задавая участнику 
        //случайный номер и определяя его ID
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            //if(!OptionA.IsPressed || !OptionB.IsPressed || !OptionC.IsPressed || One.IsChecked == false || Two.IsChecked ==true || Three.IsChecked == true)
            var b1 = EventTypeCBs.Children.OfType<CheckBox>()
                .ToList()
                .Any(x => x.IsChecked == true);
            //CharityCB
            var b2 = CharityCB.SelectedItem != null;
            //ComplectsRBs
            var b3 = ComplectsRBs.Children.OfType<RadioButton>()
                .ToList()
                .Any(x => x.IsChecked == true);

            if (!(b1 && b2 && b3))
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else if (Convert.ToInt32(SumTB.Text) < 500)
                MessageBox.Show("Минимальная Сумма взноса 500 $");
            else
            {
                DateTime now = DateTime.Now;
                connect.Open();
                string sql = $@"INSERT INTO `registration` (`ID_Racer`, `Registration_Date`, `ID_Registration_Status`, `Cost`, `ID_Charity`, `SponsorshipTarget`) VALUES ({RacerID}, '{now.ToString("yyyy-MM-dd")}', 1, '{AllSum.ToString()}', {IDCharity[CharityCB.SelectedIndex]}, '{SumTB.Text}')";
                MySqlCommand command = new MySqlCommand(sql, connect);
                command.ExecuteReader();
                connect.Close();
                connect.Open();
                int RegistrationID = 0;
                MySqlDataReader dataReader = null;
                sql = $@"SELECT * FROM Registration";
                command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    RegistrationID = Convert.ToInt32(dataReader[$@"ID_Registration"].ToString());
                }
                dataReader.Close();
                connect.Close();
                //Создание объекта для генерации чисел
                Random rnd = new Random();

                //Получить случайное число (в диапазоне от 0 до 10)
                int value = rnd.Next(0, 700);
                if (One.IsChecked == true)
                {
                    connect.Open();
                    sql = $@"INSERT INTO `result` (`ID_Registration`, `ID_Event`, `BidNumber`, `RaceTime`) VALUES ({RegistrationID}, 1, {value}, '00:00:00')";
                    command = new MySqlCommand(sql, connect);
                    command.ExecuteReader();
                    connect.Close();
                }
                if (Two.IsChecked == true)
                {
                    connect.Open();
                    sql = $@"INSERT INTO `result` (`ID_Registration`, `ID_Event`, `BidNumber`, `RaceTime`) VALUES ({RegistrationID}, 4, {value}, '00:00:00')";
                    command = new MySqlCommand(sql, connect);
                    command.ExecuteReader();
                    connect.Close();
                }
                if (Three.IsChecked == true)
                {
                    connect.Open();
                    sql = $@"INSERT INTO `result` (`ID_Registration`, `ID_Event`, `BidNumber`, `RaceTime`) VALUES ({RegistrationID}, 2, {value}, '00:00:00')";
                    command = new MySqlCommand(sql, connect);
                    command.ExecuteReader();
                    connect.Close();
                }
                Mv.MainFrame.NavigationService.Navigate(new RacerRegistrationConfirmationPage(Mv, RacerID));
            }
        }

        //переход на страницу меню гонщика
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new RacerMenuPage(Mv, RacerID));
        }
        // запрещает вводить символы кроме тех, что указаны в методе Isnum
        private void tb1_PreviewTextNum(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !e.Text.All(Isnum);
        }
        //запрещает вставлять текст
        private void tb1_PastingNum(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(Isnum))
                e.CancelCommand();
        }
        //осуществляет проверку на цифры и точку
        bool Isnum(char c)
        {
            if (c >= '0' && c <= '9')
                return true;
            if (c >= '.' && c <= '.')
                return true;

            return false;
        }
    }
}

