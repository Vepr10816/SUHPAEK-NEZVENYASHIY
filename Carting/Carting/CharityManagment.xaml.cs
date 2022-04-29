using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для CharityManagment.xaml
    /// </summary>
    public partial class CharityManagment : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        public CharityManagment(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
            MW.Back.Visibility = Visibility.Visible;
            connect.Open();
            string path = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory.ToString()).ToString()).ToString();
            DataTable datatbl = new DataTable();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter($@"select Charity_Name as 'Charity_Name', Charity_Description as 'Charity_Description', concat('C:/Users/kruto/OneDrive/Desktop/ПРАКТИКА 3 Курс №2/Carting/Carting/Images/Charity/',Charity_Logo) as 'Charity_Logo' from charity", connect);
            // mySqlDataAdapter1.Fill(datatbl1);
            mySqlDataAdapter.Fill(datatbl);
            // gridMyResult.ItemsSource = datatbl1.DefaultView;
            gridMyResult.ItemsSource = datatbl.DefaultView;



            connect.Close();
            // Thread.Sleep(5000);



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)gridMyResult.SelectedItem;
            string em = row[0].ToString();


            MessageBox.Show(em);
            Mv.MainFrame.NavigationService.Navigate(new AddEditCharity(Mv,em));
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new AddEditCharity(Mv));
        }
    }
}