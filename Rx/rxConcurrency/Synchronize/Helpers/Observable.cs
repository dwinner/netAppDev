using System;
using System.Reactive.Linq;

namespace Synchronize.Helpers;

public static class ObservableEx
{
   public static IObservable<T> FromValues<T>(params T[] values) => values.ToObservable();
}