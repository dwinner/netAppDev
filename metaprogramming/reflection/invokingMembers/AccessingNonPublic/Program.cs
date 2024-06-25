using System.Reflection;

namespace AccessingNonPublic;

internal static class Program
{
   private static void Main()
   {
      var t = typeof(Walnut);
      var w = new Walnut();
      w.Crack();
      var f = t.GetField("cracked", BindingFlags.NonPublic | BindingFlags.Instance);
      f.SetValue(w, false);
      Console.WriteLine(w); // False

      var publicStatic = BindingFlags.Public | BindingFlags.Static;
      var members = typeof(object).GetMembers(publicStatic);
      foreach (var member in members)
      {
         Console.WriteLine(member);
      }

      // The following example retrieves all the nonpublic members of type object, both static and instance:
      var nonPublicBinding =
         BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;

      var nonPublic = typeof(object).GetMembers(nonPublicBinding);
      foreach (var member in nonPublic)
      {
         Console.WriteLine(nonPublic);
      }
   }
}

internal class Walnut
{
   private bool cracked;

   public void Crack()
   {
      cracked = true;
   }

   public override string ToString() => cracked.ToString();
}