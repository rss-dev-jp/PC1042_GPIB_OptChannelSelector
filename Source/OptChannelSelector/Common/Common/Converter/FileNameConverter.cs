using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace RssDev.Common.Converter
{
	public class FileNameConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var list = value as List<string>;
			var result = new List<string>();

			if (list != null)
			{
				foreach(var file in list)
				{
					result.Add(System.IO.Path.GetFileName(file));
				}
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
