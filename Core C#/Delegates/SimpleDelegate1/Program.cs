using System;

namespace SimpleDelegate
{
   // Этот делегат может указывать на любой метод, принимающий два целых и возвращающий целое
   public delegate int BinaryOp(int x, int y);

   #region SimpleMath: Этот класс содержит методы, на которые способен указывать делегат BinaryOp
   public class SimpleMath
   {
      public int Add(int x, int y)
      {
         return x + y;
      }

      public int Subtract(int x, int y)
      {
         return x - y;
      }

      public static int SquareNumber(int a)
      {
         return a * a;
      }
   }
   #endregion

   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Simple Delegate Example *****\n");

         // Ошибка! Метод не может быть вызван через данный деоегат!
         // BinaryOp b2 = new BinaryOp(SimpleMath.SquareNumber);

         // Делегаты .NET могут также указывать на методы экземпляров.
         SimpleMath m = new SimpleMath();
         BinaryOp b = new BinaryOp(m.Add);

         DisplayDelegateInfo(b);

         // Вызовем метод Add() косвенным образом, используя делегат.
         Console.WriteLine("10 + 10 is {0}", b.Invoke(10, 10));

         BinaryOp b2 = new BinaryOp(m.Subtract);
         Console.WriteLine("10 - 10 is {0}", b2.Invoke(10, 10));

         Console.ReadLine();
      }

      #region Вывод методов, на которые указывает делегат
      static void DisplayDelegateInfo(Delegate delObj)
      {
         foreach (Delegate dlg in delObj.GetInvocationList())
         {
            Console.WriteLine("Method name: {0}", dlg.Method);
            Console.WriteLine("Type name: {0}", dlg.Target);
         }
      }
      #endregion
   }
}

