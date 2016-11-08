using System;
using System.Linq;
using System.Reflection;
using System.Windows.Threading;
using PostSharp;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace ForceGuiThreadSpecification
{
   internal static class Program
   {
      private static void Main()
      {
      }
   }

   [Serializable]
   [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
   [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
   public sealed class GuiThreadAttribute : MethodInterceptionAspect
   {
      public bool Asynchronous { get; set; }

      public override bool CompileTimeValidate(MethodBase method)
      {
         if (!typeof(DispatcherObject).IsAssignableFrom(method.DeclaringType))
         {
            Message.Write(MessageLocation.Of(method), SeverityType.Error, "CUSTOM02",
               "Cannot apply [GuiThread] to method {0} because it is not a member of a type derived from DispatcherObject.",
               method);
            return false;
         }

         if (Asynchronous &&
             (((MethodInfo)method).ReturnType == typeof(void) ||
              method.GetParameters().Any(parameter => parameter.ParameterType.IsByRef)))
         {
            Message.Write(MessageLocation.Of(method), SeverityType.Error, "CUSTOM02",
               "Cannot apply [GuiThread(Asynchronous=true)] to method {0} because it is not a member of type derived from DispatcherObject.",
               method);
         }

         return true;
      }

      public override void OnInvoke(MethodInterceptionArgs args)
      {
         var dispatcherObject = (DispatcherObject)args.Instance;
         if (dispatcherObject.CheckAccess())
         {
            args.Proceed();
         }
         else
         {
            if (Asynchronous)
            {
               dispatcherObject.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(args.Proceed));
            }
            else
            {
               dispatcherObject.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(args.Proceed));
            }
         }
      }
   }
}