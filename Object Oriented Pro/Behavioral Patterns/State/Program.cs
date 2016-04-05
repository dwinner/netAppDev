/**
 * Изменение состояния объекта в зависимости от поведения
 * TODO: Можно провести рефакторинг канонической формы для использования перечислений
 */

namespace State
{
   static class Program
   {
      static void Main()
      {
         IAutomat automat = new AutomatImpl(9);

         automat.GotApplication();
         automat.CheckApplication();
         automat.RentApartment();
      }
   }
}
