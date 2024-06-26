﻿/**
 * Анонимные методы
 */

using System;

namespace _06_AnonymMethodsDemo
{
   class Program
   {
      static void Main(string[] args)
      {
         // Создать массив целых чисел
         int[] integers = new int[] { 1, 2, 3, 4 };

         Processor proc = new Processor();
         proc.Strategy = delegate(int x)
         {
            return x * 2;
         };
         PrintArray(proc.Process(integers));

         proc.Strategy = delegate(int x)
         {
            return x * 4;
         };
         PrintArray(proc.Process(integers));

         proc.Strategy = delegate { return 0; };
         PrintArray(proc.Process(integers));

         Console.ReadLine();
      }

      private static void PrintArray(int[] array)
      {
         for (int i = 0; i < array.Length; i++)
         {
            Console.Write("{0}", array[i]);
            if (i != array.Length - 1)
            {
               Console.Write(", ");
            }
         }
         Console.WriteLine(Environment.NewLine);
      }
   }

   public delegate int ProcStrategy(int x);

   public class Processor
   {
      private ProcStrategy strategy;

      public ProcStrategy Strategy { set { strategy = value; } }

      public Processor(ProcStrategy procStrategy)
      {
         strategy = procStrategy;
      }

      public Processor() { }

      public int[] Process(int[] array)
      {
         int[] result = new int[array.Length];
         for (int i = 0; i < array.Length; i++)
         {
            result[i] = strategy(array[i]);
         }
         return result;
      }
   }
}
