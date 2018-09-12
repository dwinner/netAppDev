using System;

namespace GenericFactory
{
   public sealed class ConcreteProduct : ProductBase
   {
      protected internal override void PostConstruct()
         => Console.WriteLine($"{nameof(ConcreteProduct)} post construction");
   }
}