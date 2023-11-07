// Статический интерцептор

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using StaticInterceptionViaMonoCecil;
using static Mono.Cecil.Cil.OpCodes;

namespace StaticCecilInterceptor
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         if (args.Length <= 0)
            return;

         var mode = args[0];
         var path = args[1];
         switch (mode)
         {
            case "-dump":
               var methodName = args.Length > 2 ? args[2] : string.Empty;
               DumpAssembly(path, methodName);
               break;

            case "-inject":
               InjectToAssembly(args[1]);
               break;

            default:
               return;
         }
      }

      private static void DumpAssembly(string aPath, string aMethodName)
      {
         File.AppendAllText("dump.txt", $"Dump started...{Environment.NewLine}");
         var assembly = AssemblyDefinition.ReadAssembly(aPath);
         foreach (var method in from typeDef in assembly.MainModule.Types
                                from method in typeDef.Methods
                                where string.IsNullOrEmpty(aMethodName) || method.Name == aMethodName
                                select method)
         {
            File.AppendAllText("dump.txt", $"Method: {method}");
            File.AppendAllText("dump.txt", Environment.NewLine);
            foreach (var instruction in method.Body.Instructions)
            {
               File.AppendAllText("dump.txt", $"{instruction}{Environment.NewLine}");
            }
         }
      }

      private static void InjectToAssembly(string aPath)
      {
         var assembly = AssemblyDefinition.ReadAssembly(aPath);

         // Ссылка на GetCurrentMethod
         var getCurrentMethodRef =
            assembly.MainModule.Import(typeof(MethodBase).GetMethod(nameof(MethodBase.GetCurrentMethod)));

         // Ссылка на Attribute.GetCustomAttribute
         var getCustomAttributeRef =
            assembly.MainModule.Import(typeof(Attribute).GetMethod(nameof(Attribute.GetCustomAttribute),
               new[] { typeof(MethodInfo), typeof(Type) }));

         // Ссылка на Type.GetTypeFromHandle() - аналог typeof()
         var getTypeFromHandleRef = assembly.MainModule.Import(typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle)));

         // Ссылка на тип MethodBase
         var methodBaseRef = assembly.MainModule.Import(typeof(MethodBase));

         // Ссылка на тип MethodInterceptionAttribute
         var methodInterceptionAttributeRef = assembly.MainModule.Import(typeof(MethodInterceptionAttribute));

         // Ссылка на MethodInterceptionAttribute.OnEnter
         var onEnterMethodRef = assembly.MainModule.Import(
            typeof(MethodInterceptionAttribute).GetMethod(nameof(MethodInterceptionAttribute.OnEnter)));

         // Ссылка на тип Dictionary<string, object>
         var dictionaryType = Type.GetType(typeof(Dictionary<string, object>).FullName);
         Debug.Assert(dictionaryType != null, "dictionaryType != null");

         var dictStringObjectRef = assembly.MainModule.Import(dictionaryType);
         var dictCtorRef = assembly.MainModule.Import(dictionaryType.GetConstructor(Type.EmptyTypes));
         var dictMethodAddRef =
            assembly.MainModule.Import(dictionaryType.GetMethod(nameof(Dictionary<string, object>.Add)));

         foreach (var typeDef in assembly.MainModule.Types)
         {
            foreach (
               var method in
                  typeDef.Methods.Where(
                     m =>
                        m.CustomAttributes
                           .FirstOrDefault(
                              attr => attr.AttributeType.Resolve().BaseType.Name == nameof(MethodInterceptionAttribute)) != null))
            {
               var ilProc = method.Body.GetILProcessor();

               // Необходимо установить InitLocals в true, так как если он находился в false
               // (в методе изначально не было локальных переменных),
               // а теперь локальные переменные появятся - верификатор IL-кода выдаст ошибку
               method.Body.InitLocals = true;

               // Создаем три локальных переменных для attribute, currentMethod и parameters
               var attributeVariable = new VariableDefinition(methodInterceptionAttributeRef);
               var currentMethodVar = new VariableDefinition(methodBaseRef);
               var parametersVariable = new VariableDefinition(dictStringObjectRef);
               ilProc.Body.Variables.Add(attributeVariable);
               ilProc.Body.Variables.Add(currentMethodVar);
               ilProc.Body.Variables.Add(parametersVariable);

               var firstInstruction = ilProc.Body.Instructions[0];
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Nop));

               // Получаем текущий метод
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Call, getCurrentMethodRef));

               // Помещаем результат со стека в переменную currentMethodVar
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Stloc, currentMethodVar));

               // Загружаем на стек ссылку на текущий метод
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Ldloc, currentMethodVar));

               // Загружаем ссылку на тип MethodInterceptionAttribute
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Ldtoken, methodInterceptionAttributeRef));

               // Вызывеам GetTypeFromHandle (в него транслируется typeof()) - эквивалент typeof(MethodInterceptionAttribute)
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Call, getTypeFromHandleRef));

               // Теперь у нас на стеке текущий метод и тип MethodInterceptionAttribute. Вызываем Attribute.GetCustomAttribute
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Call, getCustomAttributeRef));

               // Приводим результат к типу MethodInterceptionAttribute
               ilProc.InsertBefore(firstInstruction,
                  Instruction.Create(Castclass, methodInterceptionAttributeRef));

               // Сохраняем в локальной переменной attributeVariable
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Stloc, attributeVariable));

               // Создаем новый Dictionary<string, object>
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Newobj, dictCtorRef));

               // Помещаем в parametersVariable
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Stloc, parametersVariable));

               foreach (var argument in method.Parameters)
               {
                  // Для каждого аргумента метода загружаем на стек наш Dictionary<string,object>
                  ilProc.InsertBefore(firstInstruction, Instruction.Create(Ldloc, parametersVariable));

                  // Загружаем имя аргумента
                  ilProc.InsertBefore(firstInstruction, Instruction.Create(Ldstr, argument.Name));

                  // Загружаем значение аргумента
                  ilProc.InsertBefore(firstInstruction, Instruction.Create(Ldarg, argument));

                  // Вызывем Dictionary.Add(string key, object value)
                  ilProc.InsertBefore(firstInstruction, Instruction.Create(Call, dictMethodAddRef));
               }

               // Загружаем на стек сначала атрибут, потом параметры для вызова его метода OnEnter
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Ldloc, attributeVariable));
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Ldloc, currentMethodVar));
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Ldloc, parametersVariable));

               // Вызываем OnEnter. На стеке должен быть объект, на котором вызывается OnEnter и параметры метода
               ilProc.InsertBefore(firstInstruction, Instruction.Create(Callvirt, onEnterMethodRef));
            }
         }

         assembly.Write(aPath);
      }
   }
}