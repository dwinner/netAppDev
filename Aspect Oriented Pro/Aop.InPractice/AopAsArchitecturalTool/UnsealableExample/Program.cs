// Применение архитектурных ограничений

using System;
using System.Linq;
using System.Reflection;
using PostSharp.Constraints;
using PostSharp.Extensibility;

namespace UnsealableExample
{
   internal static class Program
   {
      private static void Main()
      {
      }
   }

   [Serializable]
   [MulticastAttributeUsage(MulticastTargets.Class)]
   public class UnsealableConstraint : ReferentialConstraint
   {
      public override void ValidateCode(object target, Assembly assembly)
      {
         var targetType = target as Type;
         if (targetType == null)
            return;

         var sealedSubClasses =
            assembly.GetTypes()
               .Where(type => type.IsSealed)
               .Where(type => targetType.IsAssignableFrom(type))
               .ToList();

         sealedSubClasses.ForEach(
            type =>
               Message.Write(type, SeverityType.Error, "UNSEAL001",
                  "Error on {0}: subclasses of {1} cannot be sealed.", type.FullName, targetType.FullName));
      }
   }

   [UnsealableConstraint]
   public class MyUnsealableClass
   {
      protected string Value;

      public MyUnsealableClass()
      {
         Value = "I'm unsealable";
      }

      public string GetValue() => Value;
   }

   public sealed class TryingToSeal : MyUnsealableClass
   {
      public TryingToSeal()
      {
         Value = "I'm sealed!";
      }
   }
}