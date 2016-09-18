using System;

namespace ReflectionToString
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine(new CustomerReflection
         {
            Id = Guid.NewGuid(),
            Age = 20,
            FirstName = "Joe",
            LastName = "Smith"
         });
      }
   }
}