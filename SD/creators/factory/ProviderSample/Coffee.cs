﻿namespace ProviderSample;

internal class Coffee : IHotDrink
{
   public void Consume()
   {
      Console.WriteLine("This coffee is delicious!");
   }
}