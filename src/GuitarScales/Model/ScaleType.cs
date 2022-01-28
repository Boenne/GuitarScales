using System.Collections.Generic;
using System.Linq;

namespace GuitarScales.Model;

public abstract class ScaleType
{
    public abstract int[] Pattern { get; }
    public abstract int[] ArpeggioPattern { get; }
    public abstract string Name { get; }

    public List<Note> CreateScaleNotes(Note rootNote)
    {
        var notesFollowingKey = rootNote.GetFollowingNotes();
        var list = Pattern.Select(i => notesFollowingKey[i]).ToList();
        return list;
    }

    public List<Note> CreateArpeggioScaleNotes(Note rootNote)
    {
        var notesFollowingKey = rootNote.GetFollowingNotes();
        var list = ArpeggioPattern.Select(i => notesFollowingKey[i]).ToList();
        return list;
    }

    public List<Note> GetRootNoteOfScaleFromNoteCandidates(Note note, List<Note> noteCandidates)
    {
        var scales = new List<Note>();
        var currentNote = note;
        do
        {
            var scaleNotes = CreateScaleNotes(currentNote);
            var all = noteCandidates.All(noteCandidate => scaleNotes.Any(x => x.Equals(noteCandidate)));
            if (all)
            {
                scales.Add(currentNote);
            }

            currentNote = currentNote.Next;
        } while (!currentNote.Equals(note));

        return scales;
    }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object obj)
    {
        var scaleType = obj as ScaleType;
        if (scaleType == null)
        {
            return false;
        }

        return Name == scaleType.Name;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}