namespace Aop.Lib
{
   using System;
   using System.Text;

   using PostSharp.Aspects;

   public static class StringBuilderExtensions
   {
      public static void AppendCallInformation(this StringBuilder stringBuilder, MethodExecutionArgs args)
      {
         // Добавляем тип и имя метода
         Type declaringType = args.Method.DeclaringType;
         Formatter.AppendTypeName(stringBuilder, declaringType);
         stringBuilder.Append('.').Append(args.Method.Name);

         // Добавляем обобщенные параметры
         if (args.Method.IsGenericMethod)
         {
            var genericArguments = args.Method.GetGenericArguments();
            Formatter.AppendGenericArguments(stringBuilder, genericArguments);
         }

         // Добавляем аргументы
         Formatter.AppendArguments(stringBuilder, args.Arguments);
      }
   }
}