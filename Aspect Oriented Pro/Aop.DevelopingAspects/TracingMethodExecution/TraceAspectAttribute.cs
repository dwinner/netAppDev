using System;
using System.Reflection;
using PostSharp.Aspects;

namespace TracingMethodExecution
{
   [Serializable]
   public sealed class TraceAspectAttribute : OnMethodBoundaryAspect
   {       
      private readonly string _category;  // Это поле инициализируется и сериализуется во время построения, десериализуется во время выполнения

      // Эти поля инициализируются во время выполнения. Они не нуждаются в сериализации
      [NonSerialized] private string _enteringMessage;
      [NonSerialized] private string _exitingMessage;
      
      public TraceAspectAttribute() // Конструктор, вызываемый во время построения
      {         
      }
      
      public TraceAspectAttribute(string category) // Конструктор, задающий категорию трассировки, вызывается в процессе построения
      {
         _category = category;         
      }

      // Вызывается только один раз из статического конструктора типа,
      // к которому применяется данный аспект (в контексте конкретного метода)
      public override void RuntimeInitialize(MethodBase method)
      {
         if (method.DeclaringType == null) return;
         var methodName = method.DeclaringType.FullName + method.Name;
         _enteringMessage = $"Entering {methodName} - {DateTime.Now.ToString("T")}";
         _exitingMessage = $"Exiting {methodName} - {DateTime.Now.ToString("T")}";
      }
      
      public override void OnEntry(MethodExecutionArgs args)   // Вызывается во время выполнения перед вызовом метода, к которому применялся аспект
         => Console.WriteLine("{0} {1}: {2}", _enteringMessage, _category, DateTime.Now.ToString("T"));

      public override void OnExit(MethodExecutionArgs args) // Вызывается во время выполнения после вызова метода назначения (в сгенерированном блоке finally)
         => Console.WriteLine("{0} {1}: {2}", _exitingMessage, _category, DateTime.Now.ToString("T"));
   }
}