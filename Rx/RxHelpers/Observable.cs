using System.Reactive.Linq;

namespace RxHelpers;

public static class ObservableEx
{
   public static IObservable<T> FromValues<T>(params T[] values) => values.ToObservable();
}