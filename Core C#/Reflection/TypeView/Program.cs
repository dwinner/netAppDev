/**
 * Обзор средств к отражению
 */

using System;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace TypeView
{
   static class Program
   {
      private static readonly StringBuilder OutputText = new StringBuilder();

      static void Main()
      {
         Type type = typeof(double);
         AnalyzeType(type);
         MessageBox.Show(OutputText.ToString(), string.Format("Alalysis of type {0}", type.Name));

         Console.ReadLine();
      }

      private static void AddToOutput(string text)
      {
         OutputText.AppendFormat("\n{0}", text);
      }

      private static void AnalyzeType(Type type)
      {
         AddToOutput(string.Format("Type Name: {0}", type.Name));
         AddToOutput(string.Format("Full Name: {0}", type.FullName));
         AddToOutput(string.Format("Namespace: {0}", type.Namespace));
         Type baseType = type.BaseType;
         AddToOutput(baseType == null ? string.Empty : string.Format("Base Type: {0}", baseType.Name));
         Type underlyingSystemType = type.UnderlyingSystemType;
         AddToOutput(underlyingSystemType.Name);
         AddToOutput("\nPUBLIC MEMBERS:");
         MemberInfo[] members = type.GetMembers();
         foreach (var memberInfo in members)
         {
            AddToOutput(string.Format("{0} {1} {2}", memberInfo.DeclaringType, memberInfo.MemberType, memberInfo.Name));
         }
      }
   }
}
