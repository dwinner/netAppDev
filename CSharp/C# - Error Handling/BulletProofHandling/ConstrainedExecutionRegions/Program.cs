/**
 * Ограниченные области выполнения
 */

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace _08_ConstrainedExecutionRegions
{
   class Program
   {
      static void Main(string[] args)
      {
      }
   }

   class Employee { }

   class EmployeeDatabase
   {
      private List<Employee> _active;
      private List<Employee> _terminate;

      public void TerminateEmployee(int index)
      {
         // Клонировать важные объекты
         List<Employee> tempActive = _active.Clone();
         List<Employee> tempTerminate = _terminate.Clone();

         // Выполнить действия над временными объектами
         Employee employee = tempActive[index];
         tempActive.RemoveAt(index);
         tempTerminate.Add(employee);

         // Зафиксировать изменения
         RuntimeHelpers.PrepareConstrainedRegions();
         try { }
         finally
         {
            List<Employee> tempSpace = null;
            Swapper.SwapList(ref _active, ref tempActive, ref tempSpace);
            Swapper.SwapList(ref _terminate, ref tempTerminate, ref tempSpace);
         }
      }
   }
}
