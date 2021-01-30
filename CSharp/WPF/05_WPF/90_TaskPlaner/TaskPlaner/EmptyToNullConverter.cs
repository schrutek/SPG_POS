using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace TaskPlaner
{
    public class EmptyToNullConverter : IValueConverter
    {
        // Wird aufgerufen, wenn ein Wert aus der DB im Textfeld angezeigt wird.
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null ? "" : value;
        }

        // Wird aufgerufen, wenn ein Wert aus dem Textfeld in die DB geschrieben werden soll.
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrEmpty(value.ToString()) ? null : value;
        }
    }
   
}
