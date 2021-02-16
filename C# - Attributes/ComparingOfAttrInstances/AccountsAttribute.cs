using System;

namespace ComparingOfAttrInstances
{
   [AttributeUsage(AttributeTargets.Class)]
   internal sealed class AccountsAttribute : Attribute
   {
      private readonly Accounts _accounts;

      public AccountsAttribute(Accounts accounts)
      {
         _accounts = accounts;
      }

      public override bool Match(object obj)
         => obj != null && GetType() == obj.GetType() && (((AccountsAttribute) obj)._accounts & _accounts) == _accounts;

      public override bool Equals(object obj)
         => obj != null && GetType() == obj.GetType() && ((AccountsAttribute)obj)._accounts == _accounts;      

      public override int GetHashCode() => (int) _accounts;
   }
}