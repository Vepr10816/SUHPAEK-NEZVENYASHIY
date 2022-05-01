using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для CharityListPage.xaml
    /// </summary>
    public partial class CharityListPage : Page
    {
        static string constr = "server=localhost;user=root;database=kartskills;password=;";
        MySqlConnection connect = new MySqlConnection(constr);

        //Инициализация, определение видимости кнопки Назад
        public CharityListPage(MainWindow MW)
        {
            InitializeComponent();
            Injection();
            MW.Back.Visibility = Visibility;
            //MessageBox.Show(Directory.GetParent(Environment.CurrentDirectory.ToString()).ToString());
        }
        //считывает все благотворительные организации находящиеся в БД, а затем динамически добавляет их в ListBox вместе с картинками, которые
        //хранятся в проекте
        private void Injection()
        {
            connect.Open();
            string path = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory.ToString()).ToString()).ToString();
            MySqlDataReader dataReader = null;
            string sql = $@"SELECT * FROM charity";
            MySqlCommand command = new MySqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                try
                {
                    WrapPanel wrapPanel = new WrapPanel();
                    wrapPanel.Children.Add(new Image() { Source = new BitmapImage(new Uri($@"{path}/Images/Charity/{dataReader[$@"Charity_Logo"].ToString()}")), Width = 100, Height = 100 });
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Children.Add(new Label() { Content = dataReader[$@"Charity_Name"].ToString(), FontSize = 18, FontFamily = new FontFamily("Arial"), Margin = new Thickness(20, 0, 0, 0) });
                    stackPanel.Children.Add(new TextBlock() { Text = dataReader[$@"Charity_Description"].ToString(), Width = 555, TextWrapping = TextWrapping.Wrap, FontSize = 12, FontFamily = new FontFamily("Arial"), Margin = new Thickness(25, 10, 0, 0) });
                    wrapPanel.Children.Add(stackPanel);
                    CharitiesLB.Items.Add(wrapPanel);
                }
                catch { }
                /*IDRow.Add(dataReader[$@"Hall_ID"].ToString());*/
            }
            dataReader.Close();
            connect.Close();
        }
    }
}
