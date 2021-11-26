using Customers.Lib;

namespace ReflectionToString
{
   public sealed class CustomerReflection : Customer
   {
      public override string ToString() => this.ToStringReflection();
   }
}