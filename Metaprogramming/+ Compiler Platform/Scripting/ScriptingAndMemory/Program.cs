using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Nito.AsyncEx;

namespace ScriptingAndMemory
{
   internal static class Program
   {
      private static void Main()
      {
         //EvaluateRandomExpressions();
         AsyncContext.Run(MainAsync);
      }

      private static async Task MainAsync()
      {
         await EvaluateRandomScriptAsync().ConfigureAwait(false);
      }

      private static async Task EvaluateRandomScriptAsync()
      {
         var random = new Random();
         var iterations = 0;
         var stopwatch = Stopwatch.StartNew();

         while (true)
         {
            var script = $@"({random.Next(1000)} + {random.Next(1000)}) * {random.Next(10000)}";
            var result = await CSharpScript.EvaluateAsync(script).ConfigureAwait(false);
            Console.WriteLine(result);
            iterations++;

            if (iterations == 1000)
            {
               stopwatch.Stop();
               Console.WriteLine($"{Environment.WorkingSet} - time: {stopwatch.Elapsed}");
               stopwatch = Stopwatch.StartNew();
               iterations = 0;
            }
         }
         // ReSharper disable once FunctionNeverReturns
      }

      private static void EvaluateRandomExpressions()
      {
         var random = new Random();
         var iterations = 0;
         var stopwatch = Stopwatch.StartNew();

         while (true)
         {
            var lambda = Expression.Lambda(
               Expression.Multiply(
                  Expression.Add(
                     Expression.Constant(random.Next(1000)),
                     Expression.Constant(random.Next(1000))),
                  Expression.Constant(random.Next(10000))));
            var func = lambda.Compile() as Func<int>;
            if (func != null)
            {
               var result = func();
               Console.WriteLine(result);
               iterations++;

               if (iterations == 1000)
               {
                  stopwatch.Stop();
                  Console.WriteLine($"{Environment.WorkingSet} - time: {stopwatch.Elapsed}");
                  stopwatch = Stopwatch.StartNew();
                  iterations = 0;
               }
            }
         }
         // ReSharper disable once FunctionNeverReturns
      }
   }
}