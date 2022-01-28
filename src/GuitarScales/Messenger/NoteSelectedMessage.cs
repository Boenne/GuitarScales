using GuitarScales.Model;

namespace GuitarScales.Messenger
{
    public class NoteSelectedMessage
    {
        public Note Note { get; set; }
        public bool DeSelected { get; set; }
    }
}
