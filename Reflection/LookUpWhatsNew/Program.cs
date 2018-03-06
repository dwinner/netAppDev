/**
 * Чтение информации из атрибутов
 */

using System;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WhatsNewAttributes;

namespace LookUpWhatsNew
{
   internal static class Program
   {
      private static readonly StringBuilder _OutputText = new StringBuilder(0x400);
      private static DateTime _backDateTo = new DateTime(2010, 2, 1);

      private static void Main()
      {
         Assembly theAssembly = Assembly.Load("VectorClass");
         Attribute supportAttribute = Attribute.GetCustomAttribute(theAssembly, typeof (SupportsWhatsNewAttribute));
         string assemblyName = theAssembly.FullName;
         
         AddToMessage($"Assembly: {assemblyName}");
         if (supportAttribute == null)
         {
            AddToMessage("This assembly does not support WhatsNew attributes");
            return;
         }

         AddToMessage("Defined Types:");

         Type[] types = theAssembly.GetTypes();
         foreach (var definedType in types)
         {
            DisplayTypeInfo(definedType);
         }

         MessageBox.Show(_OutputText.ToString(), $"What\'s New since {_backDateTo.ToLongDateString()}");

         Console.ReadLine();
      }

      private static void DisplayTypeInfo(Type type)
      {
         if (!(type.IsClass))
         {
            return;
         }

         AddToMessage($"\nClass {type.Name}");

         Attribute[] attributes = Attribute.GetCustomAttributes(type);
         if (attributes.Length == 0)
         {
            AddToMessage("No changes to this class");
         }
         else
         {
            foreach (var attribute in attributes)
            {
               WriteAttributeInfo(attribute);
            }
         }

         MethodInfo[] methods = type.GetMethods();
         AddToMessage("CHANGES TO METHODS OF THIS CLASS:");
         foreach (var method in methods)
         {
            object[] customAttributes = method.GetCustomAttributes(typeof (LastModifiedAttribute), false);
            AddToMessage($"{method.ReturnType} {method.Name}()");
            foreach (var attribute in customAttributes)
            {
               WriteAttributeInfo(attribute as Attribute);
            }
         }
      }

      private static void WriteAttributeInfo(Attribute attribute)
      {
         if (!(attribute is LastModifiedAttribute lastModifiedAttribute))
         {
            return;
         }

         DateTime modifiedDate = lastModifiedAttribute.DateModified;
         if (modifiedDate < _backDateTo)
         {
            return;
         }

         AddToMessage($"  MODIFIED: {modifiedDate.ToLongDateString()}:");
         AddToMessage($"    {lastModifiedAttribute.Changes}");
         if (lastModifiedAttribute.Issues != null)
         {
            AddToMessage($"    Outstanding issues: {lastModifiedAttribute.Issues}");
         }
      }

      private static void AddToMessage(string message)
      {
         _OutputText.AppendFormat("\n{0}", message);
      }
   }
}
