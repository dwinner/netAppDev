using System.Diagnostics.Contracts;

namespace Invariants
{
   [ContractOption(category: "runtime", setting: "checking", enabled: true)]
   partial class Program
   {
      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
         Contract.Invariant(_x > 5);
      }
   }
}