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
    /// Логика взаимодействия для RacerControl.xaml
    /// </summary>
    public partial class RacerControl : Page
    {
        //переменные для работы с БД
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        public RacerControl(MainWindow MW)
        {
            Mv = MW;
            MW.Back.Visibility = Visibility.Visible;
            InitializeComponent();
            //запуск функции для зполнения комбобокса
            BindCombobox();
            connect.Open();
            string sql = $@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', registration_status.Statu_Name as 'Статус' from user,racer,registration_status,registration where racer.First_Name=user.First_Name and racer.Last_Name=user.Last_Name and registration.ID_Racer=racer.ID_racer and registration.ID_registration_Status=registration_status.ID_Registration_status";
            MySqlCommand command = new MySqlCommand(sql, connect);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            gridResult.ItemsSource = dt.DefaultView;
            txtRacerAllCount.Text = gridResult.Items.Count.ToString();
            connect.Close();
        }
        private void BindCombobox()
        {
            cmbDistance.Items.Add("2.5km Race");
            cmbDistance.Items.Add("4km Race");
            cmbDistance.Items.Add("6.5km Race");
            cmbStatus.Items.Add("Registered");
            cmbStatus.Items.Add("Payment Confirmed");
            cmbStatus.Items.Add("Race Attended");
            cmbSort.Items.Add("Имя");
            cmbSort.Items.Add("Фамилия");
            cmbSort.Items.Add("Email");
            cmbSort.Items.Add("Статус");
        }

        //функция для принятия в переменные того, что выбрал пользоваетель
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)gridResult.SelectedItem;
            string name = row[0].ToString();
            string surname = row[1].ToString();
            string email = row[2].ToString();
            string status = row[3].ToString();
            Mv.MainFrame.NavigationService.Navigate(new RiderControl(Mv,email));
        }

        //кнопка для вывода отфилтрованного по выбора пользователя данных
        private void btnRefr_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSort.SelectedItem == "Имя")
            {
                connect.Open();
                string sql = $@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', registration_status.Statu_Name as 'Статус' from user,racer,registration_status,registration,result,event,event_type where racer.First_Name=user.First_Name and racer.Last_Name=user.Last_Name and registration.ID_Racer=racer.ID_racer and registration.ID_registration_Status=registration_status.ID_Registration_status and result.ID_registration=registration.ID_registration and result.ID_Event=event.ID_Event and event.ID_EventType=event_type.ID_Event_type and Event_type_name='{cmbDistance.SelectedItem.ToString()}' and Statu_Name='{cmbStatus.SelectedItem.ToString()}' order by Имя";
                MySqlCommand command = new MySqlCommand(sql, connect);
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                gridResult.ItemsSource = dt.DefaultView;
                txtRacerAllCount.Text = gridResult.Items.Count.ToString();
                connect.Close();
            }
            if (cmbSort.SelectedItem == "Фамилия")
            {
                connect.Open();
                string sql = $@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', registration_status.Statu_Name as 'Статус' from user,racer,registration_status,registration,result,event,event_type where racer.First_Name=user.First_Name and racer.Last_Name=user.Last_Name and registration.ID_Racer=racer.ID_racer and registration.ID_registration_Status=registration_status.ID_Registration_status and result.ID_registration=registration.ID_registration and result.ID_Event=event.ID_Event and event.ID_EventType=event_type.ID_Event_type and Event_type_name='{cmbDistance.SelectedItem.ToString()}' and Statu_Name='{cmbStatus.SelectedItem.ToString()}' order by Фамилия";
                MySqlCommand command = new MySqlCommand(sql, connect);
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                gridResult.ItemsSource = dt.DefaultView;
                txtRacerAllCount.Text = gridResult.Items.Count.ToString();
                connect.Close();
            }
            if (cmbSort.SelectedItem == "Email")
            {
                connect.Open();
                string sql = $@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', registration_status.Statu_Name as 'Статус' from user,racer,registration_status,registration,result,event,event_type where racer.First_Name=user.First_Name and racer.Last_Name=user.Last_Name and registration.ID_Racer=racer.ID_racer and registration.ID_registration_Status=registration_status.ID_Registration_status and result.ID_registration=registration.ID_registration and result.ID_Event=event.ID_Event and event.ID_EventType=event_type.ID_Event_type and Event_type_name='{cmbDistance.SelectedItem.ToString()}' and Statu_Name='{cmbStatus.SelectedItem.ToString()}' order by Email";
                MySqlCommand command = new MySqlCommand(sql, connect);
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                gridResult.ItemsSource = dt.DefaultView;
                txtRacerAllCount.Text = gridResult.Items.Count.ToString();
                connect.Close();
            }
            if (cmbSort.SelectedItem == "Статус")
            {
                connect.Open();
                string sql = $@"select user.First_Name as 'Имя', user.Last_Name as 'Фамилия', user.Email as'Email', registration_status.Statu_Name as 'Статус' from user,racer,registration_status,registration,result,event,event_type where racer.First_Name=user.First_Name and racer.Last_Name=user.Last_Name and registration.ID_Racer=racer.ID_racer and registration.ID_registration_Status=registration_status.ID_Registration_status and result.ID_registration=registration.ID_registration and result.ID_Event=event.ID_Event and event.ID_EventType=event_type.ID_Event_type and Event_type_name='{cmbDistance.SelectedItem.ToString()}' and Statu_Name='{cmbStatus.SelectedItem.ToString()}' and order by Статус";
                MySqlCommand command = new MySqlCommand(sql, connect);
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                gridResult.ItemsSource = dt.DefaultView;
                txtRacerAllCount.Text = gridResult.Items.Count.ToString();
                connect.Close();
            }
        }
    }
}