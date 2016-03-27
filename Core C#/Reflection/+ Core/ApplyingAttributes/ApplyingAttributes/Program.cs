using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplyingAttributes
{
   class Program
   {
      static void Main(string[] args)
      {
         HorseAndBuggy mule = new HorseAndBuggy();
      }
   }

   [Serializable] // Этот класс может сохраняться на диске.
   public class Motorcycle
   {
      [NonSerialized]   // Это поле сохраняться не будет.
      float weightOfCurrentPassengers;
      // Эти поля сериализованы.
      bool hasRadioSystem;
      bool hasHeadSet;
      bool hasSissyBar;
   }

   [Serializable, Obsolete("Use another vehicle!")]
   public class HorseAndBuggy
   {
   }

   [Serializable]
   [Obsolete("Use another vehicle!")]
   public class HorseAndBuggy2
   {
   }     
}
