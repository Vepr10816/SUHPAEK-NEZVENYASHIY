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
using Carting.Extensions;

namespace Carting
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        //MainWindow MW = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        /*public bool Hiden;
        public MainWindow(bool hid)
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(new MainMenuPage(this));
            Back.Visibility = Visibility.Hidden;
            Hiden = hid;
            //MainFrame.Navigate(new MainMenuPage(this));
        }*/

        public MainWindow()
		{
			InitializeComponent();
			//MainFrame.NavigationService.Navigate(new MainMenuPage(/*this*/));
			MainFrame.NavigationService.Navigate(new MainMenuPage(this));
			//MainFrame.NavigationService.Navigate(new RacerRegistrationPage());
			Back.Visibility = Visibility.Hidden;
			RemainingTimeSetting(); //async
									//BtnHid();                     //Back.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
			Exit.Visibility = Visibility.Hidden;							  //MainFrame.Navigate(new MainMenuPage(this));
		}



		private void MainFrame_Navigated(object sender, NavigationEventArgs e)
		{
			if (((Page)MainFrame.Content).Title == "MainMenuPage")
				ShowHeaderFull();
			else
				ShowHeaderMini();

			//SetActualWindowTitle();
		}

		//private void SetActualWindowTitle()
		//{
		//    var curPage = (Page)MainFrame.Content;
		//    Title = curPage.Title == "MainMenuPage"
		//        ? "Kart Skills 2017"
		//        : $"Kart Skills 2017 - {curPage.Title}";
		//}


		public void ShowHeaderMini()
		{
			MainGrid.RowDefinitions[0].Height = new GridLength(45);
			HeaderFull.Visibility = Visibility.Collapsed;
			HeaderMini.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Изменяет верхнюю панель на широкую версию (для главного меню).
		/// </summary>
		public void ShowHeaderFull()
		{
			MainGrid.RowDefinitions[0].Height = new GridLength(140);
			HeaderMini.Visibility = Visibility.Collapsed;
			HeaderFull.Visibility = Visibility.Visible;
		}

		private void FrameGoBack_Click(object sender, RoutedEventArgs e)
		{
			//MessageBox.Show(MainFrame.Content.ToString());
			Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().MainFrame.NavigationService.GoBack();
			//MessageBox.Show(((Page)MainFrame.Content).Title.ToString());
			if (((Page)MainFrame.Content).Title == "BecomeRacerPage" || ((Page)MainFrame.Content).Title == "RacerSponsorPage" || ((Page)MainFrame.Content).Title == "SponsorshipConfirmationPage" 
				|| ((Page)MainFrame.Content).Title == "MyResults" || ((Page)MainFrame.Content).Title == "MySponsor" || 
				((Page)MainFrame.Content).Title == "EventInfoMenuPage" || ((Page)MainFrame.Content).Title == "UsersManagment" || 
				((Page)MainFrame.Content).Title == "CharityManagment" || ((Page)MainFrame.Content).Title == "RacerControl" || ((Page)MainFrame.Content).Title == "ViewSponsors")
			{
				Back.Visibility = Visibility.Hidden;
				//MessageBox.Show("fghj");
			}
		}

		private void LogoutButton_Click(object sender, RoutedEventArgs e)
		{

			//MW.MainFrame.NavigationService.GoBack();
			//Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().MainFrame.NavigationService.;
			MainFrame.NavigationService.Navigate(new MainMenuPage(this));
			Exit.Visibility = Visibility.Hidden;
			Back.Visibility = Visibility.Hidden;
		}


		private Tuple<TimeSpan, bool> GetRemainingTime()
		{
			//берём время начала - 6:00:00 24.11.2020
			var startTime = new DateTime(2022, 01, 25, 17, 10, 0);
			//если марафон начался или прошёл
			if (DateTime.Now.CompareTo(startTime) >= 0)
				return new Tuple<TimeSpan, bool>(default, true);
			//иначе вычисляем оставшееся время и возвращаем
			var remainingTime = startTime.Subtract(DateTime.Now);
			return new Tuple<TimeSpan, bool>(remainingTime, false);
		}

		/// <summary>
		/// Возвращает слово, указываемое после количества дней.
		/// </summary>
		/// <param name="d">Количество дней.</param>
		private string GetDaysWord(int d)
		{
			var ds = d.ToString();
			if (ds.Length == 1)
			{
				if (d == 0 || d > 4)
					return "дней";
				if (d == 1)
					return "день";
				return "дня";
			}
			if (ds[ds.Length - 2] != '1')
			{
				if (ds[ds.Length - 1] == '1')
					return "день";
				var dLast = Convert.ToInt32(ds[ds.Length - 1].ToString());
				if (dLast.IsBeth(2, 4))
					return "дня";
			}
			return "дней";
		}

		/// <summary>
		/// Возвращает слово, указываемое после количества часов.
		/// </summary>
		/// <param name="h">Количество часов.</param>
		private string GetHoursWord(int h)
		{
			if (h == 1 || h == 21)
				return "час";
			if (h.IsBeth(2, 4) || h.IsBeth(22, 24))
				return "часа";
			return "часов";
		}

		/// <summary>
		/// Возвращает слово, указываемое после количества минут.
		/// </summary>
		/// <param name="m">Количество минут.</param>
		private string GetMinutesWord(int m)
		{
			if (m == 1 || m == 21 || m == 31 || m == 41 || m == 51)
				return "минута";
			if (m.IsBeth(2, 4) || m.IsBeth(22, 24) || m.IsBeth(32, 34) || m.IsBeth(42, 44) || m.IsBeth(52, 54))
				return "минуты";
			return "минут";
		}

		// /// <summary>
		// /// Возвращает слово, указываемое после количества секунд.
		// /// </summary>
		// /// <param name="s">Количество секунд.</param>
		private string GetSecondsWord(int s)
		{
			if (s == 1 || s == 21 || s == 31 || s == 41 || s == 51)
				return "секунда";
			if (s.IsBeth(2, 4) || s.IsBeth(22, 24) || s.IsBeth(32, 34) || s.IsBeth(42, 44) || s.IsBeth(52, 54))
				return "секунды";
			return "секунд";
		}

		/// <summary>
		/// Изменяет отображаемое оставшееся время.
		/// </summary>
		/// <param name="time">Отображаемый промежуток времени.</param>
		private void ChangeRemainingTime(TimeSpan time)
		{
			var d = time.Days;
			var dd = GetDaysWord(d);
			var h = time.Hours;
			var hh = GetHoursWord(h);
			var m = time.Minutes;
			var mm = GetMinutesWord(m);
			var s = time.Seconds;
			var ss = GetSecondsWord(s);
			RemainingTimeLabel.Content = $"До начала события 0 лет, 0 месяцев, {d} {dd}, {h} {hh}, {m} {mm} и {s} {ss}";
			/*if(MainFrame.Content != null)
			{
				if (((Page)MainFrame.Content).Title != "MainMenuPage")
				{
					//MessageBox.Show(((Page)MainFrame.Content).Title.ToString());
					Back.Visibility = Visibility.Visible;
					//MessageBox.Show("fghj");
				}
                else
                {
					Back.Visibility = Visibility.Hidden;
				}
			}*/
			//строка по типу "23 дня 6 часов 1 минута /*56 секунд*/ до старта марафона!"
		}

		/// <summary>
		/// Цикличная установка оставшегося времени.
		/// </summary>
		private async void RemainingTimeSetting()
		{
			while (true)
			{
				var remTime = GetRemainingTime();
				//если марафон не начался, обновляем оставшееся время
				if (!remTime.Item2)
					ChangeRemainingTime(GetRemainingTime().Item1);
				//иначе, выводим, что марафон закончился, и выходим из цикла
				else
				{
					RemainingTimeLabel.Content = "До начала события 0 лет, 0 месяцев, 0 дней, 0 часов, 0 минут и 0 секунд";
					break;
				}
				//остановка потока на 1 с.
				await Task.Delay(1000);
			}
		}

		//private void ChangeRemainingBtn()
		//{
		//	if (MainFrame.Content != null)
		//	{
		//		if (((Page)MainFrame.Content).Title != "MainMenuPage")
		//		{
		//			//MessageBox.Show(((Page)MainFrame.Content).Title.ToString());
		//			Back.Visibility = Visibility.Visible;
		//			//MessageBox.Show("fghj");
		//		}
		//		else
		//		{
		//			Back.Visibility = Visibility.Hidden;
		//		}
		//	}
		//	//строка по типу "23 дня 6 часов 1 минута /*56 секунд*/ до старта марафона!"
		//}

		//private async void BtnHid()
		//{
		//	while (true)
		//	{
		//		ChangeRemainingBtn();
		//		//остановка потока на 1 с.
		//		await Task.Delay(100);
		//	}
		//}


	}
}
