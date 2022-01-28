using GuitarScales.Model;

namespace GuitarScales;

public interface INoteInitializer
{
    Note Initialize();
}

public class NoteInitializer : INoteInitializer
{
    public Note Initialize()
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

        return ab;
    }
}