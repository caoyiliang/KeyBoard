using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace KeyBoard
{
    internal class MultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new MultiParameter { Parameter1 = (Key)values[0], Parameter2 = (bool)values[1] };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
