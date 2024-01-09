using System.Diagnostics.CodeAnalysis;

namespace RequiredMemberSample;

internal static class Program
{
   private static void Main()
   {
      // In the following example, the House class chooses not to implement a convenience constructor:

      var h1 = new House { Name = "House" }; // OK
      //House h2 = new House();                   // Error!

      var a1 = new Asset { Name = "House" }; // OK
      //Asset a2 = new Asset();                   // Error: will not compile!
   }
}

public class Asset
{
   public required string Name;

   public Asset()
   {
   }

   [SetsRequiredMembers]
   public Asset(string n) => Name = n;
}

public class House : Asset; // No constructor, no worries!