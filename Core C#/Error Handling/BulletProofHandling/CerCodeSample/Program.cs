/**
 * Фрагменты кода, устойчивые к сбоям
 */

using System;
using System.Runtime.CompilerServices;

namespace CerCodeSample
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("Danger");
         try
         {
            Danger();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         Console.WriteLine();

         Console.WriteLine("Safety");
         try
         {
            SafeCode();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         Console.WriteLine();

         // NOTE: Еще один способ гарантированной очистки
         RuntimeHelpers.TryCode tryCode = data =>
         {
            Console.WriteLine("Try code");
         };

         RuntimeHelpers.CleanupCode cleanupCode = (data, thrown) =>
         {
            Console.WriteLine("Cleanup code");
         };

         RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(tryCode, cleanupCode, null);
      }

      private static void SafeCode()
      {
         // Подготавливаем код к блоке finally
         RuntimeHelpers.PrepareConstrainedRegions();

         // NOTE: Код в блоке try выполнится только, если выполнится код в блоках catch finally
         try
         {
            Console.WriteLine("In try");
         }
         finally
         {
            var safeType = new SafeType();
            safeType.ForceError(true);
         }
      }

      private static void Danger()
      {
         try
         {
            Console.WriteLine("In try");
         }
         finally
         {
            // Неявный вызов статического конструктора
            var type = new DangerType();
            type.ForceError(true);
         }
      }
   }
}