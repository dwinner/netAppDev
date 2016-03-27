/**
 * Обработка ошибок и инфраструктура отмены в потоках данных
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using static System.Tuple;

namespace ErrorHandling
{
   internal static class Program
   {
      private static void Main()
      {
         UnhandledException();
         InternalExceptionHandling();
         ExternallyExceptionHandling();
         PropagatingAndHandlingExceptions();

         var divideBlock =
            new ActionBlock<Tuple<int, int>>(
               input =>
               {
                  Thread.Sleep(10);
                  Console.WriteLine(input.Item1 / input.Item2);
               },
               new ExecutionDataflowBlockOptions
               {
                  MaxDegreeOfParallelism = 2
               });

         divideBlock.Post(Create(10, 5));
         divideBlock.Post(Create(20, 4));
         divideBlock.Post(Create(10, 0));
         divideBlock.Post(Create(10, 2));
         divideBlock.Post(Create(50, 5));
         divideBlock.Post(Create(10, 5));
         divideBlock.Post(Create(20, 4));


         //   Console.Read();

         try
         {
            divideBlock.Completion.Wait();
         }
         catch (AggregateException errors)
         {
            foreach (var error in errors.InnerExceptions)
            {
               Console.WriteLine(error.Message);
            }
         }
      }

      #region Простая отмена в блоках

      private static void SingleBlockCancellation()
      {
         var cancellationTokenSource = new CancellationTokenSource();

         var slowAction = new ActionBlock<int>(
            i =>
            {
               Console.WriteLine("{0}:Started", i);
               Thread.Sleep(1000);
               //cts.Token.WaitHandle.WaitOne(1000);
               //cts.Token.ThrowIfCancellationRequested();
               Console.WriteLine("{0}:Done", i);
            },
            new ExecutionDataflowBlockOptions { CancellationToken = cancellationTokenSource.Token });

         slowAction.Post(1);
         slowAction.Post(2);
         slowAction.Post(3);

         slowAction.Complete();
         slowAction.Completion.ContinueWith(sab => Console.WriteLine("Blocked finished in state of {0}", sab.Status),
            cancellationTokenSource.Token);

         Console.ReadLine();
         cancellationTokenSource.Cancel();

         Console.ReadLine();
      }

      #endregion

      #region Связанная отмена

      private static void LinkedCancellation()
      {
         var tokenSource = new CancellationTokenSource();

         var slowTransformBlock = new TransformBlock<int, string>(
            i =>
            {
               Console.WriteLine("{0}:Started", i);
               tokenSource.Token.WaitHandle.WaitOne(1000);
               tokenSource.Token.ThrowIfCancellationRequested();
               Console.WriteLine("{0}:Done", i);
               return i.ToString();
            },
            new ExecutionDataflowBlockOptions { CancellationToken = tokenSource.Token });

         var printActionBlock = new ActionBlock<string>((Action<string>)Console.WriteLine,
            new ExecutionDataflowBlockOptions { CancellationToken = tokenSource.Token });

         slowTransformBlock.LinkTo(printActionBlock, new DataflowLinkOptions { PropagateCompletion = true });

         slowTransformBlock.Post(1);
         slowTransformBlock.Post(2);
         slowTransformBlock.Post(3);

         //    slowTransform.Complete();
         Console.ReadLine();
         tokenSource.Cancel();

         printActionBlock.Completion.ContinueWith(pat => Console.WriteLine(pat.Status), tokenSource.Token);

         Console.ReadLine();
      }

      #endregion

      #region Продвижение ошибки с последующей обработкой

      private static void PropagatingAndHandlingExceptions()
      {
         var divideBlock = new TransformBlock<Tuple<int, int>, int>(input => input.Item1 / input.Item2);
         var printingBlock = new ActionBlock<int>((Action<int>)Console.WriteLine);

         divideBlock.LinkTo(printingBlock, new DataflowLinkOptions { PropagateCompletion = true });

         divideBlock.Post(Create(10, 5));
         divideBlock.Post(Create(20, 4));
         divideBlock.Post(Create(10, 0));
         divideBlock.Post(Create(10, 2));

         divideBlock.Complete();

         try
         {
            printingBlock.Completion.Wait();
         }
         catch (AggregateException errors)
         {
            foreach (var error in errors.Flatten().InnerExceptions)
            {
               Console.WriteLine("Divide block failed Reason:{0}", error.Message);
            }
         }
      }

      #endregion

      #region Обработка исключения задачей продолжения

      private static void ExternallyExceptionHandling()
      {
         var divideBlock = new ActionBlock<Tuple<int, int>>(input => Console.WriteLine(input.Item1 / input.Item2),
            new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 4 });

         divideBlock.Post(Create(10, 5));
         divideBlock.Post(Create(20, 0));
         divideBlock.Post(Create(10, 0));
         divideBlock.Post(Create(10, 2));

         divideBlock.Completion.ContinueWith(
            errorTask =>
            {
               var innerExceptions = errorTask.Exception?.Flatten().InnerExceptions;
               if (innerExceptions == null)
                  return;

               foreach (var error in innerExceptions)
               {
                  Console.WriteLine("Divide block failed Reason:{0}", error.Message);
               }
            }, TaskContinuationOptions.OnlyOnFaulted);

         Console.ReadLine();
      }

      #endregion

      #region Обработка исключения внутри блока

      private static void InternalExceptionHandling()
      {
         var divideBlock =
            new ActionBlock<Tuple<int, int>>(
               delegate (Tuple<int, int> pair)
               {
                  try
                  {
                     Console.WriteLine(pair.Item1 / pair.Item2);
                  }
                  catch (DivideByZeroException)
                  {
                     Console.WriteLine("Dude, can't divide by zero");
                  }
               });

         divideBlock.Post(Create(10, 5));
         divideBlock.Post(Create(20, 4));
         divideBlock.Post(Create(10, 0));
         divideBlock.Post(Create(10, 2));

         Console.ReadLine();
      }

      #endregion

      #region Необработанное исключение

      private static void UnhandledException()
      {
         var divideBlock = new ActionBlock<Tuple<int, int>>(input => Console.WriteLine(input.Item1 / input.Item2));
         divideBlock.Post(Create(10, 5));
         divideBlock.Post(Create(20, 4));
         divideBlock.Post(Create(10, 0));
         divideBlock.Post(Create(10, 2));
      }

      #endregion

      #region Квота при отмене блоков

      private static void FirstAttempt()
      {
         var cts = new CancellationTokenSource();

         var divideBlock =
            new TransformBlock<Tuple<int, int>, int>(pair => pair.Item1 / pair.Item2,
               new ExecutionDataflowBlockOptions
               {
                  CancellationToken = cts.Token
               });

         var divideConsumer = new ActionBlock<int>((result =>
         {
            Console.WriteLine(result);

            Thread.Sleep(500);
         }), new ExecutionDataflowBlockOptions { BoundedCapacity = 11, CancellationToken = cts.Token });

         divideBlock.LinkTo(divideConsumer, new DataflowLinkOptions { PropagateCompletion = true });

         divideBlock.Completion.ContinueWith(dbt => divideConsumer.Complete(), cts.Token);

         var rnd = new Random();
         cts.CancelAfter(TimeSpan.FromSeconds(2));

         for (var i = 0; i < 10; i++)
         {
            // divideBlock.Post(new Tuple<int, int>(rnd.Next(0, 10), rnd.Next(0, 10)));
            divideBlock.Post(new Tuple<int, int>(rnd.Next(1, 10), rnd.Next(1, 10)));
            //Thread.Sleep(250);
         }

         divideBlock.Complete();
         divideBlock.Completion.Wait();
         Console.WriteLine("Posting done");
         divideConsumer.Completion.Wait();
      }

      #endregion
   }
}