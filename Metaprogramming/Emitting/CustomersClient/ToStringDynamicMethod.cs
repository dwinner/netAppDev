using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace CustomersClient
{
   internal delegate string ToStringDelegate<T>(T target);

   public static class ToStringDynamicMethod
   {
      private const string Separator = " || ";

      private static readonly Dictionary<Type, Delegate> toStrings =
         new Dictionary<Type, Delegate>();

      public static string RunToString<T>(T target)
      {
         ToStringDelegate<T> toString = null;

         var targetType = target.GetType();

         if (toStrings.ContainsKey(targetType))
         {
            toString = toStrings[targetType] as ToStringDelegate<T>;
         }
         else
         {
            toString = CreateToString(target);
            toStrings.Add(targetType, toString);
         }

         return toString(target);
      }

      /// <summary>
      ///    Links to keep in mind:
      ///    Visualizer: http://blogs.msdn.com/haibo_luo/archive/2005/10/25/484861.aspx
      ///    Debugging: http://blogs.msdn.com/yirutang/archive/2005/05/26/422373.aspx
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="target"></param>
      /// <returns></returns>
      private static ToStringDelegate<T> CreateToString<T>(T target)
      {
         var type = typeof (T);

         var toString = new DynamicMethod("ToString" + typeof (T).GetHashCode(),
            typeof (string), new[] {typeof (T)}, typeof (ToStringDynamicMethod).Module);

         var generator = toString.GetILGenerator();

         var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

         if (properties.Length > 0)
         {
            var stringBuilderType = typeof (StringBuilder);

            var toStringLocal = generator.DeclareLocal(typeof (StringBuilder));

            generator.Emit(OpCodes.Newobj, stringBuilderType.GetConstructor(Type.EmptyTypes));
            generator.Emit(OpCodes.Stloc_0);
            generator.Emit(OpCodes.Ldloc_0);

            var appendMethod = stringBuilderType.GetMethod(
               "Append", new[] {typeof (string)});
            var toStringMethod = typeof (object).GetMethod("ToString");

            for (var i = 0; i < properties.Length; i++)
            {
               var property = properties[i];

               if (property.CanRead)
               {
                  generator.Emit(OpCodes.Ldstr, property.Name + ": ");
                  generator.EmitCall(OpCodes.Callvirt, appendMethod, null);
                  generator.Emit(OpCodes.Ldarg_0);

                  var propertyGet = property.GetGetMethod();

                  generator.EmitCall(propertyGet.IsVirtual ? OpCodes.Callvirt : OpCodes.Call,
                     propertyGet, null);

                  var returnType = propertyGet.ReturnType;
                  if (returnType != typeof (string))
                  {
                     if (returnType.IsValueType)
                     {
                        var localReturnType = generator.DeclareLocal(returnType);
                        generator.Emit(OpCodes.Stloc, localReturnType);
                        generator.Emit(OpCodes.Ldloca, localReturnType);
                     }

                     var returnToStringMethod = returnType.GetMethod("ToString", Type.EmptyTypes);
                     generator.EmitCall(OpCodes.Callvirt, returnToStringMethod ?? toStringMethod, null);
                  }

                  generator.EmitCall(OpCodes.Callvirt, appendMethod, null);

                  if (i < properties.Length - 1)
                  {
                     generator.Emit(OpCodes.Ldstr, Separator);
                     generator.EmitCall(OpCodes.Callvirt, appendMethod, null);
                  }
               }
            }

            generator.Emit(OpCodes.Pop);
            generator.Emit(OpCodes.Ldloc_0);
            generator.EmitCall(OpCodes.Callvirt, toStringMethod, null);
         }
         else
         {
            generator.Emit(OpCodes.Ldstr, string.Empty);
         }

         generator.Emit(OpCodes.Ret);

         return (ToStringDelegate<T>) toString.CreateDelegate(typeof (ToStringDelegate<T>));
      }
   }
}