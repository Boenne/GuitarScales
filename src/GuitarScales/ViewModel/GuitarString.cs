using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GuitarScales.Messenger;
using GuitarScales.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace GuitarScales.ViewModel;

public class GuitarString : ObservableRecipient
{
    private readonly IMessengerWrapper _messenger;
    private readonly ObservableCollection<NoteViewModel> _notes = new();
    private Scale _selectedScale;
    private Note _selectedTune;
    private bool _show = true;
    private List<Note> _tunings;

    public GuitarString(IMessengerWrapper messenger)
    {
        _messenger = messenger;
    }

    public IEnumerable<NoteViewModel> Notes => _notes;

    public Note TempTune { get; set; }

    public bool Show
    {
        get => _show;
        set => SetProperty(ref _show, value);
    }

    public Note SelectedTune
    {
        get => _selectedTune;
        set
        {
            if (SelectedTune != null && SelectedTune.Equals(value))
            {
                return;
            }

            SetProperty(ref _selectedTune, value);
            TuneChanged();
        }
    }

    public Scale SelectedScale
    {
        get => _selectedScale;
        set
        {
            _selectedScale = value;
            SelectNotes();
        }
    }

    public List<Note> Tunings
    {
        get => _tunings;
        set => SetProperty(ref _tunings, value);
    }

    public void TuneChanged()
    {
        _notes.Clear();
        foreach (var tune in SelectedTune.GetNotesForString())
        {
            _notes.Add(new NoteViewModel(_messenger) { Note = tune, NoteSelection = GetNoteSelection(tune) });
        }
    }

    public void Tune()
    {
        SelectedTune = TempTune;
        TempTune = null;
    }

    private void SelectNotes()
    {
        foreach (var noteViewModel in _notes)
        {
            noteViewModel.NoteSelection = GetNoteSelection(noteViewModel.Note);
        }
    }

    private NoteSelection GetNoteSelection(Note note)
    {
        if (SelectedScale == null)
        {
            return NoteSelection.NotSelected;
        }

        if (SelectedScale.RootNote.Equals(note))
        {
            return NoteSelection.RootNote;
        }

        if (SelectedScale.Notes.Any(note.Equals))
        {
            return NoteSelection.NoteInScale;
        }

        return NoteSelection.NotSelected;
    }
}