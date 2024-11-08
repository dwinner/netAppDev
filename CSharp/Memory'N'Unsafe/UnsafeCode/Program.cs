﻿using System;

namespace UnsafeCode
{
   #region Test Points
   struct Point
   {
      public int x;
      public int y;

      public override string ToString()
      {
         return string.Format("({0}, {1})", x, y);
      }
   }

   class PointRef  // <= Renamed and retyped.
   {
      public int x;
      public int y;

      public override string ToString()
      {
         return string.Format("({0}, {1})", x, y);
      }
   }
   #endregion

   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Calling method with unsafe code *****");

         // Values for swap.
         int i = 10, j = 20;

         // Swap values "safely."
         Console.WriteLine("\n***** Safe swap *****");
         Console.WriteLine("Values before safe swap: i = {0}, j = {1}", i, j);
         SafeSwap(ref i, ref j);
         Console.WriteLine("Values after safe swap: i = {0}, j = {1}", i, j);

         // Swap values "unsafely."
         Console.WriteLine("\n***** Unsafe swap *****");
         Console.WriteLine("Values before unsafe swap: i = {0}, j = {1}", i, j);

         unsafe { UnsafeSwap(&i, &j); }

         Console.WriteLine("Values after unsafe swap: i = {0}, j = {1}", i, j);

         UsePointerToPoint();
         PrintValueAndAddress();
         UseAndPinPoint();
         UseSizeOfOperator();

         Console.ReadLine();
      }

      #region Unsafe methods...
      unsafe static void UsePointerToPoint()
      {
         // Доступ к членам через указатель
         Point point;
         Point* p = &point;
         p->x = 100;
         p->y = 200;
         Console.WriteLine(p->ToString());

         // Доступ к членам через разыменование указателя.
         Point point2;
         Point* p2 = &point2;
         (*p2).x = 100;
         (*p2).y = 200;
         Console.WriteLine((*p2).ToString());
      }

      unsafe static void UnsafeStackAlloc()
      {
         char* p = stackalloc char[256];  // Выделение памяти в стеке
         for (int k = 0; k < 256; k++)
            p[k] = (char)k;
      }

      unsafe static void SquareIntPointer(int* myIntPointer)
      {
         *myIntPointer *= *myIntPointer;
      }

      unsafe static void PrintValueAndAddress()
      {
         int myInt;

         // Определяем указатель на целое и инициализируем его адресом myInt
         int* ptrToMyInt = &myInt;

         // Присваиваем значение через косвенный доступ по указателю.
         *ptrToMyInt = 123;

         // Печатаем статистику.
         Console.WriteLine("Value of myInt {0}", myInt);
         Console.WriteLine("Address of myInt {0:X}", (int)&ptrToMyInt);
      }

      unsafe public static void UnsafeSwap(int* i, int* j)
      {
         int temp = *i;
         *i = *j;
         *j = temp;
      }

      unsafe public static void UseAndPinPoint()
      {
         PointRef pt = new PointRef();
         pt.x = 5;
         pt.y = 6;

         // Фиксируем указатель в памяти, что GC не смог до него добраться
         fixed (int* p = &pt.x)
         {
            // Используем этот указатель
         }

         // pt, как и прежде, не фиксирован в памяти и может быть доступен GC
         Console.WriteLine("Point is: {0}", pt);
      }

      unsafe static void UseSizeOfOperator()
      {
         Console.WriteLine("The size of short is {0}.", sizeof(short));
         Console.WriteLine("The size of int is {0}.", sizeof(int));
         Console.WriteLine("The size of long is {0}.", sizeof(long));
         Console.WriteLine("The size of double is {0}.", sizeof(double));
         Console.WriteLine("The size of Point is {0}.", sizeof(Point));
      }

      #endregion

      public static void SafeSwap(ref int i, ref int j)
      {
         int temp = i;
         i = j;
         j = temp;
      }
   }
}
