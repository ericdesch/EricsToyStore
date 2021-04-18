using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Utilities
{
	public class Currency
	{
		public static string Format(int price, string cultureCode = "en-US")
		{
			// restore the current culture when we are done.
			var saveCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureCode);

			var formattedPrice = ((decimal)price / GetCurrencyDigits()).ToString("C", CultureInfo.CurrentCulture);

			Thread.CurrentThread.CurrentCulture = saveCulture;

			return formattedPrice;
		}

		public static int Parse(string price, string cultureCode = "en-US")
		{
			// restore the current culture when we are done.
			var saveCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureCode);

			bool convt = decimal.TryParse(price, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out decimal decVal);

			if (!convt)
			{
				throw new ArgumentException($"Unable to parse price: {price}");
			}

			int retPrice = 0;
			try
			{
				retPrice = (int)(decVal * GetCurrencyDigits());
			}
			catch (Exception ex)
			{
				throw new ArgumentException($"Unable to parse price: {price}", ex);
			}

			Thread.CurrentThread.CurrentCulture = saveCulture;

			return retPrice;
		}

		private static int GetCurrencyDigits()
		{
			return (int)Math.Pow(10, CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalDigits);
		}

	}
}
