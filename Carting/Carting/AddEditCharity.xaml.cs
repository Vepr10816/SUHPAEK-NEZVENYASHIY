
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
    /// Логика взаимодействия для AddEditCharity.xaml
    /// </summary>
    public partial class AddEditCharity : Page
    {
        int choose = 0;
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
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
        public AddEditCharity(MainWindow MW, string nm)
        {
            InitializeComponent();
            MW.Back.Visibility = Visibility.Hidden;
            Mv = MW;
            NameTB.IsReadOnly = true;
            choose = 1;
            connect.Open();
            string nam = nm;
            MySqlDataReader dataReader = null;
            string sql = $@"select Charity_Name, Charity_Description, Charity_Logo as 'Charity_Logo' from charity where Charity_Name = '{nam}'";
            MySqlCommand command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                NameTB.Text = dataReader[$@"Charity_Name"].ToString();
                tbOpisanie.Text = dataReader[$@"Charity_Description"].ToString();
                //FileNameTB.Text = dataReader[$@"Charity_Logo"].ToString();
                SelectedImage = dataReader[$@"Charity_Logo"].ToString().Replace("-", $@"").Replace("-", $@"\");
                // MessageBox.Show(SelectedImage);
                RunnerPhoto.SetSource(dataReader[$@"Charity_Logo"].ToString().Replace("-", $@""));
                SelectedImageName = System.IO.Path.GetFileName(dataReader[$@"Charity_Logo"].ToString().Replace("-", $@""));
                // MessageBox.Show(SelectedImageName);
                FileNameTB.Text = SelectedImageName;

                /*SelectedImage = dataReader[$@"Photo"].ToString().Replace("-", $@"\");
                RunnerPhoto.SetSource(dataReader[$@"Photo"].ToString().Replace("-", $@"\"));
                SelectedImageName = System.IO.Path.GetFileName(dataReader[$@"Photo"].ToString().Replace("-", $@"\"));
                FileNameTB.Text = SelectedImageName;*/
            }
            dataReader.Close();
            connect.Close();
        }

        public AddEditCharity(MainWindow MW)
        {
            InitializeComponent();
            MW.Back.Visibility = Visibility.Hidden;
            Mv = MW;
            NameTB.IsReadOnly = false;
            choose = 2;
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            if (NameTB.Text == "" || tbOpisanie.Text == "")
            {
                MessageBox.Show("Заполните все поля");
            }
            connect.Open();
            MySqlCommand command = new MySqlCommand("", connect);
            string sql = $@"update charity set Charity_Description = '{tbOpisanie.Text}', Charity_Logo = '{FileNameTB.Text.Replace("/", "-")}' where Charity_Name = '{NameTB.Text}'";
            string sql1 = $@"insert into charity(Charity_Name,Charity_Description,Charity_Logo) values ('{NameTB.Text}','{tbOpisanie.Text}', '{FileNameTB.Text.Replace("/", "-")}')";

            if (choose == 1)
            {
                command = new MySqlCommand(sql, connect);
                command.ExecuteNonQuery();
            }
            if (choose == 2)
            {
                command = new MySqlCommand(sql1, connect);
                command.ExecuteNonQuery();
            }
            connect.Close();
            Mv.MainFrame.NavigationService.Navigate(new AdministrationMenuPage(Mv));
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

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
            }
            catch (NotSupportedException)
            {
                MessageBox.Show("Ошибка загрузки файла.");
            }
        }

        private void btnRefresh_Click1(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new AdministrationMenuPage(Mv));
        }
    }
}