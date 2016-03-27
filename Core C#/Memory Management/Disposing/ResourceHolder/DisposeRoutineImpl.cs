using System;

namespace ResourceHolder
{
   public class DisposeRoutineImpl : DisposeRoutine
   {
      protected override void UnmanagaedCleaning()
      {
         throw new NotImplementedException();
      }

      protected override void ManagedCleaning()
      {
         throw new NotImplementedException();
      }
   }
}