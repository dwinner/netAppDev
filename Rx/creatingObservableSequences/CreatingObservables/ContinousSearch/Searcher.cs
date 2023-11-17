using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Timers;

namespace CreatingObservables.ContinousSearch;

internal static class Searcher
{
   public static IObservable<string> Create(string term) =>
      Observable.Create<string>(observer =>
      {
         var timer = new Timer(2000);
         timer.Elapsed += (_, _) =>
         {
            var results = SearchEngine.Search(term);
            foreach (var result in results)
            {
               observer.OnNext(result);
            }
         };

         return () =>
         {
            //timer.d
         };
      });
}

internal static class SearchEngine
{
   public static IEnumerable<string> Search(string term)
   {
      yield return $"{term}1";
      yield return $"{term}2";
      yield return $"{term}3";
   }
}