using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Carting.Extensions
{
	public static class ImageExtension
	{
		public static void SetSource(this Image img, string path)
		{
			path.Replace($@"\\", $@"\");
			var bi = new BitmapImage();
			bi.BeginInit();
			bi.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
			bi.EndInit();
			img.Source = bi;
		}

		public static Image ImgFromPath(string path)
		{
			path.Replace($@"\\", $@"\");
			var img = new Image();
			img.SetSource(path);
			return img;
		}
	}
}
