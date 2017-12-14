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
      private static readonly StringBuilder _OutputText = new StringBuilder();

      static void Main()
      {
         Type type = typeof(double);
         AnalyzeType(type);
         MessageBox.Show(_OutputText.ToString(), $"Alalysis of type {type.Name}");

         Console.ReadLine();
      }

      private static void AddToOutput(string text)
      {
         _OutputText.AppendFormat("\n{0}", text);
      }

      private static void AnalyzeType(Type type)
      {
         AddToOutput($"Type Name: {type.Name}");
         AddToOutput($"Full Name: {type.FullName}");
         AddToOutput($"Namespace: {type.Namespace}");
         Type baseType = type.BaseType;
         AddToOutput(baseType == null ? string.Empty : $"Base Type: {baseType.Name}");
         Type underlyingSystemType = type.UnderlyingSystemType;
         AddToOutput(underlyingSystemType.Name);
         AddToOutput("\nPUBLIC MEMBERS:");
         MemberInfo[] members = type.GetMembers();
         foreach (var memberInfo in members)
         {
            AddToOutput($"{memberInfo.DeclaringType} {memberInfo.MemberType} {memberInfo.Name}");
         }
      }
   }
}
