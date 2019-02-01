namespace AopWeakEvent
{
   using System;
   using System.Collections.Immutable;
   using System.Threading;

   using PostSharp.Aspects;
   using PostSharp.Aspects.Advices;
   using PostSharp.Serialization;

   /// <summary>
   ///    Aspect that, when added to a class, enables the class to subscribe to weak events built with the
   ///    <see cref="WeakEventAttribute" /> aspect.
   /// </summary>
   [PSerializable]
   [IntroduceInterface(typeof (IWeakEventClient))]
   public sealed class WeakEventClientAttribute : InstanceLevelAspect, IWeakEventClient
   {
      [PNonSerialized]
      private ImmutableArray <Delegate> _handlers;

      [PNonSerialized]
      private SpinLock _spinLock;

      /// <summary>
      ///    Initializes the aspect at run-time.
      /// </summary>
      /// <param name="type"></param>
      public override void RuntimeInitialize(Type type)
      {
         _handlers = ImmutableArray <Delegate>.Empty;

         // We don't need to implement RuntimeInitializeInstance because this.handlers is an immutable collection,
         // so MemberwiseClone is safe here. If we had a mutable collection, we would have to initialize the 
         // collection in RuntimeInitializeInstance instance too.
      }

      #region Implementation of IWeakEventClient

      void IWeakEventClient.RegisterEventHandler(Delegate handler)
      {
         bool lockTaken = false;

         try
         {
            _spinLock.Enter(ref lockTaken);
            _handlers = _handlers.Add(handler);
         }
         finally
         {
            if (lockTaken)
            {
               _spinLock.Exit();
            }
         }
      }

      void IWeakEventClient.UnregisterEventHandler(Delegate handler)
      {
         bool lockTaken = false;

         try
         {
            _spinLock.Enter(ref lockTaken);
            _handlers = _handlers.Remove(handler);
         }
         finally
         {
            if (lockTaken)
            {
               _spinLock.Exit();
            }
         }
      }

      #endregion
   }
}