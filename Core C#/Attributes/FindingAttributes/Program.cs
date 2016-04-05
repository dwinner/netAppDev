/**
 * Нахождение атрибутов
 */

using System;
using System.Diagnostics;
using System.Reflection;

[assembly: CLSCompliant(true)]

namespace FindingAttributes
{
   [Serializable]
   [DefaultMember("Main")]
   [DebuggerDisplay("Vinevcev", Name = "Jeff", Target = typeof (Program))]
   internal class Program
   {
      [CLSCompliant(true)]
      [STAThread]
      private static void Main()
      {
         // Вывод набора атрибутов, примененных к типу
         ShowAttributes(typeof (Program));

         // Получение и задание методов, связанных с типом
         var members = typeof (Program).FindMembers(MemberTypes.Constructor | MemberTypes.Method,
            BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static,
            Type.FilterName, "*");

         Array.ForEach(members, ShowAttributes); // Вывод набора атрибутов, примененных к члену
      }

      private static void ShowAttributes(MemberInfo attrTarget)
      {
         var attributes = Attribute.GetCustomAttributes(attrTarget);

         Console.WriteLine("Attributes applied to {0}: {1}", attrTarget.Name,
            attributes.Length == 0 ? "None" : string.Empty);

         foreach (var attr in attributes)
         {
            // Вывод типа всех примененных атрибутов
            Console.WriteLine(" {0}", attr.GetType());

            var defalutAttr = attr as DefaultMemberAttribute;
            if (defalutAttr != null)
            {
               Console.WriteLine(" MemberName={0}", defalutAttr.MemberName);
            }

            var conditionalAttr = attr as ConditionalAttribute;
            if (conditionalAttr != null)
            {
               Console.WriteLine(" ConditionString={0}", conditionalAttr.ConditionString);
            }

            var compliantAttr = attr as CLSCompliantAttribute;
            if (compliantAttr != null)
            {
               Console.WriteLine(" IsCompliant={0}", compliantAttr.IsCompliant);
            }

            var debuggerDisplayAttr = attr as DebuggerDisplayAttribute;
            if (debuggerDisplayAttr != null)
            {
               Console.WriteLine(" Value={0}, Name={1}, Target={2}",
                  debuggerDisplayAttr.Value, debuggerDisplayAttr.Name, debuggerDisplayAttr.Target);
            }
         }

         Console.WriteLine();
      }

      [Conditional("DEBUG")]
      [Conditional("Release")]
      public void DoSomething()
      {
      }
   }
}