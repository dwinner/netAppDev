/*
 * Сравнение экземпляров атрибута
 */

using System;

namespace ComparingOfAttrInstances
{
   internal class Program
   {
      private static void Main()
      {
         CanWriteCheck(new ChildAccount());
         CanWriteCheck(new AdultAccount());
         CanWriteCheck(new Program());
      }

      private static void CanWriteCheck(object obj)
      {
         Attribute checking = new AccountsAttribute(Accounts.Checking);
         var validAccounts = Attribute.GetCustomAttribute(obj.GetType(), typeof(AccountsAttribute), false);

         // Если атрибут применен к типу и указывает на счет Checking, значит, тип может выписывать чеки
         if (validAccounts != null && checking.Match(validAccounts))
         {
            Console.WriteLine("{0} types can write checks.", obj.GetType());
         }
         else
         {
            Console.WriteLine("{0} types can NOT write checks.", obj.GetType());
         }
      }
   }

   [Accounts(Accounts.Savings)]
   internal sealed class ChildAccount
   {
   }

   [Accounts(Accounts.All)]
   internal sealed class AdultAccount
   {
   }
}