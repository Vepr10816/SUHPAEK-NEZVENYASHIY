using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Логика взаимодействия для MySponsor.xaml
    /// </summary>
    
    public partial class MySponsor : Page
    {

        ObservableCollection<MyClass> collection = null;
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        int idracer = 0;

        private string selectedImage;
        private string SelectedImage
        {
            get => selectedImage;
            set
            {
                selectedImage = value;
                if (selectedImage != null)
                {
                    //DefaultImage.Visibility = Visibility.Collapsed;
                    RunnerPhoto.Visibility = Visibility.Visible;
                }
            }
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
                MessageBox.Show(SelectedImage);
                SelectedImageName = System.IO.Path.GetFileName(SelectedImage);
            }
            catch (NotSupportedException)
            {
                MessageBox.Show("Ошибка загрузки файла.");
            }
        }
        private string SelectedImageName { get; set; }
        public MySponsor(MainWindow MW,int IDRacer)
        {
            InitializeComponent();
            MW.Back.Visibility = Visibility;
            MW.Exit.Visibility = Visibility;
            try
            {

                if (collection == null)
                {
                    collection = new ObservableCollection<MyClass>();
                    grid_sponsor.ItemsSource = collection;
                }

                string path = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory.ToString()).ToString()).ToString();
                idracer = IDRacer;
                connect.Open();
                string idcharity = "";
                List<string> idsponsor = new List<string>();
                MySqlDataReader dataReader = null;
                string sql = $@"SELECT ID_Charity FROM Registration where ID_Racer = {IDRacer}";
                MySqlCommand command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    idcharity = dataReader[$@"ID_Charity"].ToString();
                }
                dataReader.Close();

                sql = $@"SELECT * FROM Charity where ID_Сharity = {idcharity}";
                command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    txt_fund.Text = dataReader[$@"Charity_Name"].ToString();
                    txt_desc.Text = dataReader[$@"Charity_Description"].ToString();
                    //this.Logo.Source = new BitmapImage(new Uri($@"{path}/Images/Charity/{dataReader[$@"Charity_Logo"].ToString()}", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                    try
                    {
                        SelectedImage = $@"{path}/Images/Charity/{dataReader[$@"Charity_Logo"].ToString()}";
                        RunnerPhoto.SetSource($@"{path}/Images/Charity/{dataReader[$@"Charity_Logo"].ToString()}");
                        SelectedImageName = System.IO.Path.GetFileName($@"{path}/Images/Charity/{dataReader[$@"Charity_Logo"].ToString()}");
                    }
                    catch 
                    { 

                    }
                }
                dataReader.Close();

                sql = $@"SELECT * FROM Sponsor_Racer where ID_Racer = {IDRacer}";
                command = new MySqlCommand(sql, connect);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    idsponsor.Add(dataReader[$@"ID_Sponsorship"].ToString());
                }
                dataReader.Close();
                double Amount = 0;
                foreach (var person in idsponsor)
                {
                    sql = $@"SELECT * FROM Sponsorship where ID_Sponsorship = {person}";
                    command = new MySqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        collection.Add(new MyClass() { First = dataReader[$@"SponsorName"].ToString(), Second = dataReader[$@"Amount"].ToString() });
                        Amount += Convert.ToDouble(dataReader[$@"Amount"].ToString());
                    }
                    dataReader.Close();
                }
                connect.Close();
                txt_fund_Copy.Text = $@"Всего ${Amount}";
            }
            catch
            {
                MessageBox.Show("У вас пока нет спонсоров!");
            }
        }
    }
}
