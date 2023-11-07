using System;
using System.Reflection;
using System.Windows.Forms;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using Message = PostSharp.Extensibility.Message;

namespace ThreadingInterception
{
   [Serializable]
   public class UiThread : MethodInterceptionAspect
   {
      public override bool CompileTimeValidate(MethodBase method)
      {
         if (typeof(Form).IsAssignableFrom(method.DeclaringType))
            return true;

         if (method.DeclaringType == null)
            return false;

         var errorMessage =
            $"UIThread aspect must be used in a Form. [Assembly: {method.DeclaringType.Assembly.FullName}, Class: {method.DeclaringType.FullName}, Method: {method.Name}]";
         Message.Write(method, SeverityType.Error, "UIThreadFormError01", errorMessage);

         return false;
      }

      public override void OnInvoke(MethodInterceptionArgs args)
      {
         var form = args.Instance as Form;
         if (form == null)
            return;

         if (form.InvokeRequired)
            form.Invoke(new Action(args.Proceed));
         else
            args.Proceed();
      }
   }
}