using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace GuitarScales.Converters;

public class FretLengthConverter : IValueConverter
{
    private static readonly List<decimal> FretsLength = CalculateFretLengths();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var index = (int)value;
        var width = FretsLength[index] * 3;
        return width;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static List<decimal> CalculateFretLengths()
    {
        var frets = new List<decimal>();
        const decimal scaleLengthInMm = 635;
        decimal distance = 0;
        decimal previous = 0;
        for (var fret = 0; fret <= 24; fret++)
        {
            var location = scaleLengthInMm - distance;
            var scalingFactor = location / 17.817m;
            distance = distance + scalingFactor;
            if (previous != 0)
            {
                frets.Add(distance - previous);
            }

            previous = distance;
        }

        frets.Add(distance - previous);
        return frets;
    }
}