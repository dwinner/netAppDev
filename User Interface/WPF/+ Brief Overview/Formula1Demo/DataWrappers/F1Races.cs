using System.Collections.Generic;
using System.Linq;
using Formula1.Data.Orm;

namespace Formula1Demo.DataWrappers
{
   public sealed class F1Races
   {
      private IEnumerable<dynamic> _cache;
      private int _lastPageSearched = -1;

      public IEnumerable<dynamic> GetRaces(int aPage, int aPageSize)
      {
         using (var f1Context = new Formula1Entities())
         {
            if (_lastPageSearched == aPage)
               return _cache;

            _lastPageSearched = aPage;
            var dynQuery = GetRaces(f1Context).Skip(aPage * aPageSize).Take(aPageSize);
            _cache = dynQuery.ToList();

            return _cache;
         }
      }

      private static IQueryable<dynamic> GetRaces(Formula1Entities aDataContext) =>
         from race in aDataContext.Races
         from raceResult in race.RaceResults
         orderby race.Date ascending
         select new
         {
            race.Date.Year,
            race.Circuit.Country,
            raceResult.Position,
            Racer = $"{raceResult.Racer.FirstName} {raceResult.Racer.LastName}",
            Car = aDataContext.Teams.FirstOrDefault(team => team.Id == raceResult.TeamId).Name,
            raceResult.Points
         };
   }
}