/*
 * Сделано в SharpDevelop.
 * Пользователь: Denis
 * Дата: 30.11.2012
 * Время: 8:08
 *  
 */
using System;

namespace SimpleException
{
   public class Radio
   {
      public Radio()
      {
      }

      public void TurnOn(bool on)
      {
         if (on)
            Console.WriteLine("Jamming...");
         else
            Console.WriteLine("Quiet time...");
      }
   }
}
