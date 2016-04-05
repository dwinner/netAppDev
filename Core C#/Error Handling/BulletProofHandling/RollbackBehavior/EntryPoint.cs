/**
 * Обеспечение поведения отката
 */

namespace _11_RollbackBehavior
{
   class EntryPoint
   {
      static void Main(string[] args)
      {
         database = new Database();
         DoSomeWork();
      }

      private static void DoSomeWork()
      {
         using (RollbackHelper guard = new RollbackHelper(database))
         {
            // Здесь выполняем некоторую работу, которая может
            // сгенерировать исключение. Удалите комментарий
            // со следующей строки, чтобы сгенерировать исключение:
            //nullPtr.GetType();
            // Если добрались сюда, фиксируем
            guard.Commit();
         }
      }

      private static Database database;
      private static object nullPtr = null;
   }
}
