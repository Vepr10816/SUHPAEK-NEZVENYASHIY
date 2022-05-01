
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
        // Подключение к базе данных
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        //Выбор текщуего фрейма
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        MySqlConnection connect = new MySqlConnection(constr);
        private string selectedImage;
        //Метод для установки картинки
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
        /// <summary>
        /// Производит инициализацию окна, а также заполняет текстовые поля записями из БД, если пользователь захотел изменить благотворительную орг
        /// </summary>
        /// <param name="MW">переменная передающаяся от MainWindow отвечающая за видимость кнопки назад</param>
        /// <param name="nm">переменная содержащая в себе название благотворительной организации</param>
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
                SelectedImage = dataReader[$@"Charity_Logo"].ToString().Replace("-", $@"").Replace("-", $@"\");
                RunnerPhoto.SetSource(dataReader[$@"Charity_Logo"].ToString().Replace("-", $@""));
                SelectedImageName = System.IO.Path.GetFileName(dataReader[$@"Charity_Logo"].ToString().Replace("-", $@""));
                FileNameTB.Text = SelectedImageName;
            }
            dataReader.Close();
            connect.Close();
        }
        /// <summary>
        /// Производит инициализацию окна, если пользователь захотел добавить благотворительную организацию
        /// </summary>
        /// <param name="MW">переменная передающаяся от MainWindow отвечающая за видимость кнопки назад</param>
        public AddEditCharity(MainWindow MW)
        {
            InitializeComponent();
            MW.Back.Visibility = Visibility.Hidden;
            Mv = MW;
            NameTB.IsReadOnly = false;
            choose = 2;
        }
        /// <summary>
        /// В зависимости от того какая кнопка была нажата до этого (Изменить/Добавить) осуществляет либо добавление благотворительной организации,
        /// либо изменение существующей
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
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
        /// <summary>
        /// Позволяет загрузить фото организации
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
                FileNameTB.Text = SelectedImageName;
            }
            catch (NotSupportedException)
            {
                MessageBox.Show("Ошибка загрузки файла.");
            }
        }
        /// <summary>
        /// Возвращает пользователя на страницу "Меню администратора"
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, предоставляет значение для событий, не содержащих данных</param>
        private void btnRefresh_Click1(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new AdministrationMenuPage(Mv));
        }
    }
}