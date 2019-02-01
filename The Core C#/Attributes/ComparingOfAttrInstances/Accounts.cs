using System;

namespace ComparingOfAttrInstances
{
   [Flags]
   internal enum Accounts
   {
      Savings = 0x1,
      Checking = 0x2,
      Brokerage = 0x4,
      All = Savings | Checking | Brokerage
   }
}