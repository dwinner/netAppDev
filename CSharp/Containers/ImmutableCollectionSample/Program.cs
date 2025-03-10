﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

SimpleArrayDemo();
ImmutableList<Account> accounts = CreateImmutableList();
UsingABuilder(accounts);
LinqDemo();

Console.ReadLine();

return;

void LinqDemo()
{
   var arr = ImmutableArray.Create<string>("one", "two", "three", "four", "five");
   var result = arr.Where(s => s.StartsWith("t"));
   Console.WriteLine(result.Count());
}

void UsingABuilder(ImmutableList<Account> immutableAccounts)
{
   ImmutableList<Account>.Builder builder = immutableAccounts.ToBuilder();
   for (var i = builder.Count - 1; i >= 0; i--)
   {
      Account a = builder[i];
      if (a.Amount > 0)
      {
         builder.Remove(a);
      }
   }

   ImmutableList<Account> overdrawnAccounts = builder.ToImmutable();

   overdrawnAccounts.ForEach(a => Console.WriteLine($"overdrawn: {a.Name} {a.Amount}"));
}

ImmutableList<Account> CreateImmutableList()
{
   List<Account> accountList = new()
   {
      new Account("Scrooge McDuck", 667377678765m),
      new Account("Donald Duck", -200m),
      new Account("Ludwig von Drake", 20000m)
   };

   ImmutableList<Account> immutableAccounts = accountList.ToImmutableList();
   foreach (var (name, amount) in immutableAccounts)
   {
      Console.WriteLine($"{name} {amount}");
   }

   immutableAccounts.ForEach(a => Console.WriteLine($"{a.Name} {a.Amount}"));

   return immutableAccounts;
}

void SimpleArrayDemo()
{
   var a1 = ImmutableArray.Create<string>();
   var a2 = a1.Add("Williams");
   var a3 = a2.Add("Ferrari").Add("Mercedes").Add("Red Bull Racing");
}

public record Account(string Name, decimal Amount);