﻿using System;
using System.Threading.Tasks;

internal class Program
{
   private static void Main(string[] args)
   {
      if (args.Length != 1)
      {
         Usage();
         return;
      }

      switch (args[0].ToLower())
      {
         case "-donthandle":
            DontHandle();
            break;
         case "-handle":
            HandleOnError();
            break;
         case "-two":
            StartTwoTasks();
            break;
         case "-twop":
            StartTwoTasksParallel();
            break;
         case "-agg":
            ShowAggregatedException();
            break;
         default:
            Usage();
            break;
      }

      Console.ReadLine();
   }

   public static void Usage()
   {
      Console.WriteLine("Usage: ErrorHandling Command");
      Console.WriteLine();
      Console.WriteLine("Commands:");
      Console.WriteLine("\t-donthandle\t\tcall async methods with exceptions not caught");
      Console.WriteLine("\t-two\t\tstart two tasks");
      Console.WriteLine("\t-twop\t\tstart two tasks parallel");
      Console.WriteLine("\t-agg\t\taggregated exception");
      Console.WriteLine("\t");
   }

   private static async void ShowAggregatedException()
   {
      Task? aggregatedTask = null;
      try
      {
         Task t1 = ThrowAfter(2000, "first");
         Task t2 = ThrowAfter(1000, "second");
         await (aggregatedTask = Task.WhenAll(t1, t2));
      }
      catch (Exception ex)
      {
         // just display the exception information of the first task that is awaited within WhenAll
         Console.WriteLine($"handled {ex.Message}");
         if (aggregatedTask?.Exception?.InnerException is not null)
         {
            foreach (var ex1 in aggregatedTask.Exception.InnerExceptions)
            {
               Console.WriteLine($"inner exception {ex1.Message} from task {ex1.Source}");
            }
         }
      }
   }

   private static async void StartTwoTasksParallel()
   {
      try
      {
         Task t1 = ThrowAfter(2000, "first");
         Task t2 = ThrowAfter(1000, "second");
         await Task.WhenAll(t1, t2);
      }
      catch (Exception ex)
      {
         // just display the exception information of the first task that is awaited within WhenAll
         Console.WriteLine($"handled {ex.Message}");
      }
   }

   private static async void StartTwoTasks()
   {
      try
      {
         await ThrowAfter(2000, "first");
         await ThrowAfter(1000,
            "second"); // the second call is not invoked because the first method throws an exception
      }
      catch (Exception ex)
      {
         Console.WriteLine($"handled {ex.Message}");
      }
   }

   private static void DontHandle()
   {
      try
      {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
         ThrowAfter(200, "first");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
         // Exception is not caught because the exception is assigned to the task which is not awaited
      }
      catch (Exception ex)
      {
         Console.WriteLine(ex.Message);
      }
   }

   private static async void HandleOnError()
   {
      try
      {
         await ThrowAfter(2000, "first");
      }
      catch (Exception ex)
      {
         Console.WriteLine($"handled {ex.Message}");
      }
   }

   private static async Task ThrowAfter(int ms, string message)
   {
      await Task.Delay(ms);
      throw new Exception(message);
   }
}