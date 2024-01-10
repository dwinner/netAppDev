using System;

namespace CustomEnumerator
{
   internal class Radio
   {
      public void TurnOn(bool turnRadioOn)
      {
         if (turnRadioOn)
         {
            Console.WriteLine("Jamming...");
         }
         else
         {
            Console.WriteLine("Quiet time...");
         }
      }
   }
}