using System;
using System.Globalization;
using System.Windows.Data;

namespace GuitarScales.Converters;

public class FretNumberConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var fret = (int)value + 1;
        if (fret == 1 ||
            fret == 3 ||
            fret == 5 ||
            fret == 7 ||
            fret == 9 ||
            fret == 12 ||
            fret == 15 ||
            fret == 17 ||
            fret == 19 ||
            fret == 21 ||
            fret == 24)
        {
            return fret.ToString();
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}