using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RssDev.Common.Converter
{
	public class BoolToVisibilityConverter : IValueConverter
	{
		public Visibility? TrueTo { get; set; }
		public Visibility? FalseTo { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool b)) { return DependencyProperty.UnsetValue; }
			return b ? TrueTo : FalseTo;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
