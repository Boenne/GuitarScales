using System.Collections.Generic;
using GuitarScales.Model;
using GuitarScales.ViewModel;

namespace GuitarScales;

public static class GuitarStringsExtensions
{
    public static void DownTune(this List<GuitarString> strings, int steps)
    {
        foreach (var guitarString in strings)
        {
            for (var i = 0; i < steps; i++)
            {
                guitarString.TempTune = guitarString.TempTune?.Previous ?? guitarString.SelectedTune.Previous;
            }
        }
    }

    public static void ToStandardTune(this List<GuitarString> strings)
    {
        strings[0].TempTune = strings[0].SelectedTune.GetNote(typeof(E));
        strings[1].TempTune = strings[1].SelectedTune.GetNote(typeof(B));
        strings[2].TempTune = strings[2].SelectedTune.GetNote(typeof(G));
        strings[3].TempTune = strings[3].SelectedTune.GetNote(typeof(D));
        strings[4].TempTune = strings[4].SelectedTune.GetNote(typeof(A));
        strings[5].TempTune = strings[5].SelectedTune.GetNote(typeof(E));
    }
}