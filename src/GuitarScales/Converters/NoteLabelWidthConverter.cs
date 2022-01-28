using System;
using System.Globalization;
using System.Windows.Data;

namespace GuitarScales.Converters;

public class NoteLabelWidthConverter : IMultiValueConverter
{
    public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
    {
        var gridWidth = (double)value[0];
        var labelWidth = (double)value[1];
        if (labelWidth >= gridWidth)
        {
            return 22.0;
        }

        return labelWidth;
    }

    public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}