/**
 * Цепочка ответственности
 */

using System;

namespace ChainOfResponsibility
{
   internal static class Program
   {
      private static void Main()
      {
         var data = CreateData();
         GetItemInfo(data);
      }

      private static IProjectItem CreateData()
      {
         IContact contact1 = new ContactImpl("Dennis", "Moore", "Managing Director", "Highway Man, LTD");
         IContact contact2 = new ContactImpl("Joseph", "Mongolfier", "High Flyer", "Lihgter than Air Productions");
         IContact contact3 = new ContactImpl("Erik", "Njoll", "Nomad without Portfolio", "Nordic Trek, Inc.");
         IContact contact4 = new ContactImpl("Lemming", "", "Principal Investigator", "BDA");

         var project = new Project("IslandParadise", "Acquire a personal island paradise", contact2);

         IProjectItem task1 = new ProjectTask(project, "Fortune", "Acquire a personal island paradise", contact4, true);
         IProjectItem task2 = new ProjectTask(project, "Isle", "Locate an island for sale", null, true);
         IProjectItem task3 = new ProjectTask(project, "Name", "Decide on a name for the island", contact3, false);

         project.Add(task1);
         project.Add(task2);
         project.Add(task3);

         IProjectItem task4 = new ProjectTask(task1, "Fortune1",
            "Use psychic hotline to predict winning lottery numbers", null, false);
         IProjectItem task5 = new ProjectTask(task1, "Fortune2", "Invest winnings to ensure 50% annual interest",
            contact1, true);
         IProjectItem task6 = new ProjectTask(task2, "Isle1",
            "Research whether climate is better in the Atlantic or Pacific", contact1, true);
         IProjectItem task7 = new ProjectTask(task2, "Isle2", "Locate an island for auction on EBay", null, false);
         IProjectItem task8 = new ProjectTask(task2, "Isle2a", "Negotiate for sale of the island", null, false);
         IProjectItem task9 = new ProjectTask(task3, "Name1", "Research every possible name in the world", null, true);
         IProjectItem task10 = new ProjectTask(task3, "Name2", "Eliminate any choices that are not coffee-related",
            contact4, false);

         task1.Add(task4);
         task1.Add(task5);
         task2.Add(task6);
         task2.Add(task7);
         task2.Add(task8);
         task3.Add(task9);
         task3.Add(task10);

         return project;
      }

      private static void GetItemInfo(IProjectItem item)
      {
         Console.WriteLine("Chain of project items: {0}", item);
         Console.WriteLine("Owner: {0}", item.Owner);
         Console.WriteLine("Details: {0}", item.Details);
         Console.WriteLine();
         if (item.ProjectItems != null)
         {
            var itemEnumerator = item.ProjectItems.GetEnumerator();
            while (itemEnumerator.MoveNext())
               GetItemInfo(itemEnumerator.Current);
         }
      }
   }
}