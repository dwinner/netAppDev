using System;
using System.Reactive.Linq;

namespace ErrorsInThePipeline.Helpers;

public static class ObservableEx
{
   public static IObservable<T> FromValues<T>(params T[] values) => values.ToObservable();
}