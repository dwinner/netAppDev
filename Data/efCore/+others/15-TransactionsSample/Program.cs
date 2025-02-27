﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace _15_TransactionsSample
{
   internal static class Program
   {
      private static void Main()
      {
         Setup();
         AddTwoRecordsWithOneTx();
         AddTwoRecordsWithTwoTx();
         TwoSaveChangesWithOneTx();
      }

      private static void Setup()
      {
         using var context = new MenusContext();
         var deleted = context.Database.EnsureDeleted();
         var deletedText = deleted ? "deleted" : "does not exist";
         Console.WriteLine($"database {deletedText}");

         var created = context.Database.EnsureCreated();

         var createdText = created ? "created" : "already exists";
         Console.WriteLine($"database {createdText}");

         var card = new MenuCard {Title = "Meat"};
         var m1 = new Menu {MenuCard = card, Text = "Wiener Schnitzel", Price = 12.90m};
         var m2 = new Menu {MenuCard = card, Text = "Goulash", Price = 8.80m};
         card.Menus.AddRange(new[] {m1, m2});
         context.MenuCards.Add(card);

         var records = context.SaveChanges();
         Console.WriteLine($"{records} records added");
      }

      private static void AddTwoRecordsWithOneTx()
      {
         Console.WriteLine(nameof(AddTwoRecordsWithOneTx));

         try
         {
            using var context = new MenusContext();
            var card = context.MenuCards.First();
            var m1 = new Menu
            {
               MenuCardId = card.MenuCardId, 
               Text = "added", 
               Price = 99.99m
            };

            var hightestCardId = context.MenuCards.Max(c => c.MenuCardId);
            var mInvalid = new Menu {MenuCardId = ++hightestCardId, Text = "invalid", Price = 999.99m};
            context.Menus.AddRange(m1, mInvalid);

            Console.WriteLine("trying to add one invalid record to the database, this should fail...");
            var records = context.SaveChanges();
            Console.WriteLine($"{records} records added");
         }
         catch (DbUpdateException ex)
         {
            Console.WriteLine($"{ex.Message}");
            Console.WriteLine($"{ex.InnerException?.Message}");
         }

         Console.WriteLine();
      }

      private static void AddTwoRecordsWithTwoTx()
      {
         Console.WriteLine(nameof(AddTwoRecordsWithTwoTx));

         try
         {
            using var context = new MenusContext();

            Console.WriteLine(
               "adding two records with two transactions to the database. One record should be written, the other not....");
            var card = context.MenuCards.First();
            var m1 = new Menu {MenuCardId = card.MenuCardId, Text = "added", Price = 99.99m};

            context.Menus.Add(m1);
            var records = context.SaveChanges();
            Console.WriteLine($"{records} records added");

            var hightestCardId = context.MenuCards.Max(c => c.MenuCardId);
            var mInvalid = new Menu {MenuCardId = ++hightestCardId, Text = "invalid", Price = 999.99m};
            context.Menus.Add(mInvalid);

            records = context.SaveChanges();
            Console.WriteLine($"{records} records added");
         }
         catch (DbUpdateException ex)
         {
            Console.WriteLine($"{ex.Message}");
            Console.WriteLine($"{ex.InnerException?.Message}");
         }

         Console.WriteLine();
      }

      private static void TwoSaveChangesWithOneTx()
      {
         Console.WriteLine(nameof(TwoSaveChangesWithOneTx));

         IDbContextTransaction tx = null;

         try
         {
            using var context = new MenusContext();
            using (tx = context.Database.BeginTransaction())
            {
               Console.WriteLine("using one explicit transaction, writing should roll back...");
               var card = context.MenuCards.First();
               var m1 = new Menu {MenuCardId = card.MenuCardId, Text = "added with explicit tx", Price = 99.99m};

               context.Menus.Add(m1);
               var records = context.SaveChanges();
               Console.WriteLine($"{records} records added");

               var hightestCardId = context.MenuCards.Max(c => c.MenuCardId);
               var mInvalid = new Menu {MenuCardId = ++hightestCardId, Text = "invalid", Price = 999.99m};
               context.Menus.Add(mInvalid);

               records = context.SaveChanges();
               Console.WriteLine($"{records} records added");

               tx.Commit();
            }
         }
         catch (DbUpdateException ex)
         {
            Console.WriteLine($"{ex.Message}");
            Console.WriteLine($"{ex.InnerException?.Message}");

            Console.WriteLine("rolling back...");
            tx?.Rollback();
         }

         Console.WriteLine();
      }
   }
}