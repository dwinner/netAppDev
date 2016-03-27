/**
 * Контракты для интерфейсов
 */

namespace ContractsForInterfaces
{
   static class Program
   {
      static void Main()
      {
         var firstPerson = new Person { FirstName = "Tom", LastName = null }; // Нарушение контракта
         var secondPerson = new Person { FirstName = "Tom", LastName = "Turbo", Age = 133 }; // Нарушение контракта
      }
   }
}
