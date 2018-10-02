using System;

namespace NotNullAttribute.Lib
{
   [AttributeUsage(AttributeTargets.Parameter)]
   public sealed class NotNullRequiredAttribute : Attribute
   {
   }
}