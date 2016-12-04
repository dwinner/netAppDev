/**
 * Декоратор - динамическое добавление нового поведения для объектов
 */

using System;

namespace Decorator
{
   internal static class Program
   {
      static void Main()
      {
         // Creating of the project elements
         IContact firstContact = new ContactImpl
         {
            FirstName = "Simone",
            LastName = "Roberto",
            Title = "Head Researcher and Chief Archivist",
            Organization = "Institute for Advanced (Java) Studies"
         };

         var firstTask =
				new ProjectTask<IProjectItem>(
					"Perform months of diligent research", firstContact, 20.0);
         var secondTask =
				new ProjectTask<IProjectItem>(
					"Obtain grant from World Java Foundation", firstContact, 40.0);

         // Creating decorators
         ProjectDecorator firstDecorator = new SupportedProjectItem("JavaHistory.txt");
         ProjectDecorator secondDecorator = new DependentProjectItem(secondTask);

         // Adding decorators to the first task
         firstDecorator.ProjectItem = firstTask;
         secondDecorator.ProjectItem = firstDecorator;

         Console.WriteLine(secondDecorator);
         Console.ReadLine();
      }
   }
}
