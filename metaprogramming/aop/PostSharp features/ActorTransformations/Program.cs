// Реализация модели акторов

using System;
using System.Threading.Tasks;
using PostSharp.Patterns.Threading;

namespace ActorTransformations
{
   internal class Program : IObservable<float>, IDisposable
   {
      public void Dispose()
      {
      }

      public IDisposable Subscribe(IObserver<float> observer)
      {
         observer.OnNext(42f);
         Console.WriteLine("Subscribed");
         return this;
      }

      private static void Main()
      {
         var program = new Program();
         program.MainAsync().GetAwaiter().GetResult();
      }

      private async Task MainAsync()
      {
         var calculator = new AverageCalculator();
         var observer = new SampleObserver(calculator);
         Subscribe(observer);
         Subscribe(observer);
         var averageValue = await calculator.GetAverageAsync().ConfigureAwait(false);
         Console.WriteLine("Average: {0}", averageValue);
      }
   }

   [Actor]   
   public class AverageCalculator
   {
      private int _count;
      private float _sum;

      public void AddSample(float n)
      {
         _count++;
         _sum += n;
      }

#pragma warning disable 1998
      [Reentrant]
      public async Task<float> GetAverageAsync()
#pragma warning restore 1998
      {
         return _sum / _count;
      }
   }

   public class SampleObserver : IObserver<float>
   {
      private readonly AverageCalculator _averageCalculator;

      public SampleObserver(AverageCalculator averageCalculator)
      {
         _averageCalculator = averageCalculator;
      }

      public void OnNext(float value)
      {
         // NOTE: Любой поставщик данных может вызвать этот метод из разных потоков и/или параллельно
         // Но мы можем не беспокоиться о гонках, потому что методы ставятся в очередь на выполнение
         // в ответственность одного актора

         _averageCalculator.AddSample(value);
      }

      public void OnError(Exception error)
      {
      }

      public void OnCompleted()
      {
      }
   }
}