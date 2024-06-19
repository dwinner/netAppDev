namespace CopyThroughSerialization;

//[Serializable] // this is, unfortunately, required
public class Foo
{
   public uint Stuff;
   public string? Whatever;

   public override string ToString() => $"{nameof(Stuff)}: {Stuff}, {nameof(Whatever)}: {Whatever}";
}