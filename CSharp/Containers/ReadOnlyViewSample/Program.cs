using System.Collections.ObjectModel;

namespace ReadOnlyViewSample;

internal class Program
{
   private static void Main()
   {
      var t = new Test();

      Console.WriteLine(t.Names.Count); // 0
      t.AddInternally();
      Console.WriteLine(t.Names.Count); // 1

      // Compiler error
      //t.Names.Add("test");                    
      ((IList<string>)t.Names).Add("test"); // NotSupportedException
   }
}

public class Test
{
   private readonly List<string> _names;

   public Test()
   {
      _names = new List<string>();
      Names = new ReadOnlyCollection<string>(_names);
   }

   public ReadOnlyCollection<string> Names { get; }//_names.AsReadOnly()

   public void AddInternally()
   {
      _names.Add("test");
   }
}