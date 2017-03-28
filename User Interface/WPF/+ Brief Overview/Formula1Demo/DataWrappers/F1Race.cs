using System;
using System.Collections.Generic;
using System.Linq;
using Formula1.Data.Orm;

namespace Formula1Demo.DataWrappers
{
   public sealed class F1Race
   {
      public string Country { get; set; }
      public DateTime Date { get; set; }

      public Lazy<IEnumerable<F1RaceResult>> Results => new Lazy<IEnumerable<F1RaceResult>>(GetResults);

      private IEnumerable<F1RaceResult> GetResults()
      {
         using (var f1Context = new Formula1Entities())
         {
            return GetRaceResults(f1Context).ToList();
         }
      }

      private IEnumerable<F1RaceResult> GetRaceResults(Formula1Entities f1Context) =>
         from raceResult in f1Context.RaceResults
         where raceResult.Race.Date == Date
         select new F1RaceResult
         {
            Position = raceResult.Position,
            Racer = $"{raceResult.Racer.FirstName} {raceResult.Racer.LastName}",
            Car = f1Context.Teams.FirstOrDefault(team => team.Id == raceResult.TeamId).Name
         };
   }
}