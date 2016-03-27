/**
 * Создание динамической сборки
 */

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace DynamicAssemblyBuilder
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("***** The Amazing Dynamic Assembly Builder App *****");
         // Get the application domain for the current thread.
         var curAppDomain = Thread.GetDomain();

         // Create the dynamic assembly using our helper f(x).
         CreateMyAsm(curAppDomain);
         Console.WriteLine("-> Finished creating MyAssembly.dll.");

         // Now load the new assembly from file.
         Console.WriteLine("-> Loading MyAssembly.dll from file.");
         var a = Assembly.Load("MyAssembly");

         // Get the HelloWorld type.
         var hello = a.GetType("MyAssembly.HelloWorld");

         // Create HelloWorld object and call the correct ctor.
         Console.Write("-> Enter message to pass HelloWorld class: ");
         var msg = Console.ReadLine();
         var ctorArgs = new object[1];
         ctorArgs[0] = msg;
         var obj = Activator.CreateInstance(hello, ctorArgs);

         // Call SayHello and show returned string.
         Console.WriteLine("-> Calling SayHello() via late binding.");
         var mi = hello.GetMethod("SayHello");
         mi.Invoke(obj, null);

         // Invoke method. 
         mi = hello.GetMethod("GetMsg");
         Console.WriteLine(mi.Invoke(obj, null));
      }

      private static void CreateMyAsm(AppDomain curAppDomain)
      {
         // Establish general assembly characteristics.
         var assemblyName = new AssemblyName { Name = "MyAssembly", Version = new Version("1.0.0.0") };

         // Create new assembly within the current AppDomain.
         var assembly =
            curAppDomain.DefineDynamicAssembly(assemblyName,
               AssemblyBuilderAccess.Save);

         // Given that we are building a single-file
         // assembly, the name of the module is the same as the assembly.
         var module =
            assembly.DefineDynamicModule("MyAssembly", "MyAssembly.dll");

         // Define a public class named "HelloWorld".
         var helloWorldClass = module.DefineType("MyAssembly.HelloWorld",
            TypeAttributes.Public);

         // Define a private String member variable named "theMessage".
         var stringType = Type.GetType("System.String");
         if (stringType != null)
         {
            var msgField =
               helloWorldClass.DefineField("theMessage", stringType,
                  FieldAttributes.Private);

            // Create the custom ctor.
            var constructorArgs = new Type[1];
            constructorArgs[0] = typeof(string);
            var constructor =
               helloWorldClass.DefineConstructor(MethodAttributes.Public,
                  CallingConventions.Standard,
                  constructorArgs);
            var constructorIl = constructor.GetILGenerator();
            constructorIl.Emit(OpCodes.Ldarg_0);
            var objectClass = typeof(object);
            var superConstructor =
               objectClass.GetConstructor(new Type[0]);
            if (superConstructor != null)
            {
               constructorIl.Emit(OpCodes.Call, superConstructor);
            }
            constructorIl.Emit(OpCodes.Ldarg_0);
            constructorIl.Emit(OpCodes.Ldarg_1);
            constructorIl.Emit(OpCodes.Stfld, msgField);
            constructorIl.Emit(OpCodes.Ret);

            // Create the default ctor.
            helloWorldClass.DefineDefaultConstructor(MethodAttributes.Public);

            // Now create the GetMsg() method.
            var getMsgMethod =
               helloWorldClass.DefineMethod("GetMsg", MethodAttributes.Public,
                  typeof(string), null);
            var methodIl = getMsgMethod.GetILGenerator();
            methodIl.Emit(OpCodes.Ldarg_0);
            methodIl.Emit(OpCodes.Ldfld, msgField);
            methodIl.Emit(OpCodes.Ret);

            // Create the SayHello method.
            var sayHiMethod =
               helloWorldClass.DefineMethod("SayHello",
                  MethodAttributes.Public, null, null);
            methodIl = sayHiMethod.GetILGenerator();
            methodIl.EmitWriteLine("Hello from the HelloWorld class!");
            methodIl.Emit(OpCodes.Ret);
         }

         // 'Bake' the class HelloWorld.
         // (Baking is the formal term for emitting the type)
         helloWorldClass.CreateType();

         // (Optionally) save the assembly to file.
         assembly.Save("MyAssembly.dll");
      }
   }
}