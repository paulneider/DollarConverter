using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DollarConverterClient;

internal class StringToDoubleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.ToString() ?? DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string strVal)
            return double.Parse(strVal, new CultureInfo("de-de"));
        return DependencyProperty.UnsetValue;
    }
}
