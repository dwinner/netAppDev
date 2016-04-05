/**
 * Код, нейтральный к исключениям
 */

using System.Collections.Generic;

namespace _07_ErrorDependantCode
{
   internal class Program
   {
      private static void Main(string[] args)
      {
      }
   }

   internal class EmployeeDatabase
   {
      private List<Employee> _activeEmployees;
      private List<Employee> _terminatedEmployees;

      public void TerminateEmployee(int index)
      {
         // Клонировать важнейшие объекты
         var tempActiveEmployees = new Employee[_activeEmployees.Count];
         var tempTerminatedEmployees = new Employee[_terminatedEmployees.Count];
         _activeEmployees.CopyTo(tempActiveEmployees);
         _terminatedEmployees.CopyTo(tempTerminatedEmployees);

         // Выполнить действия над временными объектами
         var tempActiveEmpList = new List<Employee>(tempActiveEmployees);
         var tempTerminatedEmpList = new List<Employee>(tempTerminatedEmployees);
         var employee = tempActiveEmpList[index];
         tempActiveEmpList.RemoveAt(index);
         tempTerminatedEmpList.Add(employee);

         // Зафиксировать изменения
         List<Employee> tempSpace = null;
         ListSwap(ref _activeEmployees, ref tempActiveEmpList, ref tempSpace);
         ListSwap(ref _terminatedEmployees, ref tempTerminatedEmpList, ref tempSpace);
      }

      private void ListSwap<T>(ref List<T> firstList, ref List<T> secondList, ref List<T> tempList)
      {
         tempList = firstList;
         firstList = secondList;
         secondList = tempList;
         tempList = null;
      }
   }

   internal class Employee
   {
   }
}