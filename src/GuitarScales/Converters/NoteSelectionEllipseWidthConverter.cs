using System;
using System.Globalization;
using System.Windows.Data;

namespace GuitarScales.Converters;

public class NoteSelectionEllipseWidthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var gridWidth = (double)value;
        if (gridWidth < 35)
        {
            return gridWidth;
        }

        return 35.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}