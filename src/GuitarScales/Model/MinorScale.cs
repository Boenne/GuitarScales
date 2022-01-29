namespace GuitarScales.Model;

public class MinorScale : ScaleType
{
    public override int[] Pattern => new[] { 0, 2, 3, 5, 7, 8, 10 };
    public override string Name => "Minor";
}