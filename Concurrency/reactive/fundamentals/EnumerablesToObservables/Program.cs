using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using RxCreatingObservables.Chat;
using RxHelpers;

namespace RxEnumerablesToObservables;

internal static class Program
{
   private static void Main()
   {
      EnumerableToObservable();
      SubscribingToEnumerable();
      ThrowingEnumerable();
      MergingObservableConnectionWithLoadedMessages();
      ObservableToEnumerable();
      ObservableToList();
      ObservableToDictionary();
      ObservableToLookup();
      ObservableToEnumerableWithNext();

      Console.ReadLine();
   }

   private static void ObservableToEnumerableWithNext()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Observable To Enumerable (using Next)");
      var observable = Observable.Interval(TimeSpan.FromSeconds(1));
      var enumerable = observable.Next();

      // enumerating on the enumerable - some values will be missed because the thread will sleep 
      // when they are pushed
      // only 5 items are taken, so we won't stay here forever
      foreach (var item in enumerable.Take(5))
      {
         Console.WriteLine(item);
         Thread.Sleep(TimeSpan.FromSeconds(3));
      }

      Console.WriteLine("finished iterating");
      Console.WriteLine("------------------");
   }

   private static void ObservableToLookup()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Observable To Lookup");
      // Madrid and London has the same length
      IEnumerable<string> cities = new[] { "London", "Tel-Aviv", "Tokyo", "Rome", "Madrid" };
      var lookupObservable = cities.ToObservable().ToLookup(c => c.Length);
      lookupObservable
         .Select(lookup =>
         {
            var groups = new StringBuilder();
            foreach (var grp in lookup)
            {
               groups.Append($"[Key:{grp.Key} => {grp.Count()}]");
            }

            return groups.ToString();
         })
         .SubscribeConsole("lookup");
   }

   private static void ObservableToDictionary()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Observable To Dictionary");
      IEnumerable<string> cities = new[] { "London", "Tel-Aviv", "Tokyo", "Rome" };

      // change the value to some const to see and error
      var dictionaryObservable = cities.ToObservable().ToDictionary(c => c.Length);

      dictionaryObservable
         .Select(d => string.Join(",", d))
         .SubscribeConsole("dictionary");
   }

   private static void ObservableToList()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Observable To List");
      var observable =
         Observable.Create<string>(o =>
         {
            o.OnNext("Observable");
            o.OnNext("To");
            o.OnNext("List");
            //  comment the call to OnCompleted() to see how the list is never built 
            o.OnCompleted();
            return Disposable.Empty;
         });

      var listObservable = observable.ToList();
      listObservable
         .Select(lst => string.Join(",", lst))
         .SubscribeConsole("list ready");
   }

   private static void ObservableToEnumerable()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Observable To Enumerable");

      var observable = Observable.Create<string>(o =>
      {
         o.OnNext("Observable");
         o.OnNext("To");
         o.OnNext("Enumerable");
         //  comment the call to OnCompleted() to see the thread wait
         o.OnCompleted();
         return Disposable.Empty;
      });

      var enumerable = observable.ToEnumerable();
      Console.WriteLine("got the enumerable");
      Console.WriteLine("------------------");

      foreach (var item in enumerable)
      {
         Console.WriteLine(item);
      }

      Console.WriteLine("finished iterating");
      Console.WriteLine("------------------");
   }

   private static void EnumerableToObservable()
   {
      Demo.DisplayHeader("Enumerable to Observable");

      IEnumerable<string> names = new[] { "Shira", "Yonatan", "Gabi", "Tamir" };
      var observable = names.ToObservable();

      observable.SubscribeConsole("names");
   }

   private static void SubscribingToEnumerable()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Subscribing to enumerable");

      IEnumerable<string> names = new[] { "Shira", "Yonatan", "Gabi", "Tamir" };
      names.Subscribe(new ConsoleObserver<string>("subscribe"));
   }

   private static void ThrowingEnumerable()
   {
      //this shows that exception that happen while iterating are sent to the OnError
      NumbersAndThrow()
         .ToObservable()
         .SubscribeConsole("enumerable with exception");
   }

   private static IEnumerable<int> NumbersAndThrow()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Numbers and Throw");
      yield return 1;
      yield return 2;
      yield return 3;
      throw new ApplicationException("Something Bad Happened");
      //yield return 4;
   }

   private static void MergingObservableConnectionWithLoadedMessages()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Merging ObservableConnection with loaded messages");

      var client = new ChatClient();
      var liveMessages = client.ObserveMessages("user", "pass");
      var loadedMessages = LoadMessagesFromDB();

      loadedMessages.ToObservable()
         .Concat(liveMessages)
         .SubscribeConsole("merged");

      //this another way to do the same
      liveMessages.StartWith(loadedMessages)
         .SubscribeConsole("loaded first");

      client.NotifyRecieved("live message1");
      client.NotifyRecieved("live message2");
   }

   private static IEnumerable<string> LoadMessagesFromDB()
   {
      yield return "loaded1";
      yield return "loaded2";
   }
}