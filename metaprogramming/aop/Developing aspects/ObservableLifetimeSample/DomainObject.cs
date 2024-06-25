using System;

namespace ObservableLifetimeSample
{
   [ObservableLifetime]
   public class DomainObject : IDisposable
   {
      private readonly string _tag;

      public DomainObject(string tag)
      {
         _tag = tag;
      }

      protected virtual void Dispose(bool disposing) => Console.WriteLine("Dispose({0})", disposing);

      public void Dispose() => Dispose(true);

      public override string ToString() => $"{{{_tag}}}";
   }
}