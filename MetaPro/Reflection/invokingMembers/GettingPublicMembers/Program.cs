namespace GettingPublicMembers;

internal static class Program
{
   private static void Main()
   {
      var members = typeof(Walnut).GetMembers();
      foreach (var memberInfo in members)
      {
         Console.WriteLine(memberInfo);
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
}