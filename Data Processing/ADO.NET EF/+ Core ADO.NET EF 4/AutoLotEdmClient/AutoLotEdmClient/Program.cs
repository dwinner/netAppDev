using System;
using System.Data.Objects;
using System.Linq;
using AutoLotEntityLibrary;

namespace AutoLotEdmClient
{
   public static class Program
   {
      static void Main()
      {
         PrintCustomerData("4");
         CallStoredProc();
         Console.ReadKey();
      }

      private static void PrintCustomerData(string custId)  // Навигационные свойства
      {
         int id = int.Parse(custId);
         using (var entities = new AutoLotEntities())
         {
            var carsOnOrders = from o in entities.Orders
                               where o.CustId == id
                               select o.Inventory;
            Console.WriteLine("Customer has {0} orders pending:", carsOnOrders.Count());
            foreach (var carsOnOrder in carsOnOrders)
            {
               Console.WriteLine("{0} {1} named {2}",
                  carsOnOrder.Color, carsOnOrder.Make, carsOnOrder.PetName);
            }
         }
      }

      private static void CallStoredProc()   // Вызов хранимой процедуры
      {
         using (var entities = new AutoLotEntities())
         {
            // ObjectParameter inputParameter = new ObjectParameter("CarId", 83);
            var outputParameter = new ObjectParameter("petName", typeof(string));
            /*int petName = */entities.GetPetName(83, outputParameter);
            Console.WriteLine("Car #83 is named {0}", outputParameter.Value);
         }
      }
   }
}
