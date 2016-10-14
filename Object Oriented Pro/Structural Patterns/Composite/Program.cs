using Composite.Implementation;
using Composite.IO;
/**
 * Компоновщик объектов
 */

using System;

namespace Composite
{
   static class Program
   {
      static void Main()
      {
         #region Описание

         //Console.WriteLine("Example for the Composite pattern");
         //Console.WriteLine();
         //Console.WriteLine("This code sample will propagate a method call throughout");
         //Console.WriteLine(" a tree structure. The tree represents a project, and is");
         //Console.WriteLine(" composed of three kinds of ProjectItems - composite.Project, composite.Task,");
         //Console.WriteLine(" and composite.Deliverable. Of these three classes, composite.Project and composite.Task");
         //Console.WriteLine(" can store an Linked list of ProjectItems. This means that");
         //Console.WriteLine(" they can act as branch nodes for our tree. The composite.Deliverable");
         //Console.WriteLine(" is a terminal node, since it cannot hold any ProjectItems.");
         //Console.WriteLine();
         //Console.WriteLine("In this example, the method defined by composite.ProjectItem,");
         //Console.WriteLine(" getTimeRequired, provides the method to demonstrate the");
         //Console.WriteLine(" pattern. For branch nodes, the method will be passed on");
         //Console.WriteLine(" to the children. For terminal nodes (Deliverables), a");
         //Console.WriteLine(" single value will be returned.");
         //Console.WriteLine();
         //Console.WriteLine("Remember, that it is possible to make this method call ANYWHERE");
         //Console.WriteLine(" in the tree, since all classes implement the getTimeRequired");
         //Console.WriteLine(" method. This means that you are able to calculate the time");
         //Console.WriteLine(" required to complete the whole project OR any part of it.");
         //Console.WriteLine();

         //Console.WriteLine("Deserializing a test composite.Project for the Composite pattern");
         //Console.WriteLine();

         #endregion

         Project<IProjectItem> project = DataCreator.CreateTestData();
         var dataSerializer = new DataSerializer(project);
         dataSerializer.Store();
         Project<IProjectItem> retrieve = dataSerializer.Retrieve();

         Console.WriteLine("Вычисляем время для проекта");
         Console.WriteLine("\t{0}", retrieve.Description);
         Console.WriteLine("Time required: {0}", project.TimeRequired);

         Console.ReadKey();
      }
   }
}
