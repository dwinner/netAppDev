namespace Aop.Lib
{
   using System;
   using System.Text;

   using PostSharp.Aspects;

   public static class StringBuilderExtensions
   {
      public static StringBuilder AppendCallInformation(this StringBuilder stringBuilder, MethodExecutionArgs args)
      {
         // Добавляем тип и имя метода
         Type declaringType = args.Method.DeclaringType;
         stringBuilder.AppendTypeName(declaringType);
         stringBuilder.Append('.').Append(args.Method.Name);

         // Добавляем обобщенные параметры
         if (args.Method.IsGenericMethod)
         {
            var genericArguments = args.Method.GetGenericArguments();
            stringBuilder.AppendGenericArguments(genericArguments);
         }

         // Добавляем аргументы
         stringBuilder.AppendArguments(args.Arguments);

         return stringBuilder;
      }

      public static void AppendTypeName(this StringBuilder stringBuilder, Type type)
      {
         stringBuilder.Append(type.FullName);
         if (type.IsGenericType)
         {
            var genericArguments = type.GetGenericArguments();
            stringBuilder.AppendGenericArguments(genericArguments);
         }
      }

      public static void AppendGenericArguments(this StringBuilder stringBuilder, Type[] genericArguments)
      {
         stringBuilder.Append('<');
         for (var i = 0; i < genericArguments.Length; i++)
         {
            if (i > 0)
            {
               stringBuilder.Append(", ");
            }
            stringBuilder.Append(genericArguments[i].Name);
         }
         stringBuilder.Append('>');
      }

      public static void AppendArguments(this StringBuilder stringBuilder, Arguments arguments)
      {
         stringBuilder.Append('(');
         for (var i = 0; i < arguments.Count; i++)
         {
            if (i > 0)
            {
               stringBuilder.Append(", ");
            }
            stringBuilder.Append(arguments[i]);
         }
         stringBuilder.Append(')');
      }
   }
}