using System;
// ReSharper disable UnusedVariable
#pragma warning disable 169
#pragma warning disable 618

namespace ApplyingAttributes
{
   internal static class Program
   {
      private static void Main()
      {
         var mule = new HorseAndBuggy();
      }
   }

   [Serializable] // Этот класс может сохраняться на диске.
   public class Motorcycle
   {
      private bool _hasHeadSet;

      // Эти поля сериализованы.
      private bool _hasRadioSystem;

      private bool _hasSissyBar;

      [NonSerialized] // Это поле сохраняться не будет.
      private float _weightOfCurrentPassengers;
   }

   [Serializable]
   [Obsolete("Use another vehicle!")]
   public class HorseAndBuggy
   {
   }

   [Serializable]
   [Obsolete("Use another vehicle!")]
   public class HorseAndBuggy2
   {
   }
}