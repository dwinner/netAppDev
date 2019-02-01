
#pragma warning disable 420

namespace AopWeakEvent
{
   using System;
   using System.Collections.Immutable;
   using System.Threading;

   using PostSharp.Serialization;

   [PSerializable]
   internal struct WeakEventHandler
   {
      private bool _initialized;
      private ImmutableArray <object> _handlers;

      [PNonSerialized]
      private volatile int _cleanUpCounter;

      private SpinLock _spinLock;

      public void Initialize()
      {
         if (!_initialized)
         {
            _cleanUpCounter = 0;
            _initialized = true;
            _handlers = ImmutableArray <object>.Empty;
         }
      }

      public bool AddHandler(Delegate handler, bool weak)
      {
         bool lockTaken = false;

         try
         {
            _spinLock.Enter(ref lockTaken);
            _handlers = _handlers.Add(weak ? (object) new WeakReference(handler) : handler);
            return _handlers.Length == 1;
         }
         finally
         {
            if (lockTaken)
            {
               _spinLock.Exit();
            }
         }
      }

      public bool RemoveHandler(Delegate handler)
      {
         bool lockTaken = false;
         try
         {
            _spinLock.Enter(ref lockTaken);
            _handlers =
               _handlers.RemoveAll(
                                   o =>
                                   ReferenceEquals(o, handler) ||
                                   o is WeakReference && ReferenceEquals(((WeakReference) o).Target, handler));

            return _handlers.IsEmpty;
         }
         finally
         {
            if (lockTaken)
            {
               _spinLock.Exit();
            }
         }
      }

      public void InvokeHandler(object[] args)
      {
         int lastCleanUpCounter = -1;

         // Take a snapshot of the handlers list.
         var invocationList = _handlers;

         bool needCleanUp = false;

         foreach (object obj in invocationList)
         {
            var handler = obj as Delegate;

            if (handler == null)
            {
               handler = (Delegate) ((WeakReference) obj).Target;

               if (handler == null)
               {
                  if (!needCleanUp)
                  {
                     needCleanUp = true;
                     lastCleanUpCounter = _cleanUpCounter;
                  }

                  continue;
               }
            }

            handler.DynamicInvoke(args);
         }

         if (needCleanUp && lastCleanUpCounter == _cleanUpCounter && lastCleanUpCounter == _cleanUpCounter)
         {
            bool lockTaken = false;
            try
            {
               _spinLock.Enter(ref lockTaken);
               _handlers = _handlers.RemoveAll(w => w is WeakReference && !((WeakReference) w).IsAlive);
               Interlocked.Increment(ref _cleanUpCounter);
            }
            finally
            {
               if (lockTaken)
               {
                  _spinLock.Exit();
               }
            }
         }
      }
   }
}