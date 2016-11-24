namespace ExceptionHandlingSample
{
   using System;
   using System.Text;

   using PostSharp.Aspects;
   using PostSharp.Aspects.Dependencies;
   using PostSharp.Serialization;

   /// <summary>
   ///    Аспект, примененный к методу. Уведомляет и подавляет любое исключение, которое
   ///    может "вбрасывать" метод. Этот аспект также печатает данные, которые были сохранены
   ///    аспектом <see cref="AddContextOnExceptionAttribute" />
   /// </summary>
   /// <remarks>
   ///    <para>
   ///       Не стоит повсюду применять этот аспект, так как исключения как правило полезны.
   ///       Используйте его только в качестве протоколирования точек входа и/или для других событий
   ///    </para>
   ///    <para>
   ///       Данный аспект применяться исключительно только после аспекта <see cref="AddContextOnExceptionAttribute" />
   ///    </para>
   /// </remarks>
   [AspectTypeDependency(AspectDependencyAction.Order, AspectDependencyPosition.After,
      typeof (AddContextOnExceptionAttribute))]
   [PSerializable]
   public sealed class ReportAndSwallowExceptionAttribute : OnExceptionAspect
   {
      public override void OnException(MethodExecutionArgs args)
      {
         // Write the default exception information.
         Console.WriteLine("Exception information");
         Console.WriteLine("--------------------------------------------------------------");
         Console.WriteLine(args.Exception.ToString());
         Console.WriteLine("--------------------------------------------------------------");
         var additionalContext = (StringBuilder) args.Exception.Data[AddContextOnExceptionAttribute.ContextTag];

         // Write the additional information that was gathered by AddContextOnExceptionAttribute.
         if (additionalContext != null)
         {
            Console.WriteLine("Additional context information (call stack with parameter values)");
            Console.WriteLine("--------------------------------------------------------------");
            Console.Write(additionalContext.ToString());
            Console.WriteLine("--------------------------------------------------------------");
         }

         // Ignore the exception.
         Console.WriteLine("*** Ignoring the exception ***");
         args.FlowBehavior = FlowBehavior.Continue;
      }
   }
}