namespace GuitarScales.Model;

public class MajorScale : ScaleType
{
    public override int[] Pattern => new[] { 0, 2, 4, 5, 7, 9, 11 };
    public override int[] ArpeggioPattern => new[] { 0, 4, 7 };
    public override string Name => "Major";
}