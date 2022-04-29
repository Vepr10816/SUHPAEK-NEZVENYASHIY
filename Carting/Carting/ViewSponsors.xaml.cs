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
    /// Логика взаимодействия для ViewSponsors.xaml
    /// </summary>
    public partial class ViewSponsors : Page
    {
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public ViewSponsors(MainWindow MW)
        {
            Mv = MW;
            MW.Back.Visibility = Visibility;
            InitializeComponent();
            BindComboBox();
            connect.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("select Charity_Name, concat('C:/Users/kruto/OneDrive/Desktop/ПРАКТИКА 3 Курс №2/Carting/Carting/Images/Charity/',Charity_Logo) AS 'Charity_Logo', SUM(SponsorshipTarget) AS 'SUM' FROM registration, charity where registration.ID_charity=charity.ID_Сharity group by charity.charity_Name ", connect);
            DataTable dt = new DataTable();
            mySqlDataAdapter.Fill(dt);
            gridMyResult.ItemsSource = dt.DefaultView;
            CharityOrganisations.Content = (gridMyResult.Items.Count).ToString();
            MySqlCommand comm = new MySqlCommand("select SUM(SponsorshipTarget) FROM registration, charity where registration.ID_charity=charity.ID_Сharity", connect);
            MySqlDataReader dataReader = null;
            dataReader = comm.ExecuteReader();
            dataReader.Read();
            TotalSponsorshipContributions.Content = dataReader.GetString(0);
            dataReader.Close();
            connect.Close();
        }
        private void BindComboBox()
        {

            Filtration.Items.Add("Итоговая сумма");
            Filtration.Items.Add("Наименование");

        }

        private void btnFiltration_Click(object sender, RoutedEventArgs e)
        {
            if (Filtration.SelectedItem == "Итоговая сумма")
            {
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("select Charity_Name, concat('C:/Users/kruto/OneDrive/Desktop/ПРАКТИКА 3 Курс №2/Carting/Carting/Images/Charity/',Charity_Logo) AS 'Charity_Logo', SUM(SponsorshipTarget) AS 'SUM' FROM registration, charity where registration.ID_charity=charity.ID_Сharity group by charity.charity_Name order by SUM", connect);
                DataTable dt = new DataTable();
                mySqlDataAdapter.Fill(dt);
                gridMyResult.ItemsSource = dt.DefaultView;
            }
            if (Filtration.SelectedItem == "Наименование")
            {
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("select Charity_Name, concat('C:/Users/kruto/OneDrive/Desktop/ПРАКТИКА 3 Курс №2/Carting/Carting/Images/Charity/',Charity_Logo) AS 'Charity_Logo', SUM(SponsorshipTarget) AS 'SUM' FROM registration, charity where registration.ID_charity=charity.ID_Сharity group by charity.charity_Name order by Charity_Name", connect);
                DataTable dt = new DataTable();
                mySqlDataAdapter.Fill(dt);
                gridMyResult.ItemsSource = dt.DefaultView;
            }
        }
    }
}