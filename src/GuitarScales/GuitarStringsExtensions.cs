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
                guitarString.TempTuning = guitarString.TempTuning?.Previous ?? guitarString.SelectedTuning.Previous;
            }
        }
    }

    public static void ToStandardTune(this List<GuitarString> strings, bool finalizeTuning = false)
    {
        strings[0].TempTuning = strings[0].SelectedTuning.GetNote(typeof(E));
        strings[1].TempTuning = strings[1].SelectedTuning.GetNote(typeof(B));
        strings[2].TempTuning = strings[2].SelectedTuning.GetNote(typeof(G));
        strings[3].TempTuning = strings[3].SelectedTuning.GetNote(typeof(D));
        strings[4].TempTuning = strings[4].SelectedTuning.GetNote(typeof(A));
        strings[5].TempTuning = strings[5].SelectedTuning.GetNote(typeof(E));
        
        if(finalizeTuning)
            strings.ForEach(x => x.Tune());
    }
}