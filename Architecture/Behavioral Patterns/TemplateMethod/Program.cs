/**
 * Шаблонный метод
 */

using System;

namespace TemplateMethod
{
   static class Program
   {
      static void Main()
      {
         Console.WriteLine("Example for the Template Method pattern");
         Console.WriteLine("This code demonstrates how the template method can");
         Console.WriteLine(" be used to define a variable implementation for a");
         Console.WriteLine(" common operation. In this case, the ProjectItem");
         Console.WriteLine(" abstract class defines the method getCostEstimate,");
         Console.WriteLine(" which is a combination of the cost for time and");
         Console.WriteLine(" materials. The two concrete subclasses used here,");
         Console.WriteLine(" Task and Deliverable, have different methods of");
         Console.WriteLine(" providing a cost estimate.");
         Console.WriteLine();
         Console.WriteLine("Creating a demo Task and Deliverable");
         Console.WriteLine();

         var primaryTask = new Task("Put a JVM on the moon", "Lunar mission as part of the JavaSpace program ;)", 240.0,
            100.0);
         primaryTask.Add(new Task("Establish ground control", "", 1000.0, 10.0));
         primaryTask.Add(new Task("Train the Javanaughts", "", 80.0, 30.0));
         var deliverableOne = new Deliverable("Lunar landing module",
            "Ask the local garage if they can make a few minor modifications to one of their cars", 2800, 40.0, 35.0);

         Console.WriteLine("Calculating the cost estimates using the Template Method, getCostEstimate.");
         Console.WriteLine();
         Console.WriteLine("Total cost estimate for: " + primaryTask);
         Console.WriteLine("\t" + primaryTask.CostEstimate());
         Console.WriteLine();
         Console.WriteLine("Total cost estimate for: " + deliverableOne);
         Console.WriteLine("\t" + deliverableOne.CostEstimate());
      }
   }
}
