using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

StringBuilder outputText = new();

// modify this line to retrieve details of any other data type
Type t = typeof(double);

AnalyzeType(t);
Console.WriteLine($"Analysis of type {t.Name}");
Console.WriteLine(outputText.ToString());

Console.ReadLine();

void AnalyzeType(Type type)
{
   TypeInfo typeInfo = type.GetTypeInfo();
   AddToOutput($"Type Name: {type.Name}");
   AddToOutput($"Full Name: {type.FullName}");
   AddToOutput($"Namespace: {type.Namespace}");

   var tBase = typeInfo.BaseType;

   if (tBase != null)
   {
      AddToOutput($"Base Type: {tBase.Name}");
   }

   ShowMembers("constructors", type.GetConstructors());
   ShowMembers("methods", type.GetMethods());
   ShowMembers("properties", type.GetProperties());
   ShowMembers("fields", type.GetFields());
   ShowMembers("events", type.GetEvents());

   void ShowMembers(string title, ICollection<MemberInfo> members)
   {
      if (members.Count == 0)
      {
         return;
      }

      AddToOutput($"\npublic {title}:");
      var names = members.Select(m => m.Name).Distinct();
      AddToOutput(string.Join(" ", names));
   }

   void AddToOutput(string text) =>
      outputText.Append($"{text}{Environment.NewLine}");
}