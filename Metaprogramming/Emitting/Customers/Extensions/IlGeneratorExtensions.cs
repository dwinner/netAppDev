using System;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using Customers.Extensions.Descriptors;

namespace Customers.Extensions
{
   internal static class IlGeneratorExtensions
   {
      internal static void Emit(this ILGenerator @this,
         OpCode opCode, ISymbolDocumentWriter document, StreamWriter file, int lineNumber)
      {
         var line = opCode.Name;
         file.WriteLine(line);
         @this.MarkSequencePoint(document, lineNumber, 1, lineNumber, line.Length + 1);
         @this.Emit(opCode);
      }

      internal static void Emit(this ILGenerator @this,
         OpCode opCode, ConstructorInfo method, ISymbolDocumentWriter document, StreamWriter file, int lineNumber)
      {
         var line = $"{opCode.Name} {new MethodDescriptor(method, method.DeclaringType?.Assembly).Value}";
         file.WriteLine(line);
         @this.MarkSequencePoint(document, lineNumber, 1, lineNumber, line.Length + 1);
         @this.Emit(opCode, method);
      }

      internal static void Emit(this ILGenerator @this,
         OpCode opCode, MethodInfo method, ISymbolDocumentWriter document, StreamWriter file, int lineNumber)
      {
         var line = $"{opCode.Name} {new MethodDescriptor(method, method.DeclaringType?.Assembly).Value}";
         file.WriteLine(line);
         @this.MarkSequencePoint(document, lineNumber, 1, lineNumber, line.Length + 1);
         @this.Emit(opCode, method);
      }

      internal static void Emit(this ILGenerator @this,
         OpCode opCode, string value, ISymbolDocumentWriter document, StreamWriter file, int lineNumber)
      {
         var line = opCode.Name + " \"" + value + "\"";
         file.WriteLine(line);
         @this.MarkSequencePoint(document, lineNumber, 1, lineNumber, line.Length + 1);
         @this.Emit(opCode, value);
      }

      internal static void Emit(this ILGenerator @this,
         OpCode opCode, Type value, ISymbolDocumentWriter document, StreamWriter file, int lineNumber)
      {
         var line = $"{opCode.Name} {new TypeDescriptor(value).Value}";
         file.WriteLine(line);
         @this.MarkSequencePoint(document, lineNumber, 1, lineNumber, line.Length + 1);
         @this.Emit(opCode, value);
      }      
   }
}