using System;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using static System.Reflection.Emit.OpCodes;

namespace Customers.Extensions
{
   internal static class ToStringWithDebuggingIlGenerator
   {
      internal static void Generate(this ILGenerator @this,
         Type target, ISymbolDocumentWriter document, StreamWriter file)
      {
         var properties = target.GetProperties(BindingFlags.Public | BindingFlags.Instance);
         var lineNumber = 1;

         if (properties.Length > 1)
         {
            var stringBuilderType = typeof (StringBuilder);
            var toStringLocal = @this.DeclareLocal(typeof (StringBuilder));
            toStringLocal.SetLocalSymInfo("builder");

            @this.Emit(Newobj, stringBuilderType.GetConstructor(Type.EmptyTypes), document, file, lineNumber++);
            @this.Emit(Stloc_0, document, file, lineNumber++);
            @this.Emit(Ldloc_0, document, file, lineNumber++);

            var appendMethod = stringBuilderType.GetMethod(nameof(StringBuilder.Append), new[] {typeof (string)});
            var toStringMethod = typeof (StringBuilder).GetMethod(nameof(StringBuilder.ToString), Type.EmptyTypes);

            for (var i = 0; i < properties.Length; i++)
            {
               lineNumber = @this.CreatePropertyForToString(properties[i], appendMethod, i < properties.Length, document,
                  file, lineNumber);
            }

            @this.Emit(Pop, document, file, lineNumber++);
            @this.Emit(Ldloc_0, document, file, lineNumber++);
            @this.Emit(Callvirt, toStringMethod, document, file, lineNumber++);
         }
         else
         {
            @this.Emit(Ldstr, string.Empty, document, file, lineNumber++);
         }

         @this.Emit(Ret, document, file, lineNumber);
      }

      private static int CreatePropertyForToString(this ILGenerator @this,
         PropertyInfo property, MethodInfo appendMethod,
         bool needsSeparator, ISymbolDocumentWriter document, StreamWriter file, int lineNumber)
      {
         var propertyLineNumber = lineNumber;

         if (property.CanRead)
         {
            @this.Emit(Ldstr, $"{property.Name}: ", document, file, propertyLineNumber++);
            @this.Emit(Callvirt, appendMethod, document, file, propertyLineNumber++);
            @this.Emit(Ldarg_0, document, file, propertyLineNumber++);

            var propertyGet = property.GetGetMethod();

            @this.Emit(propertyGet.IsVirtual ? Callvirt : Call, propertyGet, document, file,
               propertyLineNumber++);

            var appendType = typeof (StringBuilder).GetMethod(nameof(StringBuilder.Append),
               new[] {propertyGet.ReturnType});
            if (appendType.GetParameters()[0].ParameterType != propertyGet.ReturnType
                && propertyGet.ReturnType.IsValueType)
            {
               @this.Emit(Box, propertyGet.ReturnType, document, file, propertyLineNumber++);
            }

            @this.Emit(Callvirt, appendType, document, file, propertyLineNumber++);

            if (needsSeparator)
            {
               @this.Emit(Ldstr, Constants.Separator, document, file, propertyLineNumber++);
               @this.Emit(Callvirt, appendMethod, document, file, propertyLineNumber++);
            }
         }

         return propertyLineNumber;
      }
   }
}