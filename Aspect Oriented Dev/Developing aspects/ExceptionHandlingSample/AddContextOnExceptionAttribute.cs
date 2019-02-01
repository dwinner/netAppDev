namespace ExceptionHandlingSample
{
   using System;
   using System.Text;

   using Aop.Lib;

   using PostSharp.Aspects;
   using PostSharp.Serialization;

   /// <summary>
   ///    Аспект, который можно применить к методу и, если этот метод вбрасывает исключение, то
   ///    добавляет значение параметров метода в контекст объекта <see cref="Exception" />
   ///    <see cref="ReportAndSwallowExceptionAttribute" /> может использовать эту информацию и
   ///    напечатать ее, если исключение не было перехвачено
   /// </summary>
   [PSerializable]
   public sealed class AddContextOnExceptionAttribute : OnExceptionAspect
   {
      internal const string ContextTag = "Context";

      /// <summary>
      ///    Метод, который будет вызван, если целевой метод "вбрасывает" исключения
      /// </summary>
      /// <param name="args">Контекст выполнения метода</param>
      public override void OnException(MethodExecutionArgs args)
      {
         // Возьмем или создадим StringBuilder для исключения, в которое будем добавлять дополнительные данные
         var stringBuilder = args.Exception.Data[ContextTag] as StringBuilder;
         if (stringBuilder == null)
         {
            stringBuilder = new StringBuilder();
            args.Exception.Data[ContextTag] = stringBuilder;
         }

         // Добавляем контекст к StringBuilder
         stringBuilder.AppendCallInformation(args).AppendLine();         
      }
   }
}