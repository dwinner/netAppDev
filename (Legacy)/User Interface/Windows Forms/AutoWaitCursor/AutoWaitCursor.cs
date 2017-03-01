using System;
using System.Windows.Forms;

namespace AutoWaitCursor
{
   internal class AutoWaitCursor : IDisposable
   {
      private readonly Cursor _prevCursor;
      private readonly Control _target;

      public AutoWaitCursor(Control control)
      {
         if (control == null)
         {
            throw new ArgumentNullException("control");
         }
         _target = control;
         _prevCursor = _target.Cursor;
         _target.Cursor = Cursors.WaitCursor;
      }

      public void Dispose()
      {
         _target.Cursor = _prevCursor;
      }
   }
}