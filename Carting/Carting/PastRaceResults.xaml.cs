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
    /// Логика взаимодействия для PastRaceResults.xaml
    /// </summary>
    public partial class PastRaceResults : Page
    {
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        public PastRaceResults(MainWindow MW, int IDRacer)
        {
            InitializeComponent();
            BindComboBox();

            connect.Open();
            string sql = $@"select ID_Result, RaceTime as 'Время', concat(Racer.First_Name, ' ', Racer.Last_Name) as 'Гонщик', country.ID_Country as 'Страна' from result, racer, country, registration where country.ID_Country=racer.ID_Country and racer.ID_Racer=registration.ID_Racer and registration.ID_registration=result.ID_Registration and RaceTime <> '00:00:00' order by RaceTime";
            MySqlCommand command = new MySqlCommand(sql, connect);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            gridResult.ItemsSource = dt.DefaultView;
            txtRacerFinishCount.Text = (gridResult.Items.Count).ToString();
            int count = 0;
            DataGridRow dataGridRow = new DataGridRow();
            string sql2 = $@"select * from racer";
            MySqlCommand command2 = new MySqlCommand(sql2, connect);
            MySqlDataReader mySqlDataReader = null;
            mySqlDataReader = command2.ExecuteReader();
            int i = 0;

            while (mySqlDataReader.Read())
                i++;
            txtRacerAllCount.Text = i.ToString();
            //MessageBox.Show(((DataRowView)gridResult.SelectedItems[0]).Row["Время"].ToString());
            mySqlDataReader.Close();
            MySqlDataReader dataReader = null;

            double srtime = 0;
            sql = $@"SELECT AVG(RaceTime) AS average FROM result;";
            command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                srtime = Convert.ToDouble(dataReader[$@"average"].ToString());
            }
            dataReader.Close();
            srtime = Math.Round(srtime / 60, 2);
            txtTime.Text = srtime.ToString().Replace(",", "m ") + "s";
            connect.Close();


            //while (count<= gridResult.Items.Count)
            //{
            // count++;
            // dt.Rows.
            //}
        }
        private void BindComboBox()
        {

            connect.Open();
            DataTable datatbl1 = new DataTable();

            //MySqlCommand mySqlDataReader = new MySqlCommand("select ID_Employee, Surname, Employee_Name from Employee", connect);
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("select * from Event", connect);

            mySqlDataAdapter.Fill(datatbl1);

            MySqlDataReader dataReader;
            MySqlCommand comman = new MySqlCommand("select * from Event", connect);
            dataReader = comman.ExecuteReader();

            while (dataReader.Read())
            {
                cmbEvent.Items.Add(dataReader[$@"Event_Name"].ToString());
            }


            cmbTrackTye.Items.Add("2.5km Race");
            cmbTrackTye.Items.Add("4km Race");
            cmbTrackTye.Items.Add("6.5km Race");

            cmbGender.Items.Add("M");
            cmbGender.Items.Add("F");
            cmbGender.Items.Add("ANY");

            cmbAge.Items.Add("До 18");
            cmbAge.Items.Add("18-29");
            cmbAge.Items.Add("30-39");
            cmbAge.Items.Add("40-55");
            cmbAge.Items.Add("56-70");
            cmbAge.Items.Add("Более 70");

            connect.Close();

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            {
                string a = "";
                string b = "";

                if (cmbAge.SelectedItem == "До 18")
                {
                    a = "2010-01-01";
                    b = "2005-01-01";
                }

                if (cmbAge.SelectedItem == "18-29")
                {
                    a = "1993-01-01";
                    b = "2004-01-01";
                }

                if (cmbAge.SelectedItem == "30-39")
                {
                    a = "1983-01-01";
                    b = "1992-01-01";
                }

                if (cmbAge.SelectedItem == "40-55")
                {
                    a = "1967-01-01";
                    b = "1982-01-01";
                }

                if (cmbAge.SelectedItem == "56-70")
                {
                    a = "1952-01-01";
                    b = "1966-01-01";
                }

                if (cmbAge.SelectedItem == "Более 70")
                {
                    a = "1922-01-01";
                    b = "1951-01-01";
                }

                if (cmbGender.SelectedItem == "ANY")
                {
                    connect.Open();

                    MessageBox.Show(a.ToString());

                    string sql2 = $@"SELECT ID_Result, RaceTime as 'Время', concat(Racer.First_Name, ' ', Racer.Last_Name) as'Гонщик', country.ID_Country as'Страна' from result,event,country,racer, registration, event_type where event.ID_Event=result.ID_Event and country.ID_Country=racer.ID_Country and racer.ID_Racer=registration.ID_Racer and
registration.ID_registration=result.ID_Registration and ID_Event_Type=ID_Eventtype and Event_Name = '{cmbEvent.SelectedItem.ToString()}' and Event_Type_Name='{cmbTrackTye.SelectedItem.ToString()}' and racer.DateOfBirth BETWEEN cast('{a.ToString()}' as date) and cast('{b.ToString()}' as date) and not RaceTime in (cast('00:00:00' as time)) order by RaceTime";
                    MySqlCommand command = new MySqlCommand(sql2, connect);
                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    gridResult.ItemsSource = dt.DefaultView;
                    txtRacerFinishCount.Text = gridResult.Items.Count.ToString();
                    txtRacerAllCount.Text = gridResult.Items.Count.ToString();
                    //string sql1 = $@"select ID_event_type from event join event_type on event_type.ID_event_type=ID_event_type where ID_event_type = '{cmbTrackTye.SelectedItem}'";
                    connect.Close();
                    connect.Open();
                    MySqlDataReader dataReader = null;

                    double srtime = 0;
                    string idEvent = "";
                    string idEventType = "";
                    string sql = $@"SELECT * FROM Event_Type where Event_Type_Name='{cmbTrackTye.SelectedItem.ToString()}'";
                    command = new MySqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        idEventType = dataReader[$@"ID_Event_Type"].ToString();
                    }
                    dataReader.Close();
                    sql = $@"SELECT * from Event where Event_Name = '{cmbEvent.SelectedItem.ToString()}' and ID_EventType='{idEventType}'";
                    command = new MySqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        idEvent = dataReader[$@"ID_Event"].ToString();
                    }
                    dataReader.Close();

                    sql = $@"SELECT AVG(RaceTime) AS average FROM result where ID_Event = '{idEvent}'";
                    command = new MySqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    try
                    {
                        while (dataReader.Read())
                        {
                            srtime = Convert.ToDouble(dataReader[$@"average"].ToString());
                        }
                    }
                    catch { srtime = 0; }
                    dataReader.Close();
                    srtime = Math.Round(srtime / 60, 2);
                    txtTime.Text = srtime.ToString().Replace(",", "m ") + "s";

                    connect.Close();
                }
                else
                {
                    connect.Open();

                    MessageBox.Show(a.ToString());

                    string sql2 = $@"SELECT ID_Result, RaceTime as 'Время', concat(Racer.First_Name, ' ', Racer.Last_Name) as'Гонщик', country.ID_Country as'Страна' from result,event,country,racer, registration, event_type where event.ID_Event=result.ID_Event and country.ID_Country=racer.ID_Country and racer.ID_Racer=registration.ID_Racer and registration.ID_registration=result.ID_Registration and ID_Event_Type=ID_Eventtype and Event_Name = '{cmbEvent.SelectedItem.ToString()}' and Event_Type_Name='{cmbTrackTye.SelectedItem.ToString()}' and racer.Gender='{cmbGender.SelectedItem.ToString()}' and racer.DateOfBirth BETWEEN cast('{a.ToString()}' as date) and cast('{b.ToString()}' as date) and not RaceTime in (cast('00:00:00' as time)) order by RaceTime";
                    MySqlCommand command = new MySqlCommand(sql2, connect);
                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    gridResult.ItemsSource = dt.DefaultView;
                    txtRacerFinishCount.Text = gridResult.Items.Count.ToString();
                    txtRacerAllCount.Text = gridResult.Items.Count.ToString();
                    //string sql1 = $@"select ID_event_type from event join event_type on event_type.ID_event_type=ID_event_type where ID_event_type = '{cmbTrackTye.SelectedItem}'";
                    connect.Close();
                    connect.Close();
                    connect.Open();
                    MySqlDataReader dataReader = null;

                    double srtime = 0;
                    string idEvent = "";
                    string idEventType = "";
                    string sql = $@"SELECT * FROM Event_Type where Event_Type_Name='{cmbTrackTye.SelectedItem.ToString()}'";
                    command = new MySqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        idEventType = dataReader[$@"ID_Event_Type"].ToString();
                    }
                    dataReader.Close();
                    sql = $@"SELECT * from Event where Event_Name = '{cmbEvent.SelectedItem.ToString()}' and ID_EventType='{idEventType}'";
                    command = new MySqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        idEvent = dataReader[$@"ID_Event"].ToString();
                    }
                    dataReader.Close();

                    sql = $@"SELECT AVG(RaceTime) AS average FROM result where ID_Event = '{idEvent}'";
                    command = new MySqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    try
                    {
                        while (dataReader.Read())
                        {
                            srtime = Convert.ToDouble(dataReader[$@"average"].ToString());
                        }
                    }
                    catch
                    {
                        srtime = 0;
                    }
                    dataReader.Close();
                    srtime = Math.Round(srtime / 60, 2);
                    txtTime.Text = srtime.ToString().Replace(",", "m ") + "s";
                    connect.Close();
                }


                //string sql2 = $@"SELECT RaceTime as 'Время', concat(Racer.First_Name, ' ', Racer.Last_Name) as'Гонщик', country.ID_Country as'Страна' from Event_type join Event on ID_EventType = ID_Event join Result on result.ID_event = event.ID_event join Registration on result.ID_Registration = registration.ID_registration join Racer on registration.ID_racer = racer.ID_Racer join Gender on racer.Gender = ID_Gender join Race on event.ID_Race=race.ID_Race join Country on country.ID_Country=racer.ID_Country where Event_type_name = '{cmbTrackTye.SelectedItem}' and racer.Gender='{cmbGender.SelectedItem}'"; /*and Event_Name = '{cmbEvent.SelectedItem}'*/
                ////string sql1 = $@"select ID_event_type from event join event_type on event_type.ID_event_type=ID_event_type where ID_event_type = '{cmbTrackTye.SelectedItem}'";
                //string sql = $@"select racer.First_name from racer join Gender on racer.Gender = ID_Gender where racer.Gender='{cmbGender.SelectedItem}'";
                //MySqlCommand command = new MySqlCommand(sql2, connect);
                ///* INTERSECT select ID_event_type from event join event_type on ID_event_type = event_type.ID_event_type where ID_event_type = '{cmbTrackTye.SelectedItem}'*/







            }
        }

        private void gridResult_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
            gridResult.Columns[4].Visibility = Visibility.Hidden;
        }
    }
}