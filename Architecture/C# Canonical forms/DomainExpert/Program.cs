/**
 * Эксперт предметной области
 */

using System;

namespace DomainExpert
{
   internal class Program
   {
      private static void Main()
      {
         try
         {
            throw new InvalidOperationException("Error");
         }
         catch (Exception ex)
         {
            IExpert handlExpert = new ErrorHandleExpert(ex.Message);
            handlExpert.Handle(new ConsoleLog());
         }
      }
   }
}