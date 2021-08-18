using System;

namespace AspectsForTypes
{
   [AttributeUsage(AttributeTargets.All)]
   internal sealed class ExecutionBehaviorAttribute : Attribute
   {
      public ExecutionBehaviorAttribute(ExecutionType executionBehavior)
      {
         ExecutionBehavior = executionBehavior;
      }

      public ExecutionType ExecutionBehavior { get; }
   }
}