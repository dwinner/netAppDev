using System;
using System.Diagnostics;

namespace _11_RollbackBehavior
{
   public class RollbackHelper : IDisposable
   {
      private Database _database;
      private bool _disposed = false;
      private bool _committed = false;

      public RollbackHelper(Database database)
      {
         _database = database;
      }

      public void Commit()
      {
         _database.Commit();
         _committed = true;
      }

      #region Disposable

      public void Dispose()
      {
         Dispose(true);
      }

      ~RollbackHelper()
      {
         Dispose(false);
      }

      private void Dispose(bool disposing)
      {
         // Если объект уже освобожден, то не делать ничего. Помните,
         // что вызывать Dispose() несколько раз на одном объекте
         // совершенно законно.
         if (!_disposed)
         {
            _disposed = true;
            // Помните, что мы не хотим ничего делать с _database,
            // если попали сюда из финализатора, поскольку поле базы
            // данных может быть уже финализированным!
            if (disposing)
            {
               if (!_committed)
               {
                  _database.Rollback();
               }
            }
            else
            {
               Debug.Assert(false, "Сбой при вызове Dispose() на RollbackHelper");
            }
         }
      }

      #endregion
   }
}
