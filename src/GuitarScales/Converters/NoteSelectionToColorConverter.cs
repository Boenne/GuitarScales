using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using GuitarScales.ViewModel;

namespace GuitarScales.Converters;

public class NoteSelectionToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var noteSelection = (NoteSelection)value;
        switch (noteSelection)
        {
            case NoteSelection.NotSelected:
                return new SolidColorBrush(Colors.White);
            case NoteSelection.NoteInScale:
                return new SolidColorBrush(Colors.Black);
            case NoteSelection.RootNote:
                return new SolidColorBrush(Colors.DarkGray);
            case NoteSelection.UserSelected:
                return new SolidColorBrush(Colors.Green);
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}