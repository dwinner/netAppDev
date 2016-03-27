/**
 * Таблица на тип, отслеживание состояния сущностей.
 */

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Formula1Demo
{
   internal class Program
   {
      private static void Main()
      {
         //EagerLoadingDemo();
         ChangeInformation();
      }

      #region Немедленная загрузка связанных сущностей

      private static void EagerLoadingDemo()
      {
         using (var entities = new Formula1v2Entities())
         {
            foreach (Racer racer in entities.Racers.Include("RaceResults.Race.Circuit"))
            {
               Console.WriteLine("{0} {1}", racer.FirstName, racer.LastName);
               foreach (RaceResult raceResult in racer.RaceResults)
               {
                  Console.WriteLine("\t{0} {1:d} {2}", raceResult.Race.Circuit.Name, raceResult.Race.Date,
                     raceResult.Position);
               }
            }
         }
      }

      #endregion

      #region Изменение информации, хранение и отслеживание изменений

      private static void ChangeInformation()
      {
         using (var data = new Formula1v2Entities())
         {
            var esteban = data.Racers.Create();
            esteban.FirstName = "Esteban";
            esteban.LastName = "Gutierrez";
            esteban.Nationality = "Mexico";
            esteban.Starts = 0;
            data.Racers.Add(esteban);

            var fernando = data.Racers.First(racer => racer.LastName == "Alonso");
            fernando.Wins++;
            fernando.Starts++;

            foreach (var entry in data.ChangeTracker.Entries<Racer>())
            {
               Console.WriteLine("{0}, state: {1}", entry.Entity, entry.State);
               if (entry.State == EntityState.Modified)
               {
                  Console.WriteLine("Original values");
                  DbPropertyValues values = entry.OriginalValues;
                  foreach (var propertyName in values.PropertyNames)
                  {
                     Console.WriteLine("{0} {1}", propertyName, values[propertyName]);
                  }
                  Console.WriteLine();

                  Console.WriteLine("Current values");
                  values = entry.CurrentValues;
                  foreach (var propertyName in values.PropertyNames)
                  {
                     Console.WriteLine("{0} {1}", propertyName, values[propertyName]);
                  }
               }
            }            
         }
      }

      #endregion

      #region Отслеживание изменений для EF5

      /*private static void TrackingDemo()
      {
         using (var data = new Formula1Entities())
         {
            data.ObjectStateManager.ObjectStateManagerChanged += ObjectStateManager_ObjectStateManagerChanged;


            Racer niki1 = (from r in data.Racers
                           where r.Nationality == "Austria" && r.LastName == "Lauda"
                           select r).First();

            Racer niki2 = (from r in data.Racers
                           where r.Nationality == "Austria"
                           orderby r.Wins descending
                           select r).First();

            if (Object.ReferenceEquals(niki1, niki2))
            {
               Console.WriteLine("the same object");
            }
         }
      }
       
      private static void ObjectStateManager_ObjectStateManagerChanged(object sender, CollectionChangeEventArgs e)
      {
         Console.WriteLine("Object State change — action: {0}", e.Action);
         Racer r = e.Element as Racer;
         if (r != null)
            Console.WriteLine("Racer {0}", r.LastName);
      }
       
      private static void DetachDemo()
      {
         using (var data = new Formula1Entities())
         {
            data.ObjectStateManager.ObjectStateManagerChanged += ObjectStateManager_ObjectStateManagerChanged;
            ObjectQuery<Racer> racers = data.Racers.Where("it.Lastname='Alonso'");
            Racer fernando = racers.First();
            EntityKey key = fernando.EntityKey;
            data.Racers.Detach(fernando);
            // Racer is now detached and can be changed independent of the 
            // object context
            fernando.Starts++;
            Racer originalObject = data.GetObjectByKey(key) as Racer;
            data.Racers.ApplyCurrentValues(fernando);
         }
      }
       
      */

      #endregion

      static void DisplayState(IEnumerable<DbEntityEntry> entityEntries)
      {
         foreach (var racer in entityEntries.Select(entry => entry.Entity).OfType<Racer>())
         {
            Console.WriteLine(racer.FirstName);
         }
      }

/*
      static void DisplayState(string state, IEnumerable<ObjectStateEntry> entries)
      {
         foreach (var racer in entries.Select(entry => entry.Entity).OfType<Racer>())
         {
            Console.WriteLine("{0}: {1}", state, racer.LastName);
         }
      }
*/
   }
}