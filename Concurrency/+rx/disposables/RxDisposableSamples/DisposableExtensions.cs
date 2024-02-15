using System.Reactive.Disposables;

namespace RxDisposableSamples;

public static class DisposableExtensions
{
   public static CompositeDisposable AddToCompositeDisposable(this IDisposable @this,
      CompositeDisposable compositeDisposable)
   {
      if (compositeDisposable == null)
      {
         throw new ArgumentNullException(nameof(compositeDisposable));
      }

      compositeDisposable.Add(@this);
      return compositeDisposable;
   }
}