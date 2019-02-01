/**
 * - Доступ к указателям
 * - Ускорение доступа к массивам
 * - Предотвращение перемещения объектов в памяти
 */

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pointers
{
   class Program
   {
      static void Main()
      {
         unsafe
         {
            // Note: Доступ к указателям
            int x = 0;
            int* pX = &x;
            *pX = 13;

            Console.WriteLine("x = {0}", x);

            Point pt;
            Point* pPt = &pt;
            pPt->X = 13;
            pPt->Y = 14;
            pPt->Offset(1, 2);

            Console.WriteLine("pt = {0}", pt);

            // ReSharper disable once UnusedVariable
            List<object> list = new List<object>();
            //List<object>* pList = &list;//won't compile!

            // Note: Ускорение доступа к массивам
            const int size = 10;
            int[] vals = new int[size];
            try
            {
               for (int i = 0; i < size + 1; i++)
               {
                  vals[i] = i;
               }
            }
            catch (IndexOutOfRangeException ex)
            {
               Console.WriteLine("Caught exception: " + ex.Message);
            }

            // Арифметика указателей

            Console.WriteLine("Pointer arithmetic");
            fixed (int* pI = &vals[0])
            {
               int* pA = pI;
               while (*pA < 8)
               {
                  // Увеличить на 2 * sizeof(element)

                  pA += 2;
                  Console.WriteLine("*pA = {0}", *pA);
               }
            }

            // Note: Предотвращение перемещения объектов в памяти

            Console.WriteLine("Going out of bounds");

            // Местоположение значений зафиксировано на время выполнения блока

            fixed (int* pI = &vals[0])
            {
               // Какая досада! Мы зашли слишком далеко и испортили данные
               // в памяти, которая нам не принадлежит

               for (int i = 0; i < size + 1; i++)
               {
                  pI[i] = i;
               }
               Console.WriteLine("No exception thrown! We just overwrote memory we shouldn't have!");
            }
         }

         Console.ReadKey();
      }
   }
}
