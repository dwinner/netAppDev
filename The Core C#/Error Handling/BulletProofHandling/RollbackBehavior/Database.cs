using System;

namespace _11_RollbackBehavior
{
   public class Database
   {
      public void Commit()
      {
         Console.WriteLine("Изменения зафиксированы");
      }

      public void Rollback()
      {
         Console.WriteLine("Изменения отменены");
      }
   }
}
