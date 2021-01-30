﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace ObservablesDemo
{
    internal class AgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return (DateTime.Now - date).TotalDays / 365.2425;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
