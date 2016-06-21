namespace ValidateResourceString
{
   using System;
   using System.Resources;

   internal class Program
   {
      private const string ResourceName = "ValidateResourceString.MyResource"; // nameof(MyResource)

      private static readonly ResourceManager _ResourceManager = new ResourceManager(ResourceName,
                                                                                     typeof (Program).Assembly);

      private static void Main()
      {
         // These two method calls are valid.
         Console.WriteLine(GetResourceString("String1"));
         Console.WriteLine(GetResourceString("String2"));

         // There is a warning for the following line because Strong3 is not a valid string name.
         Console.WriteLine(GetResourceString("Strong3"));
      }

      private static string GetResourceString([ValidateResourceString(ResourceName)] string key)
      {
         return _ResourceManager.GetString(key) ?? "*** Error ***";
      }
   }
}