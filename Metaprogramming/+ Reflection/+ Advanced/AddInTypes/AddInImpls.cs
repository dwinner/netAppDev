// Реализации компонента

using HostSdk;

namespace AddInTypes
{
   public sealed class AddInA : IAddIn
   {
      public string DoSomething(int x)
      {
         return $"{nameof(AddInA)}: {x}";
      }
   }

   public sealed class AddInB : IAddIn
   {
      public string DoSomething(int x)
      {
         return $"{nameof(AddInB)}: {(x * 2)}";
      }
   }
}