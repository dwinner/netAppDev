using Composite.Implementation;

namespace Composite.IO
{
   public static class DataCreator
   {
      public static Project<IProjectItem> CreateTestData()
      {
         // Creating the contacts
         IContact firstContact =
            new ContactImpl("Dennis", "Moore", "Managing Director", "Highway Man, LTD");
         IContact secondContact =
            new ContactImpl("Joseph", "Mongolfier", "High Flyer", "Ligher than Air Productions");
         IContact thirdContact =
            new ContactImpl("Erik", "Njoll", "Nomad without Portfolio", "Nordik Trek Inc.");
         IContact fourthContact =
            new ContactImpl("Lemming", "", "Principal Investigator", "BDA");

         // Creating the project
         var project =
            new Project<IProjectItem>("IslandParadise", "Acquire a personal island paradise");
         var firstDeliverable =
            new Deliverable("Island Paradise", "", firstContact);
         var firstWorkTask =
            new WorkTask<IProjectItem>("Fortune", "Acquire a small fortune", fourthContact, 11.0);
         var secondWorkTask =
            new WorkTask<IProjectItem>("Isle", "Locate an island for sale", secondContact, 7.5);
         var thirdTask =
            new WorkTask<IProjectItem>("Name", "Decide on a name for the island", thirdContact, 3.2);

         // Adding tasks to the projects
         project.Add(firstDeliverable, firstWorkTask, secondWorkTask, thirdTask);

         // Adding subtasks to the first task
         var underFirstDeliverable =
            new Deliverable("$1,000,000", "(total net worth after taxes)", firstContact);
         var underFirstTask1 = new WorkTask<IProjectItem>("Fortune1",
            "Use psychic hotline to predict winning lottery numbers", fourthContact, 2.5);
         var underFirstTask2 = new WorkTask<IProjectItem>("Fortune2",
            "Invest winnings to ensure 50% annual interest", firstContact, 14.0);
         firstWorkTask.Add(underFirstTask1).Add(underFirstTask2).Add(underFirstDeliverable);

         // Adding subtasks to the second task
         var underSecondTask1 =
            new WorkTask<IProjectItem>("Isle1",
            "Research whether climate is better in the Altantic or Pacific", firstContact, 1.8);
         var underSecondTask2 =
            new WorkTask<IProjectItem>("Isle2", "Locate an island for auction on EBay",
            fourthContact, 5.0);
         var underSecondTask3
            = new WorkTask<IProjectItem>("Isle2a", "Negotiate for sale of the island", thirdContact, 17.5);
         secondWorkTask.Add(underSecondTask1).Add(underSecondTask2).Add(underSecondTask3);

         // Adding the manager of the third task
         var deliverable31 = new Deliverable("Island Name", string.Empty, firstContact);
         thirdTask.Add(deliverable31);

         return project;
      }
   }
}