/*
 * Выявление атрибутов без создания производных
 */

using System;
using System.Diagnostics;
using System.Reflection;

[assembly: CLSCompliant(true)]

namespace FindingAttributes2
{
   [Serializable]
   [DefaultMember("Main")]
   [DebuggerDisplay("Vinevcev", Name = "Jeff", Target = typeof(Program))]
   internal class Program
   {
      [CLSCompliant(true)]
      [STAThread]
      private static void Main(string[] args)
      {
         // Вывод набора атрибутов, примененных к типу
         ShowAttributes(typeof(Program));

         // Получение и задание методов, связанных с типом
         var members = typeof(Program).FindMembers(MemberTypes.Constructor | MemberTypes.Method,
            BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static,
            Type.FilterName, "*");

         Array.ForEach(members, ShowAttributes); // Вывод набора атрибутов, примененных к члену
      }

      private static void ShowAttributes(MemberInfo attrTarget)
      {
         var attributes = CustomAttributeData.GetCustomAttributes(attrTarget);

         Console.WriteLine("Attributes applied to {0}: {1}", attrTarget.Name,
            attributes.Count == 0 ? "None" : string.Empty);

         foreach (var attribute in attributes)
         {
            // Вывод типа каждого примененного атрибута
            var attrCtor = attribute.Constructor;
            var t = attrCtor.DeclaringType;
            Console.WriteLine(" {0}", t);
            Console.WriteLine(" Constructor called={0}", attrCtor);

            var posArgs = attribute.ConstructorArguments;
            Console.WriteLine(" Positional arguments passed to constructor: " +
                              (posArgs.Count == 0 ? "None" : string.Empty));

            foreach (var pa in posArgs)
            {
               Console.WriteLine(" Type={0}, Value={1}", pa.ArgumentType, pa.Value);
            }

            var namedArgs = attribute.NamedArguments;
            Console.WriteLine(" Named arguments set after construction:" +
                              (namedArgs != null && namedArgs.Count == 0 ? " None" : string.Empty));
            if (namedArgs != null)
            {
               foreach (var na in namedArgs)
               {
                  Console.WriteLine(" Name={0}, Type={1}, Value={2}", na.MemberInfo.Name, na.TypedValue.ArgumentType,
                     na.TypedValue.Value);
               }

               Console.WriteLine();
            }

            Console.WriteLine();
         }
      }
   }
}