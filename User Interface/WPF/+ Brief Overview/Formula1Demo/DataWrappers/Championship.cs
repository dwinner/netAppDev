using System;
using System.Collections.Generic;
using System.Linq;
using Formula1.Data.Orm;

namespace Formula1Demo.DataWrappers
{
   public sealed class Championship
   {
      public int Year { get; set; }

      public Lazy<IEnumerable<F1Race>> Races => new Lazy<IEnumerable<F1Race>>(GetRaces);

      private IEnumerable<F1Race> GetRaces()
      {
         using (var f1Context = new Formula1Entities())
         {
            return
            (from race in f1Context.Races
               where race.Date.Year == Year
               orderby race.Date
               select new F1Race
               {
                  Date = race.Date,
                  Country = race.Circuit.Country
               }).ToList();
         }
      }
   }
}