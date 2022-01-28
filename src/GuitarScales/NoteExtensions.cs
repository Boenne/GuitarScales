using System;
using System.Collections.Generic;
using GuitarScales.Model;

namespace GuitarScales;

public static class NoteExtensions
{
    public static List<Note> GetNotesForString(this Note tune)
    {
        var currentNote = tune;
        var notes = new List<Note>();
        for (var i = 0; i < 24; i++)
        {
            notes.Add(currentNote.Next);
            currentNote = currentNote.Next;
        }

        return notes;
    }

    public static Note GetNote(this Note note, Type noteType)
    {
        var currentNote = note;
        do
        {
            if (currentNote.GetType() == noteType)
            {
                return currentNote;
            }

            currentNote = currentNote.Next;
        } while (!currentNote.Equals(note));

        throw new Exception($"Note not found: {noteType}");
    }

    public static List<Note> GetFollowingNotes(this Note note)
    {
        var currentNote = note;
        var notes = new List<Note>();
        do
        {
            notes.Add(currentNote);
            currentNote = currentNote.Next;
        } while (!currentNote.Equals(note));

        return notes;
    }
}