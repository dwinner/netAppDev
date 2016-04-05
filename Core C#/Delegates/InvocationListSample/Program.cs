/**
 * Дополнительные средства управления цепочками делегатов
 */

using System;
using System.Linq;
using System.Text;

namespace InvocationListSample
{
   internal static class Program
   {
      private static void Main()
      {
         // Объявление пустой цепочки делегатов
         GetStatus getStatus = null;

         // Создание 3-х компонентов и добавление в цепочку методов проверки их состояния
         getStatus += new Light().SwitchPosition;
         getStatus += new Fan().Speed;
         getStatus += new Speaker().Volume;

         // Сводный отчет о состоянии 3-х компонентов
         Console.WriteLine(GetComponentStatusReport(getStatus));
      }

      /// <summary>
      ///    Запрашивание состояния у компонентов делегата GetStatus
      /// </summary>
      /// <param name="status">Делегат GetStatus</param>
      /// <returns>Отчет</returns>
      private static string GetComponentStatusReport(GetStatus status)
      {
         // Если цепочка пуста, действий не нужно
         if (status == null)
         {
            return null;
         }

         // Построение отчета о состоянии
         StringBuilder report=new StringBuilder();

         // Создание массива из делегатов цепочки
         var delegates = status.GetInvocationList();

         foreach (var getStat in delegates.Cast<GetStatus>())
         {
            try
            {
               report.AppendFormat("{0}{1}{1}", getStat(), Environment.NewLine);
            }
            catch (InvalidOperationException e)
            {
               var component = getStat.Target;
               report.AppendFormat("Failed to get status from {1}{2}{0} Error: {3}{0}{0}", Environment.NewLine,
                  component == null ? "" : component.GetType() + ".", getStat.Method.Name, e.Message);
            }
         }

         return report.ToString();
      }

      // Определние делегатов, позволяющих запрашивать состояние объекта Speaker
      private delegate string GetStatus();
   }

   internal class Light
   {
      public string SwitchPosition() => "The light is off";
   }

   internal class Fan
   {
      public string Speed()
      {
         throw new InvalidOperationException("The fan broke due to overheating");
      }
   }

   internal class Speaker
   {
      public string Volume() => "The volume is loud";
   }
}