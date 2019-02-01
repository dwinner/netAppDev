/**
 * Immutable collections sample
 */

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static System.Console;

namespace ImmutableCollectionSample
{
   public class Program
   {
      public static void Main()
      {
         SimpleArrayDemo();
         var accounts = CreateImmutableList();
         UsingBuilder(accounts);
         LinqDemo();

         ReadLine();
      }

      private static void LinqDemo()
      {
         var arr = ImmutableArray.Create("one", "two", "three", "four", "five");
         var result = arr.Where(s => s.StartsWith("t"));
         WriteLine(result.FirstOrDefault());
      }

      private static void UsingBuilder(ImmutableList<Account> accounts)
      {
         var builder = accounts.ToBuilder();
         for (var i = 0; i < builder.Count; i++)
         {
            var account = builder[i];
            if (account.Amount > 0)
            {
               builder.Remove(account);
            }
         }

         var overdrawnAccounts = builder.ToImmutable();
         overdrawnAccounts.ForEach(account => WriteLine($"{account.Name} {account.Amount}"));
      }

      private static void SimpleArrayDemo()
      {
         var a1 = ImmutableArray.Create<string>();
         var a2 = a1.Add("Williams");
         var a3 = a2.Add("Ferrarri").Add("Mersedes").Add("Red Bull Racing");
         foreach (var element in a3)
         {
            WriteLine(element);
         }
      }

      private static ImmutableList<Account> CreateImmutableList()
      {
         var accounts = new List<Account>
         {
            new Account("Scrooge McDuck", 667377678765m),
            new Account("Donald Duck", -200m),
            new Account("Ludwig von Drake", 20000m)
         };

         var immutableAccounts = accounts.ToImmutableList();

         foreach (var account in immutableAccounts)
         {
            WriteLine($"{account.Name} {account.Amount}");
         }

         immutableAccounts.ForEach(account => WriteLine($"{account.Name} {account.Amount}"));

         return immutableAccounts;
      }
   }
}