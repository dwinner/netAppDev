using System;

namespace GenericDelegate
{
   // Параметризованный делегат
   public delegate void MyGenericDelegate<T>(T arg);

   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Generic Delegates *****\n");

         // Регистрация назначений.
         MyGenericDelegate<string> strTarget = new MyGenericDelegate<string>(StringTarget);
         strTarget("Some string data");

         MyGenericDelegate<int> intTarget = new MyGenericDelegate<int>(IntTarget);
         intTarget(9);
         Console.ReadLine();
      }

      static void StringTarget(string arg)
      {
         Console.WriteLine("arg in uppercase is: {0}", arg.ToUpper());
      }

      static void IntTarget(int arg)
      {
         Console.WriteLine("++arg is: {0}", ++arg);
      }
   }
}

