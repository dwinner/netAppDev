using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Formula1.Data.Orm;

namespace Formula1Demo.UserControls
{
   public partial class F1CustomGridUserControl
   {
      private const int PageSize = 50;
      private int _currentPage;

      public F1CustomGridUserControl()
      {
         InitializeComponent();
         DataContext = Races;
      }

      public IEnumerable<dynamic> Races => GetRacers();

      private IEnumerable<dynamic> GetRacers()
      {
         using (var f1Context = new Formula1Entities())
         {
            return GetRacers(f1Context).Skip(_currentPage * PageSize).Take(PageSize).ToList();
         }
      }

      private static IQueryable<dynamic> GetRacers(Formula1Entities f1Context) =>
         from race in f1Context.Races
         from raceResult in race.RaceResults
         orderby race.Date ascending
         select new
         {
            race.Date.Year,
            race.Circuit.Country,
            raceResult.Position,
            raceResult.Points,
            Racer = raceResult.Racer.FirstName + " " + raceResult.Racer.LastName,
            Car = f1Context.Teams.FirstOrDefault(team => team.Id == raceResult.TeamId).Name
         };

      private void OnPrev(object sender, RoutedEventArgs e)
      {
         if (_currentPage > 0)
         {
            _currentPage--;
            DataContext = Races;
         }
      }

      private void OnNext(object sender, RoutedEventArgs e)
      {
         _currentPage++;
         DataContext = Races;
      }
   }
}