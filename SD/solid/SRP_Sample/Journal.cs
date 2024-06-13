namespace SRP_Sample;

// just stores a couple of journal entries and ways of
// working with them
public class Journal
{
   private static int _count;
   private readonly List<string> _entries = [];

   public int AddEntry(string text)
   {
      _entries.Add($"{++_count}: {text}");
      return _count; // memento pattern!
   }

   public void RemoveEntry(int index)
   {
      _entries.RemoveAt(index);
   }

   public override string ToString() => string.Join(Environment.NewLine, _entries);

   #region breaks single responsibility principle

/*
   public void Save(string filename, bool overwrite = false)
   {
      File.WriteAllText(filename, ToString());
   }

   public void Load(string filename)
   {
   }

   public void Load(Uri uri)
   {
   }
*/

   #endregion
}