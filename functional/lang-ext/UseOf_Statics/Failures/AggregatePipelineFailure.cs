using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UseOf_Statics.Failures;

public class AggregatePipelineFailure : IAmFailure
{
   public AggregatePipelineFailure(IEnumerable<IAmFailure> failures)
   {
      frequencies = new Dictionary<IAmFailure, int>();
      var failureNames = failures.GroupBy(o => o.GetType().Name + o.Reason);
      var sb = new StringBuilder();
      foreach (var name in failureNames)
      {
         var lookupFailure = failures.First(o => (o.GetType().Name + o.Reason).Equals(name.Key));
         if (!frequencies.ContainsKey(lookupFailure))
         {
            frequencies.Add(lookupFailure, name.Count());
         }

         sb.Append($"Failure name: {name.Key} Count: {name.Count()}\n");
      }

      Reason = sb.ToString();
   }

   public Dictionary<IAmFailure, int> frequencies { get; set; }
   public string Reason { get; set; }
   public static IAmFailure Create(IEnumerable<IAmFailure> failures) => new AggregatePipelineFailure(failures);
}