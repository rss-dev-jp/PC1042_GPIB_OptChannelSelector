﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace RssDev.Common.Converter
{
	public class BoolNegativeConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is bool && (bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is bool && (bool)value);
        }

    }
}
