using System;
using System.Threading;
using System.Threading.Tasks;
using AutolotWCFClient.AutolotServiceReference;

namespace AutolotWCFClient
{
   class Program
   {
      static void Main()
      {
         using (AutoLotServiceClient client = new AutoLotServiceClient())
         {
            client.Open();
            Task<InventoryRecord[]> retrieveTask = client.GetInventoryAsync();
            while (!retrieveTask.IsCompleted)
            {
               Console.WriteLine("Task is executing...");
               Thread.Sleep(100);
            }
            InventoryRecord[] inventoryRecords = retrieveTask.Result;
            Console.WriteLine("Task is done");
            foreach (InventoryRecord inventoryRecord in inventoryRecords)
            {
               Console.WriteLine("PetName:\t{0}", inventoryRecord.PetName);
            }            
         }

         Console.Write("Press any key to continue...");
         Console.ReadKey();
      }
   }
}
