using MethodBoundarySample.Aspects;

namespace MethodBoundarySample
{
   public sealed class Customer
   {
      [ObjectInitializationAspect]
      public string Name { get; set; }
      
      [ObjectInitializationAspect]
      public string Address { get; set; }

      [ObjectInitializationAspect]
      public string Company { get; set; }
   }
}