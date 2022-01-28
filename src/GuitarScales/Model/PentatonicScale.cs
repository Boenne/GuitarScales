namespace GuitarScales.Model;

public class PentatonicScale : ScaleType
{
    public override int[] Pattern => new[] { 0, 3, 5, 7, 10 };
    public override int[] ArpeggioPattern => new[] { 0, 5, 10 };
    public override string Name => "Pentatonic";
}