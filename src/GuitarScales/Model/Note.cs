namespace GuitarScales.Model;

public abstract class Note
{
    public abstract string Name { get; }
    public Note Next { get; set; }
    public Note Previous { get; set; }
    public int Index { get; set; }

    public override string ToString()
    {
        return Name;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        var note = obj as Note;
        if (note == null)
        {
            return false;
        }

        return Name == note.Name;
    }
}