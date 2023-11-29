using System.Reactive.Disposables;

namespace RxCreatingObservables;

public class NumbersObservable : IObservable<int>
{
   private readonly int _amount;

   public NumbersObservable(int amount) => _amount = amount;

   public IDisposable Subscribe(IObserver<int> observer)
   {
      for (var i = 0; i < _amount; i++)
      {
         observer.OnNext(i);
      }

      observer.OnCompleted();
      return Disposable.Empty;
   }
}