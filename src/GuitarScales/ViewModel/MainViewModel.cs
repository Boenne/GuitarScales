using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using GuitarScales.Messenger;
using GuitarScales.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace GuitarScales.ViewModel;

public interface IMainViewModel
{
    List<GuitarString> Strings { get; set; }
    List<Note> Notes { get; set; }
    RelayCommand ShowScaleNotesCommand { get; }
    RelayCommand ClearNotesCommand { get; }
    RelayCommand FindScaleCommand { get; }
    RelayCommand SetStringsAndTuningCommand { get; }
    Note SelectedScaleNote { get; set; }
    ScaleType SelectedScaleType { get; set; }
    List<ScaleType> Scales { get; }
    List<Scale> ScaleMatches { get; set; }
    Scale Scale { get; set; }
    int[] NumberOfFrets { get; }
    int[] NumberOfStrings { get; }
    int SelectedNumberOfStrings { get; set; }
    Dictionary<Tuning, string> Tunings { get; }
    Tuning SelectedTuning { get; set; }
    bool IsArpeggio { get; set; }
}

public class MainViewModel : ObservableRecipient, IMainViewModel
{
    private readonly List<Note> _selectedNotes = new();
    private bool _isArpeggio;
    private Note _note;
    private List<Note> _notes;
    private Scale _scale;
    private List<Scale> _scaleMatches;
    private int _selectedNumberOfStrings = 6;
    private Note _selectedScaleNote;
    private ScaleType _selectedScaleType;
    private Tuning _selectedTuning;
    private List<GuitarString> _strings;

    public MainViewModel(IMessengerWrapper messenger)
    {
        InitializeNotes();

        Notes = _note.GetFollowingNotes();
        SelectedScaleNote = Notes.First();
        SelectedScaleType = Scales.First();

        Strings = new List<GuitarString>
        {
            new(messenger) { SelectedTune = _note.GetNote(typeof(E)), Tunings = Notes },
            new(messenger) { SelectedTune = _note.GetNote(typeof(B)), Tunings = Notes },
            new(messenger) { SelectedTune = _note.GetNote(typeof(G)), Tunings = Notes },
            new(messenger) { SelectedTune = _note.GetNote(typeof(D)), Tunings = Notes },
            new(messenger) { SelectedTune = _note.GetNote(typeof(A)), Tunings = Notes },
            new(messenger) { SelectedTune = _note.GetNote(typeof(E)), Tunings = Notes }
        };

        messenger.Register<MainViewModel, NoteSelectedMessage>(this, (r, message) =>
        {
            var noteIsInList = _selectedNotes.Any(x => x.Index == message.Note.Index);
            if (noteIsInList && message.DeSelected)
            {
                _selectedNotes.RemoveAll(x => x.Index == message.Note.Index);
            }
            else if (!noteIsInList)
            {
                _selectedNotes.Add(message.Note);
            }
        });
    }

    public List<GuitarString> Strings
    {
        get => _strings;
        set => SetProperty(ref _strings, value);
    }

