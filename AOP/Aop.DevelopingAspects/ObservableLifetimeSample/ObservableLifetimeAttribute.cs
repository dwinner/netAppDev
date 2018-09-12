using System;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Reflection;

namespace ObservableLifetimeSample
{
   [Serializable]
   [IntroduceInterface(typeof(IObservableLifetime), OverrideAction = InterfaceOverrideAction.Fail)]
   public sealed class ObservableLifetimeAttribute : InstanceLevelAspect, IObservableLifetime
   {
      [NonSerialized]
      private bool _disposed;
      [NonSerialized]
      private bool _notPrototype;

      [ImportMember("Dispose", IsRequired = true, Order = ImportMemberOrder.BeforeIntroductions)]
      public Action<bool> BaseDisposeMethod;

      [IntroduceMember(OverrideAction = MemberOverrideAction.Fail)]
      public event EventHandler Disposed;

      [IntroduceMember(OverrideAction = MemberOverrideAction.Fail)]
      public event EventHandler Finalized;

      public override void RuntimeInitializeInstance() => _notPrototype = true;

      [IntroduceMember(IsVirtual = true, OverrideAction = MemberOverrideAction.OverrideOrFail,
         Visibility = Visibility.Family)]
      public void Dispose(bool disposing)
      {
         if (_disposed)
            return;

         _disposed = true;
         BaseDisposeMethod(disposing);
         Disposed?.Invoke(Instance, EventArgs.Empty);

         if (disposing)
            GC.SuppressFinalize(this);
      }

      ~ObservableLifetimeAttribute()
      {
         if (!_notPrototype)
            return;

         BaseDisposeMethod(false);
         Finalized?.Invoke(Instance, EventArgs.Empty);
      }
   }
}