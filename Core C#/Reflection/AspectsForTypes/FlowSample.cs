using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AspectsForTypes
{
   [Description("This is a flow aspect sample")]
   [ExecutionBehavior(ExecutionType.Parallel)]
   public class FlowSample
   {
      [Description("This is the Go test aspect method")]
      public void Go()
      {
         // Поведение при выполнении
         var executionType = GetType()
            .GetCustomAttributes(typeof(ExecutionBehaviorAttribute), true)
            .Cast<ExecutionBehaviorAttribute>()
            .First()
            .ExecutionBehavior;

         // Извлечения атрибута для текущего метода
         var currentMethod = MethodBase.GetCurrentMethod();
         var currentDescription = GetType()
            .GetMember(currentMethod.Name)
            .First()
            .GetCustomAttributes(typeof(DescriptionAttribute), true)
            .Cast<DescriptionAttribute>()
            .First()
            .Description;

         switch (executionType)
         {
            case ExecutionType.Sequence:
               Console.WriteLine("Sequence behavior: {0}", currentDescription);
               break;
            case ExecutionType.Parallel:
               Console.WriteLine("Parallel behavior: {0}", currentDescription);
               break;
            default:
               throw new ArgumentOutOfRangeException(nameof(executionType));
         }
      }
   }
}