using System;

namespace LambdaExpressionsMultipleParams
{
   #region SimpleMath (with delegates)
   public class SimpleMath
   {
      public delegate void MathMessage(string msg, int result);
      private MathMessage mmDelegate;

      public void SetMathHandler(MathMessage target)
      {
         mmDelegate = target;
      }

      public void Add(int x, int y)
      {
         if (mmDelegate != null)
            mmDelegate.Invoke("Adding has completed!", x + y);
      }
   }
   #endregion

   class Program
   {
      static void Main(string[] args)
      {
         // Регистрируем делегат через lambda-выражение
         SimpleMath m = new SimpleMath();
         m.SetMathHandler((msg, result) =>
         {
            Console.WriteLine("Message: {0}, Result: {1}", msg, result);
         });

         m.Add(10, 10);
         Console.ReadLine();
      }
   }
}
