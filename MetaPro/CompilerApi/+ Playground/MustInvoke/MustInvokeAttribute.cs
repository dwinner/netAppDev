using System;

namespace MustInvoke
{
   [AttributeUsage(AttributeTargets.Method, Inherited = false)]
   public sealed class MustInvokeAttribute : Attribute
   {
   }
}