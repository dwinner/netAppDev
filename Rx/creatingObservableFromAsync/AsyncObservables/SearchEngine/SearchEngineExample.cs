using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace AsyncObservables.SearchEngine
{
   internal static class SearchEngineExample
   {
      public static IObservable<string> Search_WithAsyncAwait(string term) =>
         Observable.Create<string>(async o =>
         {
            var searchEngineA = new SearchEngineA();
            var searchEngineB = new SearchEngineB();
            var resultsA = await searchEngineA.SearchAsync(term)
               .ConfigureAwait(false);
            foreach (var result in resultsA)
            {
               o.OnNext(result);
            }

            var resultsB = await searchEngineB.SearchAsync(term)
               .ConfigureAwait(false);
            foreach (var result in resultsB)
            {
               o.OnNext(result);
            }

            o.OnCompleted();
         });

      public static IObservable<string> Search_WithCancellation(string term) =>
         Observable.Create<string>(async (o, cancellationToken) =>
         {
            var searchEngineA = new SearchEngineA();
            var searchEngineB = new SearchEngineB();
            var resultsA = await searchEngineA.SearchAsync(term)
               .ConfigureAwait(false);
            foreach (var result in resultsA)
            {
               cancellationToken.ThrowIfCancellationRequested();
               o.OnNext(result);
            }

            cancellationToken.ThrowIfCancellationRequested();
            var resultsB = await searchEngineB.SearchAsync(term)
               .ConfigureAwait(false);
            foreach (var result in resultsB)
            {
               cancellationToken.ThrowIfCancellationRequested();
               o.OnNext(result);
            }

            o.OnCompleted();
         });

      public static IObservable<string> Search_ConcatingTasks(string term)
      {
         var searchEngineA = new SearchEngineA();
         var searchEngineB = new SearchEngineB();
         var resultsA = searchEngineA.SearchAsync(term).ToObservable();
         var resultsB = searchEngineB.SearchAsync(term).ToObservable();
         return resultsA
            .Concat(resultsB)
            .SelectMany(x => x);
      }

      public static IObservable<string> Search_DefferedConcatingTasks(string term)
      {
         var searchEngineA = new SearchEngineA();
         var searchEngineB = new SearchEngineB();
         var resultsA = Observable.Defer(() => searchEngineA.SearchAsync(term).ToObservable());
         var resultsB = Observable.Defer(() => searchEngineB.SearchAsync(term).ToObservable());
         return resultsA
            .Concat(resultsB)
            .SelectMany(x => x);
      }
   }
}