    public List<Note> Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }

    public RelayCommand ShowScaleNotesCommand => new(ShowScaleNotesAsync);
    public RelayCommand ClearNotesCommand => new(ClearNotes);
    public RelayCommand FindScaleCommand => new(FindScale);
    public RelayCommand SetStringsAndTuningCommand => new(SetStringsAndTuning);

    public Note SelectedScaleNote
    {
        get => _selectedScaleNote;
        set => SetProperty(ref _selectedScaleNote, value);
    }

    public ScaleType SelectedScaleType
    {
        get => _selectedScaleType;
        set => SetProperty(ref _selectedScaleType, value);
    }

    public List<ScaleType> Scales => new() { new MinorScale(), new MajorScale(), new PentatonicScale() };

    public List<Scale> ScaleMatches
    {
        get => _scaleMatches;
        set => SetProperty(ref _scaleMatches, value);
    }

    public Scale Scale
    {
        get => _scale;
        set => SetProperty(ref _scale, value);
    }

    public int[] NumberOfFrets => new int[24];
    public int[] NumberOfStrings => new[] { 4, 6 };

    public int SelectedNumberOfStrings
    {
        get => _selectedNumberOfStrings;
        set => SetProperty(ref _selectedNumberOfStrings, value);
    }

    public Dictionary<Tuning, string> Tunings
        =>
            new()
            {
                { Tuning.Standard, "Standard" },
                { Tuning.HalfStepDown, "Half step down" },
                { Tuning.DropD, "Drop D" },
                { Tuning.D, "D" },
                { Tuning.DropC, "Drop C" },
                { Tuning.DropB, "Drop B" },
                { Tuning.DropA, "Drop A" },
                { Tuning.Ukulele, "Ukulele" }
            };

    public Tuning SelectedTuning
    {
        get => _selectedTuning;
        set => SetProperty(ref _selectedTuning, value);
    }

    public bool IsArpeggio
    {
        get => _isArpeggio;
        set => SetProperty(ref _isArpeggio, value);
    }

    public void InitializeNotes()
    {
        var ab = new Ab { Index = 0 };
        var a = new A { Index = 1 };
        var bb = new Bb { Index = 2 };
        var b = new B { Index = 3 };
        var c = new C { Index = 4 };
        var db = new Db { Index = 5 };
        var d = new D { Index = 6 };
        var eb = new Eb { Index = 7 };
        var e = new E { Index = 8 };
        var f = new F { Index = 9 };
        var gb = new Gb { Index = 10 };
        var g = new G { Index = 11 };

        ab.Next = a;
        ab.Previous = g;
        a.Next = bb;
        a.Previous = ab;
        bb.Next = b;
        bb.Previous = a;
        b.Next = c;
        b.Previous = bb;
        c.Next = db;
        c.Previous = b;
        db.Next = d;
        db.Previous = c;
        d.Next = eb;
        d.Previous = db;
        eb.Next = e;
        eb.Previous = d;
        e.Next = f;
        e.Previous = eb;
        f.Next = gb;
        f.Previous = e;
        gb.Next = g;
        gb.Previous = f;
        g.Next = ab;
        g.Previous = gb;

        _note = ab;
    }

    public void SetStringsAndTuning()
    {
        Strings.ToStandardTune();
        ClearScaleMatches();
        switch (SelectedTuning)
        {
            case Tuning.DropD:
                Strings[5].TempTune = _note.GetNote(typeof(D));
                break;
            case Tuning.HalfStepDown:
                Strings.DownTune(1);
                break;
            case Tuning.D:
                Strings.DownTune(2);
                break;
            case Tuning.DropC:
                Strings.DownTune(2);
                Strings[5].TempTune = Strings[3].TempTune;
                break;
            case Tuning.Ukulele:
                Strings[2].TempTune = _note.GetNote(typeof(A));
                Strings[3].TempTune = _note.GetNote(typeof(E));
                Strings[4].TempTune = _note.GetNote(typeof(C));
                Strings[5].TempTune = _note.GetNote(typeof(G));
                break;
            case Tuning.DropB:
                Strings.DownTune(3);
                Strings[5].TempTune = Strings[3].TempTune;
                break;
            case Tuning.DropA:
                Strings.DownTune(5);
                Strings[5].TempTune = Strings[3].TempTune;
                break;
            case Tuning.Standard:
            default:
                break;
        }

        for (var i = 0; i < Strings.Count; i++)
        {
            var guitarString = Strings[i];
            if ((i == 0 || i == 1) && SelectedNumberOfStrings == 4)
            {
                guitarString.Show = false;
            }
            else
            {
                guitarString.Show = true;
            }

            guitarString.Tune();
        }
    }

    public void ShowScaleNotesAsync()
    {
        Task.Factory.StartNew(ShowScaleNotes);
    }

    public void ShowScaleNotes()
    {
        ClearScaleMatches();
        var scaleNotes = IsArpeggio
            ? SelectedScaleType.CreateArpeggioScaleNotes(SelectedScaleNote)
            : SelectedScaleType.CreateScaleNotes(SelectedScaleNote);
        Scale = new Scale { Notes = scaleNotes, RootNote = SelectedScaleNote, ScaleType = SelectedScaleType };
        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)SetScaleOnEachString);
    }

    public void FindScale()
    {
        var orderedNotes = _selectedNotes.OrderBy(x => x.Index);
        var scales = new List<Scale>();
        foreach (var scaleType in Scales)
        {
            var isPartOfScale = scaleType.GetRootNoteOfScaleFromNoteCandidates(_note, orderedNotes.ToList());
            scales.AddRange(
                isPartOfScale.Select(
                    x => new Scale { RootNote = x, ScaleType = scaleType, Notes = scaleType.CreateScaleNotes(x) }));
        }

        ScaleMatches = scales;
    }

    public void ClearNotes()
    {
        Scale = null;
        SetScaleOnEachString();
        ClearScaleMatches();
    }

    public void ClearScaleMatches()
    {
        _selectedNotes.Clear();
        ScaleMatches = new List<Scale>();
    }

    private void SetScaleOnEachString()
    {
        foreach (var guitarString in Strings)
        {
            guitarString.SelectedScale = Scale;
        }
    }
}