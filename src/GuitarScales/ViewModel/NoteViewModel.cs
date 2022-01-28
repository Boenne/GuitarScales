using GuitarScales.Messenger;
using GuitarScales.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace GuitarScales.ViewModel;

public class NoteViewModel : ObservableRecipient
{
    private readonly IMessengerWrapper _messenger;
    private Note _note;
    private NoteSelection _noteSelection;
    private bool _wasInScale;

    public NoteViewModel(IMessengerWrapper messenger)
    {
        _messenger = messenger;
    }

    public RelayCommand NoteClickCommand => new(NoteClick);

    public Note Note
    {
        get => _note;
        set => SetProperty(ref _note, value);
    }

    public NoteSelection NoteSelection
    {
        get => _noteSelection;
        set => SetProperty(ref _noteSelection, value);
    }

    public void NoteClick()
    {
        if (NoteSelection == NoteSelection.NoteInScale)
        {
            _wasInScale = true;
        }

        if (_wasInScale && NoteSelection == NoteSelection.UserSelected)
        {
            NoteSelection = NoteSelection.NoteInScale;
        }
        else
        {
            NoteSelection = NoteSelection == NoteSelection.UserSelected
                ? NoteSelection.NotSelected
                : NoteSelection.UserSelected;
        }

        _messenger.Send(new NoteSelectedMessage
        {
            Note = _note,
            DeSelected = NoteSelection != NoteSelection.UserSelected
        });
    }

    public override string ToString()
    {
        return Note.Name;
    }
}