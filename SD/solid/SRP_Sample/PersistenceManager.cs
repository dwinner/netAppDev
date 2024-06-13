namespace SRP_Sample;

// handles the responsibility of persisting objects
public class PersistenceManager
{
   public void SaveToFile(Journal journal, string filename, bool overwrite = false)
   {
      if (overwrite || !File.Exists(filename))
      {
         File.WriteAllText(filename, journal.ToString());
      }
   }
}