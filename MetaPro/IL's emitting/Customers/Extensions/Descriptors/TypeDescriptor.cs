using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Spackle.Extensions;

namespace Customers.Extensions.Descriptors
{
   internal sealed class TypeDescriptor : Descriptor
   {
      private TypeDescriptor()
      {
      }

      internal TypeDescriptor(Type type)
         : this(type, type?.Assembly, true)
      {
      }

      internal TypeDescriptor(Type type, bool showKind)
      {
         CreateDescriptor(type, type.Assembly, showKind);
      }

      internal TypeDescriptor(Type type, Assembly containingAssembly)
         : this(type, containingAssembly, true)
      {
      }

      internal TypeDescriptor(Type type, Assembly containingAssembly, bool showKind)
      {
         CreateDescriptor(type, containingAssembly, showKind);
      }

      private static void CheckAssembly(Assembly containingAssembly)
      {
         if (containingAssembly == null)
         {
            throw new ArgumentNullException(nameof(containingAssembly));
         }
      }

      private void CreateDescriptor(Type type, Assembly containingAssembly, bool showKind)
      {
         StringBuilder builder=new StringBuilder();
         if (type==null||type==typeof(void))
         {
            builder.Append("void");
         }
         else
         {
            TypeDescriptor.CheckAssembly(containingAssembly);
            var elementType = type.GetRootElementType();

            if (TypeDescriptor.IsSpecial(type))
            {
               builder.Append(GetSpecialName(type));
            }
            else
            {
               if (type.Assembly.Equals(containingAssembly))
               {
                  builder
                     .Append(TypeDescriptor.GetTypeKind(type, showKind))
                     .Append(" ")
                     .Append(TypeDescriptor.GetTypeName(type));
               }
               else
               {
                  builder
                     .Append(TypeDescriptor.GetTypeKind(type, showKind))
                     .Append(" ")
                     .Append(TypeDescriptor.GetTypeName(type));
               }

               TypeDescriptor.AddGenerics(type, containingAssembly, builder);
               builder.Append(type.Name.Replace(elementType.Name, string.Empty));
            }            
         }

         Value = builder.ToString().Trim();
      }

      private static void AddGenerics(Type type, Assembly containingAssembly, StringBuilder builder)
      {
         var genericArguments = type.GetGenericArguments();

         if (genericArguments != null && genericArguments.Length > 0)
         {
            var genericCount = "`"+genericArguments.Length.ToString(CultureInfo.CurrentCulture);

            if (!builder.ToString().EndsWith(genericCount,StringComparison.CurrentCulture))
            {
               builder.Append(genericCount);
            }

            builder.Append("<");
            List<string> genericTypes=new List<string>();

            foreach (var genericArgument in genericArguments)
            {
               if (genericArgument.IsGenericParameter)
               {
                  genericTypes.Add(genericArgument.Name);
               }
               else
               {
                  genericTypes.Add(
                     new TypeDescriptor(
                        genericArgument.GetRootElementType(), containingAssembly).Value);
               }
            }

            builder.Append(string.Join(", ", genericTypes.ToArray()));
            builder.Append(">");
         }
      }

      private static string GetSpecialName(Type type)
      {
         var elementType = type.GetRootElementType();

         var specialName = elementType != typeof (float)
            ? (elementType != typeof (double)
               ? (elementType != typeof (long)
                  ? (elementType != typeof (ulong)
                     ? (elementType != typeof (int)
                        ? (elementType != typeof (uint)
                           ? (elementType != typeof (short)
                              ? (elementType != typeof (ushort)
                                 ? (elementType != typeof (sbyte)
                                    ? (elementType != typeof (byte)
                                       ? (elementType != typeof (string)
                                          ? (elementType == typeof (object) ? "object" : string.Empty)
                                          : "string")
                                       : "uint8")
                                    : "int8")
                                 : "uint16")
                              : "int16")
                           : "uint32")
                        : "int32")
                     : "uint64")
                  : "int64")
               : "float64")
            : "float32";

         return $"{specialName}{type.Name.Replace(elementType.Name, string.Empty)}";
      }

      private static string GetTypeKind(Type type, bool showKind)
      {
         var kind = string.Empty;
         if (showKind)
         {
            kind = type.GetRootElementType().IsValueType ? "valuetype" : "class";
         }

         return kind;
      }

      private static string GetTypeName(Type type)
      {
         var elementType = type.GetRootElementType();
         return $"{elementType.Namespace}.{elementType.Name}";
      }

      private static bool IsSpecial(Type type)
      {
         var elementType = type.GetRootElementType();

         return elementType == typeof(float) | elementType == typeof(double) |
            elementType == typeof(long) | elementType == typeof(int) |
            elementType == typeof(ulong) | elementType == typeof(uint) |
            elementType == typeof(short) | elementType == typeof(ushort) |
            elementType == typeof(byte) | elementType == typeof(sbyte) |
            elementType == typeof(void) | elementType == typeof(string) |
            elementType == typeof(object);
      }
   }
}