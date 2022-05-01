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
    /// Логика взаимодействия для RiderControl.xaml
    /// </summary>
    public partial class RiderControl : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);

        /// <summary>
        /// картинка
        /// </summary>
        private string selectedImage;
        /// <summary>
        /// работа с картинкой
        /// </summary>
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
        /// название картинка
        /// </summary>
        private string SelectedImageName { get; set; }

        string IDRacer = "";
        /// <summary>
        /// инициализация окна и связь с бд
        /// </summary>
        /// <param name="MW">переменная навигации</param>
        /// <param name="email">эмейл</param>
        public RiderControl(MainWindow MW,string email)
        {
            Mv = MW;
            InitializeComponent();

            Email.Content = email;
            //Name.Content =
            connect.Open();
            string sql = $@"select user.First_Name, user.Last_Name, user.Email, Password, role_name from user join Role on user.ID_role = role.ID_Role WHERE user.Email ='{email}'";
            // string sql1 = $@"SELECT select user.First_Name from user WHERE user.Email ='{em1}' ";
            MySqlDataReader dataReader = null;
            MySqlCommand command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Email.Content = dataReader[$@"Email"].ToString();
                Name.Content = dataReader[$@"First_Name"].ToString();
                Surname.Content = dataReader[$@"Last_Name"].ToString();
                //pas.Text = dataReader[$@"Password"].ToString();
                //pas2.Text = dataReader[$@"Password"].ToString();
                //cmbPoisk.SelectedItem = dataReader[$@"Role_Name"].ToString();
            }
            dataReader.Close();
            string sql1 = $@"SELECT * from Racer where racer.First_Name ='{Name.Content}' and racer.Last_Name ='{Surname.Content}'";
            command = new MySqlCommand(sql1, connect);
            //command.ExecuteScalar

            dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                Gender.Content = dataReader[$@"Gender"].ToString();
                Dateofbirth.Content = dataReader[$@"DateOfBirth"].ToString();
                Country.Content = dataReader[$@"ID_Country"].ToString();
                IDRacer = dataReader[$@"ID_Racer"].ToString();
                //MessageBox.Show(dataReader[$@"Photo"].ToString());
            }
            dataReader.Close();
            sql = $@"SELECT * FROM Racer where ID_Racer = {IDRacer}";
            command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                SelectedImage = dataReader[$@"Photo"].ToString().Replace("-", $@"\");
                RunnerPhoto.SetSource(dataReader[$@"Photo"].ToString().Replace("-", $@"\"));
                SelectedImageName = System.IO.Path.GetFileName(dataReader[$@"Photo"].ToString().Replace("-", $@"\"));
            }
            dataReader.Close();

            //imageface.Source = new BitmapImage(new Uri(@"kart-skill-2017-map-1.ppng", UriKind.Relative));

            //imageface.Source = new BitmapImage(new Uri(@"/source/repos/Carting/Carting/Images/printer.jpg"));


            //Uri myUri = new Uri(dataReader[$@"Photo"].ToString(), UriKind.RelativeOrAbsolute);

            //Uri uri = new Uri("");
            //if (dataReader[$@"Photo"].ToString() != "")
            //{
            // imageface.Source = new Uri("");
            //}



            dataReader.Close();
            string sql2 = $@"SELECT Event_Type_Name from result,event,country,racer, registration, event_type where event.ID_Event=result.ID_Event and country.ID_Country=racer.ID_Country and racer.ID_Racer=registration.ID_Racer and registration.ID_registration=result.ID_Registration and ID_Event_Type=ID_Eventtype and racer.First_Name='{Name.Content}' and racer.Last_Name = '{Surname.Content}' ";
            command = new MySqlCommand(sql2, connect);
            //command.ExecuteScalar
            // MessageBox.Show(command.ExecuteScalar().ToString());
            dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                RaceType.Content = dataReader[$@"Event_Type_Name"].ToString();

            }

            dataReader.Close();
            string sql3 = $@"SELECT Charity_Name from racer,registration,charity where charity.ID_Сharity=registration.ID_Charity and registration.ID_Racer=racer.ID_Racer and racer.First_Name='{Name.Content}' and racer.Last_Name = '{Surname.Content}' ORDER BY Charity_Name";
            command = new MySqlCommand(sql3, connect);
            //command.ExecuteScalar
            // MessageBox.Show(command.ExecuteScalar().ToString());
            dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                Charity.Content = dataReader[$@"Charity_Name"].ToString();

            }

            dataReader.Close();
            string sql4 = $@"SELECT SponsorshipTarget from racer,registration where registration.ID_Racer=racer.ID_Racer and racer.First_Name='{Name.Content}' and racer.Last_Name = '{Surname.Content}'";
            command = new MySqlCommand(sql4, connect);
            //command.ExecuteScalar
            // MessageBox.Show(command.ExecuteScalar().ToString());
            dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                Donated.Content = "$" + dataReader[$@"SponsorshipTarget"].ToString();

            }

            dataReader.Close();
            string sql5 = $@"SELECT Statu_Name from registration_status,racer,registration where registration_status.ID_Registration_Status=registration.ID_Registration_Status and registration.ID_Racer=racer.ID_Racer and racer.First_Name='{Name.Content}' and racer.Last_Name = '{Surname.Content}'";
            command = new MySqlCommand(sql5, connect);
            //command.ExecuteScalar
            // MessageBox.Show(command.ExecuteScalar().ToString());
            dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                PaymentState.Content = dataReader[$@"Statu_Name"].ToString();

            }
            dataReader.Close();
            connect.Clone();
        }
        /// <summary>
        /// обработчик кнопки перехода
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new Sponsor_RacerDataChangePage(Mv, Email.Content.ToString(),IDRacer));
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
                //MessageBox.Show(SelectedImage);
                SelectedImageName = System.IO.Path.GetFileName(SelectedImage);
            }
            catch (NotSupportedException)
            {
                MessageBox.Show("Ошибка загрузки файла.");
            }
        }
    }
}