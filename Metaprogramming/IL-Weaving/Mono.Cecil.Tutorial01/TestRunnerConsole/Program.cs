using System;
using TestLibrary;

namespace TestRunnerConsole
{
   internal static class Program
   {
      private static void Main()
      {
         //Example 1 - returning a fake string
         Console.WriteLine("Example 1" + Environment.NewLine + "---------");
         Console.WriteLine(HelloMessages.GetHelloWorld());
         Console.WriteLine(HelloMessages.GetSumMessage(121, 46));

         //Example 2 - throwing a simple exception
         Console.WriteLine(Environment.NewLine + "Example 2" + Environment.NewLine + "---------");
         var person = new Person("John", new DateTime(1981, 12, 1));

         try
         {
            var age = person.GetAge();
            Console.WriteLine("This person's age is " + age);
         }
         catch
         {
            Console.WriteLine("An exception was thrown when calling the GetAge method.");
         }

         //Example 3 Throwing a custom exception
         Console.WriteLine(Environment.NewLine + "Example 3" + Environment.NewLine + "---------");
         var charles = new Person("Charles", new DateTime(1970, 12, 20)) {Occupation = "Programmer"};

         try
         {
            var intro = charles.GetIntro();
            Console.WriteLine(intro);
         }
         catch (MySillyException exception)
         {
            Console.WriteLine("An exception of type MySillyException was thrown.");
            Console.WriteLine("Message: " + exception.Message);
            Console.WriteLine("Number: " + exception.Number);
         }

         //Example 4
         Console.WriteLine(Environment.NewLine + "Example 4" + Environment.NewLine + "---------");
         Console.WriteLine("My name is: " + charles.Name);

         Console.WriteLine("Whose occupation is: " + charles.Occupation);

         Console.ReadKey();
      }
   }
}