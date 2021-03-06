using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carting.Extensions
{
	public static class ObjectExtension
	{
		/// <summary>
		/// Конвертирует объект в System.Int32.
		/// </summary>
		/// <param name="obj">Объект.</param>
		/// <returns>Число.</returns>
		public static int ToInt(this object obj) => Convert.ToInt32(obj);

		public static double ToDouble(this object obj) => Convert.ToDouble(obj);
	}
}
