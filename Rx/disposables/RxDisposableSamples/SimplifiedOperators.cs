using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace RxDisposableSamples;

public static class SimplifiedOperators
{
   public static IObservable<T> Return<T>(T value) =>
      Observable.Create<T>(o =>
      {
         o.OnNext(value);
         o.OnCompleted();
         return Disposable.Empty;
      });

   public static IObservable<TSource> MySubscribeOn<TSource>(this IObservable<TSource> source, IScheduler scheduler) =>
      Observable.Create<TSource>(observer =>
      {
         var disposable = new SerialDisposable();
         disposable.Disposable = scheduler.Schedule(() =>
         {
            disposable.Disposable = new ScheduledDisposable(scheduler, source.SubscribeSafe(observer));
         });

         return disposable;
      });
}