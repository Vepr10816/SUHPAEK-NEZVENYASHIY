using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для MyResults.xaml
    /// </summary>


    class MyClass
    {
        public string First { get; set; }

        public string Second { get; set; }
        public string Thrid { get; set; }
        public string Fourth { get; set; }

        public string Fifth { get; set; }
    }

    public partial class MyResults : Page
    {
        ObservableCollection<MyClass> collection = null;
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        int RacerID = 0;
        public MyResults(MainWindow MW, int IDRacer)
        {
            RacerID = IDRacer;
            MW.Back.Visibility = Visibility.Visible;
            Mv = MW;
            try
            {
                List<string> idevent = new List<string>();
                List<string> idrace = new List<string>();
                List<string> sobitie = new List<string>();
                List<string> vidgonki = new List<string>();
                List<string> vremya = new List<string>();
                List<string> obsheevremya = new List<string>();
                List<string> kategoryvremya = new List<string>();
                List<string> IDReg = new List<string>();
                List<string> IDRecers = new List<string>();
                string SQLdata = "";
                string gender = "";
                InitializeComponent();
                connect.Open();
                if (collection == null)
                {
                    collection = new ObservableCollection<MyClass>();
                    gridMyResult.ItemsSource = collection;
                }

                string DateOfBirth = "";
                MySqlDataReader dataReader = null;
                string sql = $@"SELECT * FROM Racer where ID_Racer = {IDRacer}";
                MySqlCommand command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Gender.Content = dataReader[$@"Gender"].ToString().Replace("F", "Женский").Replace("M", "Мужской");
                    DateOfBirth = dataReader[$@"DateOfBirth"].ToString();
                    gender = dataReader[$@"Gender"].ToString();
                }
                dataReader.Close();
                DateOfBirth = DateOfBirth.Substring(6).Replace("0:00:00", "");
                int Kategory = 2021 - Convert.ToInt32(DateOfBirth);
                if (Kategory < 18)
                {
                    AgeCategory.Content = "до 18";
                    SQLdata = $@"Select* from racer where racer.DateOfBirth BETWEEN cast('2003.01.01' as date) and cast('2009.01.01' as date) and Gender = '{gender}'";
                }
                if (Kategory > 18 && Kategory <= 29)
                {
                    AgeCategory.Content = "18-29";
                    SQLdata = $@"Select* from racer where racer.DateOfBirth BETWEEN cast('1991.01.01' as date) and cast('2003.01.01' as date) and Gender = '{gender}'";
                }
                if (Kategory > 29 && Kategory <= 39)
                {
                    AgeCategory.Content = "30-39";
                    SQLdata = $@"Select* from racer where racer.DateOfBirth BETWEEN cast('1981.01.01' as date) and cast('1991.01.01' as date) and Gender = '{gender}'";
                }
                if (Kategory > 39 && Kategory <= 55)
                {
                    AgeCategory.Content = "40-55";
                    SQLdata = $@"Select* from racer where racer.DateOfBirth BETWEEN cast('1967.01.01' as date) and cast('1981.01.01' as date) and Gender = '{gender}'";
                }
                if (Kategory > 55 && Kategory <= 70)
                {
                    AgeCategory.Content = "56-70";
                    SQLdata = $@"Select* from racer where racer.DateOfBirth BETWEEN cast('1952.01.01' as date) and cast('1967.01.01' as date) and Gender = '{gender}'";
                }
                if (Kategory > 70)
                {
                    AgeCategory.Content = "более 70";
                    SQLdata = $@"Select* from racer where racer.DateOfBirth BETWEEN cast('1932.01.01' as date) and cast('1952.01.01' as date) and Gender = '{gender}'";
                }

                string registration = "";
                sql = $@"SELECT * FROM Registration where ID_Racer = {IDRacer}";
                command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    registration = dataReader[$@"ID_Registration"].ToString();
                }
                dataReader.Close();

                sql = $@"SELECT * FROM Result where ID_Registration = {registration}";
                command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    idevent.Add(dataReader[$@"ID_Event"].ToString());
                    //collection.Add(new MyClass() { Second = dataReader[$@"RaceTime"].ToString()});
                    vremya.Add(dataReader[$@"RaceTime"].ToString());
                }
                dataReader.Close();

                foreach (var person in idevent)
                {
                    sql = $@"SELECT * FROM Event where ID_Event = {person}";
                    command = new MySqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        idrace.Add(dataReader[$@"ID_Race"].ToString());
                        //collection.Add(new MyClass() { First = dataReader[$@"ID_EventType"].ToString() });
                        vidgonki.Add(dataReader[$@"ID_EventType"].ToString());
                    }
                    dataReader.Close();
                }

                foreach (var person in idrace)
                {
                    sql = $@"SELECT * FROM Race where ID_Race = {person}";
                    command = new MySqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        //collection.Add(new MyClass() { Thrid = dataReader[$@"Year_Held"].ToString() + " " + dataReader[$@"Sity"].ToString() });
                        sobitie.Add(dataReader[$@"Year_Held"].ToString() + " " + dataReader[$@"Sity"].ToString());
                    }
                    dataReader.Close();
                }


                int j = 1;
                for (int i = 0; i < idevent.Count; i++)
                {
                    sql = $@"SELECT * FROM Result where ID_Event = {idevent[i]} and RaceTime <> '00:00:00' order by RaceTime";
                    command = new MySqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (dataReader[$@"ID_Registration"].ToString() == registration)
                            obsheevremya.Add(j.ToString());
                        //idevent.Add(dataReader[$@"ID_Event"].ToString());
                        //collection.Add(new MyClass() { Second = dataReader[$@"RaceTime"].ToString()});
                        //vremya.Add(dataReader[$@"RaceTime"].ToString());
                        j++;
                    }
                    dataReader.Close();
                }

                command = new MySqlCommand(SQLdata, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    IDRecers.Add(dataReader[$@"ID_Racer"].ToString());
                }
                dataReader.Close();


                foreach (var person in IDRecers)
                {
                    sql = $@"SELECT * FROM Registration where ID_Racer = {person}";
                    command = new MySqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        IDReg.Add(dataReader[$@"ID_Registration"].ToString());
                    }
                    dataReader.Close();
                }


                string SQLKategoryNumberRegistrations = "(";
                foreach (var person in IDReg)
                {
                    SQLKategoryNumberRegistrations += $@" ID_Registration = {person} or";
                }
                SQLKategoryNumberRegistrations += ")";
                SQLKategoryNumberRegistrations = SQLKategoryNumberRegistrations.Replace("or)", ")");

                j = 1;
                foreach (var person in idevent)
                {
                    string SQLKategoryMesto = $@"SELECT * FROM Result where ID_Event = {person} and {SQLKategoryNumberRegistrations} order by RaceTime";
                    command = new MySqlCommand(SQLKategoryMesto, connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (dataReader[$@"ID_Registration"].ToString() == registration)
                            kategoryvremya.Add(j.ToString());
                        j++;
                    }
                    dataReader.Close();
                }


                for (int i = 0; i < sobitie.Count; i++)
                {
                    collection.Add(new MyClass() { First = sobitie[i], Second = vidgonki[i], Thrid = vremya[i], Fourth = obsheevremya[i], Fifth = kategoryvremya[i] });
                }
                //MessageBox.Show(SQLKategoryNumberRegistrations);



                connect.Close();
            }
            catch
            {
                connect.Close();
                connect.Open();
                string sql = $@"select count(*) from Registration where ID_Racer = {RacerID}";
                MySqlCommand command = new MySqlCommand(sql, connect);
                MySqlDataReader dataReader = null;
                dataReader = command.ExecuteReader();
                string count = "";
                while (dataReader.Read())
                {
                    count = dataReader[$@"count(*)"].ToString();
                }
                dataReader.Close();
                if (count == "0")
                    MessageBox.Show("Сначала зарегестрируйтесь на гонку");
                else
                    MessageBox.Show("Результаты обрабатываются, для более быстрого получения информации - свяжитьсь с Координатором");
                connect.Close();
            }


        }

        private void ShowAllResults_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new PastRaceResults(Mv, RacerID));
        }
    }
}
