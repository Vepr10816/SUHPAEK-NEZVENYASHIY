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
    /// Логика взаимодействия для RacerSponsorPage.xaml
    /// </summary>
    public partial class RacerSponsorPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        List<string> IDRacer = new List<string>();
        List<string> IDRegistration = new List<string>();
        List<string> IDCountry = new List<string>();
        List<string> Racer = new List<string>();
        List<string> BidNumber = new List<string>();
        List<string> Country = new List<string>();
        /// <summary>
        /// инициализация окна и связь с бд
        /// </summary>
        /// <param name="MW">переменная навигации</param>
        public RacerSponsorPage(MainWindow MW)
        {
            Mv = MW;
            Mv.Back.Visibility = Visibility.Hidden;
            InitializeComponent();
            connect.Open();
            MySqlDataReader dataReader = null;
            string sql = $@"SELECT * FROM Registration";
            MySqlCommand command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                IDRegistration.Add(dataReader[$@"ID_Registration"].ToString());
                IDRacer.Add(dataReader[$@"ID_Racer"].ToString());
            }
            dataReader.Close();
            foreach (var person in IDRacer)
            {
                sql = $@"SELECT concat(First_Name, ' ',Last_Name) FROM Racer where ID_Racer = {person}";
                command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Racer.Add(dataReader[$@"concat(First_Name, ' ',Last_Name)"].ToString());
                }
                dataReader.Close();

                sql = $@"SELECT * FROM Racer where ID_Racer = {person}";
                command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    IDCountry.Add(dataReader[$@"ID_Country"].ToString());
                }
                dataReader.Close();

            }
            foreach (var person in IDRegistration)
            {
                sql = $@"SELECT * FROM result where ID_Registration = {person}";
                command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    BidNumber.Add(dataReader[$@"BidNumber"].ToString());
                }
                dataReader.Close();
            }
            foreach (var person in IDCountry)
            {
                sql = $@"SELECT * FROM Country where ID_Country = '{person}'";
                command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Country.Add(dataReader[$@"Country_Name"].ToString());
                }
                dataReader.Close();
            }
            connect.Close();
            for (int i = 0; i < Racer.Count; i++)
            {
                try
                {
                    RunnersCB.Items.Add(Racer[i] + "- " + BidNumber[i] + " (" + Country[i] + ")");
                }
                catch
                {
                    IDRegistration.RemoveAt(i);
                    IDRacer.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// Обработчик кнопки отмена
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new MainMenuPage(Mv));
            
        }
        /// <summary>
        /// Обработчик кнопки заплатить
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            if (CardNumberTB.Text.Contains("_"))
                MessageBox.Show("Некорректный Номер Карты!");

            else if (SponsorNameTB.Text.Replace(" ", "") != "" || RunnersCB.SelectedIndex != -1 || CardOwnerTB.Text.Replace(" ", "") != "" || CardMonthTB.Text.Replace(" ", "") != "" || CardYearTB.Text.Replace(" ", "") != "" || CardCvcTB.Text.Replace(" ", "") != "")
            {
                if ((Convert.ToInt32(CardMonthTB.Text) <= 12 && Convert.ToInt32(CardMonthTB.Text) >= 1) && (Convert.ToInt32(CardYearTB.Text) >= 2012 && Convert.ToInt32(CardYearTB.Text) <= 2032))
                {



                    connect.Open();
                    int IDSponsorShip = 0; ;
                    MySqlDataReader dataReader = null;
                    string sql = $@"SELECT * FROM sponsorship";
                    MySqlCommand command = new MySqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        IDSponsorShip = Convert.ToInt32(dataReader[$@"ID_Sponsorship"].ToString());
                    }
                    dataReader.Close();
                    IDSponsorShip += 1;
                    //MessageBox.Show(IDSponsorShip.ToString());
                    sql = $@"INSERT INTO `sponsorship` (`ID_Sponsorship`, `SponsorName`, `Amount`) VALUES ({IDSponsorShip}, '{SponsorNameTB.Text}', '{SumTB.Text}')";
                    command = new MySqlCommand(sql, connect);
                    command.ExecuteReader();
                    //command.ExecuteReader().Close();
                    connect.Close();
                    connect.Open();
                    sql = $@"INSERT INTO `Sponsor_Racer` (`ID_Racer`, `ID_Sponsorship`) VALUES ({IDRacer[RunnersCB.SelectedIndex]}, {IDSponsorShip})";
                    command = new MySqlCommand(sql, connect);
                    command.ExecuteReader();
                    //command.ExecuteReader().Close();
                    connect.Close();
                    string Spider = RunnersCB.SelectedItem.ToString().Replace(")", "");
                    Spider = Spider.ToString().Replace("(", ") из ");
                    Spider = Spider.ToString().Replace("-", "(");
                    Mv.MainFrame.NavigationService.Navigate(new SponsorshipConfirmationPage(Mv, Spider, CharityNameLabel.Content.ToString(), SumLabel.Content.ToString()));
                }
                else
                    MessageBox.Show("Некорректный ввод срока действия карты!");
            }
            else
                MessageBox.Show("Все поля должны быть заполнены!");


        }
        /// <summary>
        /// Инкрементация
        /// </summary>
        /// <param name="n">число</param>
        private void IncrementSum(int n) => SumTB.Text = (Convert.ToInt32(SumTB.Text) + n).ToString();
        /// <summary>
        /// Обработчик кнопки минус
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (SumLabel.Content.ToString() != "$10")
                IncrementSum(-10);
        }
        /// <summary>
        /// Обработчик кнопки плюс
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementSum(10);
        }
        /// <summary>
        /// Изменение отображаемой суммы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void SumTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SumTB.Text) || Convert.ToInt32(SumTB.Text) < 0)
            {
                SumTB.Text = "0";
            }

            SumLabel.Content = $"${SumTB.Text}";
        }
        /// <summary>
        /// работа с благотоворительностью
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void RunnersCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            connect.Open();
            string Reg = "";
            MySqlDataReader dataReader = null;
            string sql = $@"SELECT ID_Charity FROM Registration where ID_Registration = {IDRegistration[RunnersCB.SelectedIndex]}";
            MySqlCommand command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Reg = dataReader["ID_Charity"].ToString();
            }
            dataReader.Close();
            sql = $@"SELECT Charity_Name FROM Charity where ID_Сharity = {Reg}";
            command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                CharityNameLabel.Content = dataReader["Charity_Name"].ToString();
            }
            dataReader.Close();
            connect.Close();
        }

        /// <summary>
        /// проверка на кириллицу
        /// </summary>
        /// <param name="c">символ</param>
        /// <returns></returns>
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
            if (c >= '-' && c <= '-')
                return true;
            return false;
        }
        /// <summary>
        /// проверка на латиницу
        /// </summary>
        /// <param name="c">символ</param>
        /// <returns>true если соответствует, false Если нет</returns>
        bool IsAngl(char c)
        {
            if (c >= 'a' && c <= 'z')
                return true;
            if (c >= 'A' && c <= 'Z')
                return true;
            if (c >= '-' && c <= '-')
                return true;
            return false;
        }
        /// <summary>
        /// проверка на латынь кириллицу и минус
        /// </summary>
        /// <param name="c">символ</param>
        /// <returns>true если соответствует, false Если нет</returns>
        bool IsnumRus(char c)
        {
            /*if (c >= '0' && c <= '9')
                return true;
            if (c >= '.' && c <= '.')
                return true;*/
            if (c >= 'а' && c <= 'я')
                return true;
            if (c >= 'А' && c <= 'Я')
                return true;
            if (c >= '-' && c <= '-')
                return true;
            if (c >= 'a' && c <= 'z')
                return true;
            if (c >= 'A' && c <= 'Z')
                return true;
            return false;
        }
        /// <summary>
        /// проверка на цифры
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
        /// проверка на ввод кириллицы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !e.Text.All(IsRus);
        }
        /// <summary>
        /// обработчик вставки
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                try
                {
                    string str = "";//Clipboard.GetText().Replace(".", ",");
                    if (str == null || str.All(IsRus))
                    {
                        TextBox thisTextBox = (sender as TextBox);
                        thisTextBox.Text += str;
                    }
                }
                catch (FormatException) { }
                catch (Exception) { }
            }

        }

        /// <summary>
        /// проверка ввода латиницы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void tb1_PreviewTextAngl(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsAngl);
        }
        /// <summary>
        /// обработчик вставки
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        public void OnPasteCommandAngl(object sender, ExecutedRoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                try
                {
                    string str = "";//Clipboard.GetText().Replace(".", ",");
                    if (str == null || str.All(IsAngl))
                    {
                        TextBox thisTextBox = (sender as TextBox);
                        thisTextBox.Text += str;
                    }
                }
                catch (FormatException) { }
                catch (Exception) { }
            }
        }
        /// <summary>
        /// проверка ввода цифр
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
        private void tb1_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsRus))
                e.CancelCommand();
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
        /// обработчик вставки
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void tb1_PastingAngl(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsAngl))
                e.CancelCommand();
        }
        /// <summary>
        /// проверка ввода цифр
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void NumberTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inputSymbol = e.Text[0];
            e.Handled = !char.IsDigit(inputSymbol);
        }
        /// <summary>
        /// проверка ввода кириллицы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void tb1_PreviewTextRusNum(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsnumRus);
        }
        /// <summary>
        /// обработчик вставки
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        public void OnPasteCommandRusNum(object sender, ExecutedRoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                try
                {
                    string str = "";//Clipboard.GetText().Replace(".", ",");
                    if (str == null || str.All(IsnumRus))
                    {
                        TextBox thisTextBox = (sender as TextBox);
                        thisTextBox.Text += str;
                    }
                }
                catch (FormatException) { }
                catch (Exception) { }
            }
        }

    }
}
