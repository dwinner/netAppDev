﻿using System;

namespace CustomException
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with Custom Exceptions *****\n");
         Car myCar = new Car("Rusty", 90);

         try
         {
            myCar.Accelerate(50);
         }
         catch (CarIsDeadException e)
         {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.ErrorTimeStamp);
            Console.WriteLine(e.CauseOfError);
         }
         Console.ReadLine();
      }

   }
}
