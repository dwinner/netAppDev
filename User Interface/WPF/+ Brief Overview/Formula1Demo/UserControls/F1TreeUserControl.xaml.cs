using System.Collections.Generic;
using System.Linq;
using Formula1.Data.Orm;
using Formula1Demo.DataWrappers;

namespace Formula1Demo.UserControls
{
   public partial class F1TreeUserControl
   {
      private List<Championship> _years;

      public F1TreeUserControl()
      {
         InitializeComponent();
         DataContext = Years;
      }

      public List<Championship> Years => _years ?? (_years = GetYears());

      private static List<Championship> GetYears()
      {
         using (var f1Context = new Formula1Entities())
         {
            return
               f1Context.Races.Select(race => new Championship {Year = race.Date.Year})
                  .Distinct()
                  .OrderBy(championship => championship.Year)
                  .ToList();
         }
      }
   }
}