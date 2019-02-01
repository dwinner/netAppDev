/**
 * Делегаты и отражение
 */

using System;
using System.Reflection;
using static System.Array;
using static System.Delegate;
using static System.IO.Path;
using static System.Reflection.Assembly;
using static System.Reflection.BindingFlags;

namespace DelegatesAndReflection
{
   internal delegate object TwoInt32S(int n1, int n2);

   internal delegate object OneString(string s1);

   internal class Program
   {
      private static void Main(string[] args)
      {
         if (args.Length < 2)
         {
            var fileName = GetFileNameWithoutExtension(GetEntryAssembly().Location);
            var usage = @"Usage:" +
                        "{0}{1} delType methodName [Arg1] [Arg2]" +
                        "{0} where delType must be TwoInt32S or OneString" +
                        "{0} if delType is TwoInt32, methodName must be Add or Subtract" +
                        "{0} if delType is OneString, methodName must be NumChars or Reverse" +
                        "{0}" +
                        "{0}Examples:" +
                        "{0} {1} TwoInt32S Add 123 321" +
                        "{0} {1} TwoInt32S Subtract 123 321" +
                        "{0} {1} OneString NumChars \"Hello there\"" +
                        "{0} {1} OneString Reverse \"Hello there\"";
            Console.WriteLine(usage, Environment.NewLine, fileName);
            return;
         }

         // преобразование аргумента delType в тип делегата
         var delType = Type.GetType(args[0]);
         if (delType == null)
         {
            Console.WriteLine("Invalid delType argument: {0}", args[0]);
            return;
         }

         Delegate d;
         try
         {
            // Преобразование аргумента Arg1 в метод
            var mi = typeof(Program).GetMethod(args[1], NonPublic | Static);

            // Создание делегата, служащего оболочкой статического метода
            d = CreateDelegate(delType, mi);
         }
         catch (ArgumentException)
         {
            Console.WriteLine("Invalid methodName argument: {0}", args[1]);
            return;
         }

         // Создание массива, содержащего аргументы, передаваемые методу через делегат
         var callbackArgs = new object[args.Length - 2];

         if (d.GetType() == typeof(TwoInt32S))
         {
            try
            {
               // Преобразование аргументов типа string в тип int
               for (var a = 2; a < args.Length; a++)
               {
                  callbackArgs[a - 2] = int.Parse(args[a]);
               }
            }
            catch (FormatException)
            {
               Console.WriteLine("Parameters must be integers.");
               return;
            }
         }

         if (d.GetType() == typeof(OneString))
         {
            // Простое копирование аргумента типа String
            Copy(args, 2, callbackArgs, 0, callbackArgs.Length);
         }

         try
         {
            // Вызов делегата и вывод результата
            var result = d.DynamicInvoke(callbackArgs);
            Console.WriteLine("Result = {0}", result);
         }
         catch (TargetParameterCountException)
         {
            Console.WriteLine("Incorrect number of parameters specified");
         }
      }

      private static object Add(int n1, int n2) => n1 + n2;

      private static object Subtract(int n1, int n2) => n1 - n2;

      private static object NumChars(string s1) => s1.Length;

      private static object Reverse(string s1)
      {
         var chars = s1.ToCharArray();
         Array.Reverse(chars);
         return new string(chars);
      }
   }
}