using Customers.Builders;
using Customers.Lib;

namespace Customers
{
   public sealed class CustomerDependencyInjected
      : Customer
   {
      public CustomerDependencyInjected(IToStringBuilder builder) => Builder = builder;

      private IToStringBuilder Builder { get; }

      public override string ToString() => Builder.ToString(this);
   }
}