using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using GuitarScales.Model;

namespace GuitarScales.Converters;

public class NotesToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var notes = value as List<Note>;
        var res = notes?.Aggregate("", (current, note) => current + note.Name + ", ");
        return res?.Substring(0, res.Length - 2);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}