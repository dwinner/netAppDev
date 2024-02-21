using System.Reflection;

namespace AdvancedWeakDelegate;

public class WeakDelegate<TDelegate> where TDelegate : class
{
   private readonly List<MethodTarget> _targets = new();

   public WeakDelegate()
   {
      if (!typeof(TDelegate).IsSubclassOf(typeof(Delegate)))
      {
         throw new InvalidOperationException("TDelegate must be a delegate type");
      }
   }

   public TDelegate? Target
   {
      get
      {
         Delegate? combinedTarget = null;
         foreach (var target in _targets.ToArray())
         {
            var weakRef = target.Reference;

            // Static target || alive instance target
            if (weakRef is not { Target: null })
            {
               var newDelegate = Delegate.CreateDelegate(typeof(TDelegate), weakRef?.Target, target.Method);
               combinedTarget = Delegate.Combine(combinedTarget, newDelegate);
            }
            else
            {
               _targets.Remove(target);
            }
         }

         return combinedTarget as TDelegate;
      }
      set
      {
         _targets.Clear();
         if (value != null)
         {
            Combine(value);
         }
      }
   }

   public void Combine(TDelegate target)
   {
      if (target is not Delegate delegateTarget)
      {
         return;
      }

      foreach (var @delegate in delegateTarget.GetInvocationList())
      {
         _targets.Add(new MethodTarget(@delegate));
      }
   }

   public void Remove(TDelegate target)
   {
      if (target is not Delegate delegateTarget)
      {
         return;
      }

      foreach (var @delegate in delegateTarget.GetInvocationList())
      {
         var methodTarget = _targets.Find(mTarget =>
            Equals(@delegate.Target, mTarget.Reference?.Target)
            && Equals(@delegate.Method.MethodHandle, mTarget.Method.MethodHandle));
         if (methodTarget != null)
         {
            _targets.Remove(methodTarget);
         }
      }
   }

   private class MethodTarget
   {
      public readonly MethodInfo Method;
      public readonly WeakReference? Reference;

      public MethodTarget(Delegate @delegate)
      {
         // d.Target will be null for static method targets:
         if (@delegate.Target != null)
         {
            Reference = new WeakReference(@delegate.Target);
         }

         Method = @delegate.Method;
      }
   }
}