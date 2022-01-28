using System.Collections.Generic;

namespace GuitarScales.Model;

public class Scale
{
    public Note RootNote { get; set; }
    public List<Note> Notes { get; set; }
    public ScaleType ScaleType { get; set; }

    public override string ToString()
    {
        return $"{ScaleType} {RootNote}";
    }
}