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
    private Note _selectedTuning;
    private bool _show = true;
    private List<Note> _tunings;

    public GuitarString(IMessengerWrapper messenger, Note selectedTuning, List<Note> tunings)
    {
        _messenger = messenger;
        SelectedTuning = selectedTuning;
        Tunings = tunings;
    }

    public IEnumerable<NoteViewModel> Notes => _notes;

    public Note TempTuning { get; set; }

    public bool Show
    {
        get => _show;
        set => SetProperty(ref _show, value);
    }

    public Note SelectedTuning
    {
        get => _selectedTuning;
        set
        {
            if (_selectedTuning != null && _selectedTuning.Equals(value))
            {
                return;
            }

            SetProperty(ref _selectedTuning, value);
            TuningChanged();
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

    public void TuningChanged()
    {
        _notes.Clear();
        foreach (var note in SelectedTuning.GetNotesForString())
        {
            _notes.Add(new NoteViewModel(_messenger) { Note = note, NoteSelection = GetNoteSelection(note) });
        }
    }

    public void Tune()
    {
        SelectedTuning = TempTuning;
        TempTuning = null;
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