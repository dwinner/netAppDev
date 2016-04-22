/**
 * Демонстрация простого способа обработки исключений
 */

using System;

namespace SimpleExceptions
{
   public static class Program
   {
      public static void Main()
      {
         while (true)
         {
            try
            {
               Console.Write("Input a number between 0 and 5 (or just hit return to exit)> "); // запрос на ввод числа
               string userInput = Console.ReadLine();
               if (userInput == string.Empty)
               {
                  break;
               }

               int index = Convert.ToInt32(userInput);
               if (index < 0 || index > 5)
               {
                  throw new IndexOutOfRangeException(string.Format("You typed in {0}", userInput));
               }

               // вывод введенного числа
               Console.WriteLine("Your number was {0}", index);
            }
            catch (IndexOutOfRangeException ex)
            {
               Console.WriteLine("Exception: Number should be between 0 and 5. {0}", ex.Message);
            }
            catch (Exception ex)
            {
               Console.WriteLine("An exception was thrown. Message was: {0}", ex.Message);
            }
            finally
            {
               Console.WriteLine("Thank you.");
            }
         }
      }
   }
}
