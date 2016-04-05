using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace MyAsmBuilder
{
   internal static class Program
   {
      private static void Main()
      {
         var currentDomain = Thread.GetDomain();
         CreateMyAsm(currentDomain);
         var dynamicAssembly = Assembly.Load("MyAssembly");
         var helloType = dynamicAssembly.GetType("MyAssembly.HelloWorld");

         Console.WriteLine("Enter a message to pass HelloWorld class:");
         var message = Console.ReadLine();
         
         var ctorArgs = new object[] { message };
         var helloInstance = Activator.CreateInstance(helloType, ctorArgs);
         var sayHelloMethodInfo = helloType.GetMethod("SayHello");
         sayHelloMethodInfo.Invoke(helloInstance, null);

         var getMsgMethodInfo = helloType.GetMethod("GetMsg");
         Console.WriteLine(getMsgMethodInfo.Invoke(helloInstance, null));
      }

      /// <summary>
      ///    Создание сборки "на лету"
      /// </summary>
      /// <param name="domain">Домен приложения</param>
      private static void CreateMyAsm(AppDomain domain)
      {
         // Установка общих характеристик сборки
         var assemblyName = new AssemblyName
         {
            Name = "MyAssembly",
            Version = new Version("1.0.0.0")
         };

         // Создание новой сборки в текущем домене приложения
         var assembly = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);

         // Поскольку создается однофайловая сборка, имя модуля должно совпадать с именем самой сборки
         var module = assembly.DefineDynamicModule(assemblyName.Name, string.Format("{0}.dll", assemblyName.Name));

         // Определение общедоступного класса по имени HelloWorld
         var helloWorldClass = module.DefineType(string.Format("{0}.HelloWorld", assemblyName.Name),
            TypeAttributes.Public);

         // Определение приватной строковой переменной экземпляра по имени theMessage
         var stringType = Type.GetType("System.String");
         Debug.Assert(stringType != null, "stringType != null");
         var messageFld = helloWorldClass.DefineField("theMessage", stringType, FieldAttributes.Private);

         // Создание специального конструктора
         var ctorArgs = new[] { typeof(string) };
         var constructor = helloWorldClass.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard,
            ctorArgs);
         var ctorIlGenerator = constructor.GetILGenerator();
         ctorIlGenerator.Emit(OpCodes.Ldarg_0);
         var objType = typeof(object);
         var superCtor = objType.GetConstructor(new Type[0]);
         Debug.Assert(superCtor != null, "superCtor != null");
         ctorIlGenerator.Emit(OpCodes.Call, superCtor);
         ctorIlGenerator.Emit(OpCodes.Ldarg_0);
         ctorIlGenerator.Emit(OpCodes.Ldarg_1);
         ctorIlGenerator.Emit(OpCodes.Stfld, messageFld);
         ctorIlGenerator.Emit(OpCodes.Ret);

         // Создание конструктора по умолчанию
         helloWorldClass.DefineDefaultConstructor(MethodAttributes.Public);

         // Создание метода GetMsg()
         var getMessageMethodBuilder = helloWorldClass.DefineMethod("GetMsg", MethodAttributes.Public, typeof(string),
            null);
         var methodIlGenerator = getMessageMethodBuilder.GetILGenerator();
         methodIlGenerator.Emit(OpCodes.Ldarg_0);
         methodIlGenerator.Emit(OpCodes.Ldfld, messageFld);
         methodIlGenerator.Emit(OpCodes.Ret);

         // Создание метода SayHello
         var sayHiMethodBuilder = helloWorldClass.DefineMethod("SayHello", MethodAttributes.Public, null, null);
         var sayHiIlGenerator = sayHiMethodBuilder.GetILGenerator();
         sayHiIlGenerator.EmitWriteLine("Hello from the HelloWorld class!");
         sayHiIlGenerator.Emit(OpCodes.Ret);

         // Генерация класса HelloWorld
         helloWorldClass.CreateType();

         // Сохранение сборки в файле
         assembly.Save("MyAssembly.dll");
      }
   }
}