using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OrderEntrySystem
{
    public class DoubleToStringConverter : IValueConverter
    {
        private string lastEnteredValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = lastEnteredValue ?? value.ToString();

            lastEnteredValue = null;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result;

            string stringValue = value.ToString();

            if (double.TryParse(stringValue, out result))
            {
                lastEnteredValue = stringValue;
            }

            return result;
        }
    }
}