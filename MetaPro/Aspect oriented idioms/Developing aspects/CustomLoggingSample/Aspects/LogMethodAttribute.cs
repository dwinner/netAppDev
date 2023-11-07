namespace CustomLoggingSample.Aspects
{
   using System.Reflection;
   using System.Text;

   using Aop.Lib;

   using PostSharp.Aspects;
   using PostSharp.Serialization;

   /// <summary>
   ///    Аспект, применяемый к методу, который добавляет запись о вызове метода в класс <see cref="Logger" />
   /// </summary>
   [PSerializable]
   [LinesOfCodeAvoided(6)]
   public sealed class LogMethodAttribute : OnMethodBoundaryAspect
   {
      /// <summary>
      ///    Метод, вызываемый перед вызовом целевого метода.
      /// </summary>
      /// <param name="args">Контекст выполения метода.</param>
      public override void OnEntry(MethodExecutionArgs args)
      {
         var stringBuilder = new StringBuilder();
         stringBuilder.Append("Entering ");
         stringBuilder.AppendCallInformation(args);
         Logger.WriteLine(stringBuilder.ToString());
         Logger.Indent();
      }

      /// <summary>
      ///    Метод, вызываемый после вызова целевого метода.
      /// </summary>
      /// <param name="args">Контекст выполнения метода.</param>
      public override void OnSuccess(MethodExecutionArgs args)
      {
         Logger.Unindent();

         var stringBuilder = new StringBuilder();
         stringBuilder.Append("Exiting ");
         stringBuilder.AppendCallInformation(args);
         if (!args.Method.IsConstructor && ((MethodInfo) args.Method).ReturnType != typeof (void))
         {
            stringBuilder.Append(" with return value ").Append(args.ReturnValue);
         }

         Logger.WriteLine(stringBuilder.ToString());
      }

      /// <summary>
      ///    Метод, вызываемый, когда целевой метод "вбросил" исключение.
      /// </summary>
      /// <param name="args">Контекст выполнения метода.</param>
      public override void OnException(MethodExecutionArgs args)
      {
         Logger.Unindent();
         var stringBuilder = new StringBuilder();
         stringBuilder.Append("Exiting ");
         stringBuilder.AppendCallInformation(args);
         if (!args.Method.IsConstructor && ((MethodInfo) args.Method).ReturnType != typeof (void))
         {
            stringBuilder.Append(" with exception ").Append(args.Exception.GetType().Name);
         }

         Logger.WriteLine(stringBuilder.ToString());
      }
   }
}