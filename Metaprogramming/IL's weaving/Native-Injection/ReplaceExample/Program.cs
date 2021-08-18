using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using NativeAssemblerInjection;

namespace ReplaceExample
{
   internal class Program
   {
      private static void Main()
      {
         // Enable Jit debugging
         if (IntPtr.Size == 4)
            Console.WriteLine("Enabling JIT debugging.");

         // Run static method replace
         StaticTests();

         // Run instance replace tests
         InstanceTests();

         // Run the dynamic tests
         DynamicTests();

         Console.ReadLine();
      }

      private static void StaticTests()
      {
         MethodBase[] methods =
         {
            typeof(StaticClassA).GetMethod("A", BindingFlags.Static | BindingFlags.Public),
            typeof(StaticClassA).GetMethod("B", BindingFlags.Static | BindingFlags.Public),
            typeof(StaticClassB).GetMethod("A", BindingFlags.Static | BindingFlags.Public),
            typeof(StaticClassB).GetMethod("B", BindingFlags.Static | BindingFlags.Public)
         };

         // Jit TestStaticReplaceJited
         RuntimeHelpers.PrepareMethod(
            typeof(Program).GetMethod("TestStaticReplaceJited", BindingFlags.Static | BindingFlags.NonPublic)
               .MethodHandle);

         // Replace StaticClassA.A() with StaticClassB.A()
         Console.WriteLine("Replacing StaticClassA.A() with StaticClassB.A()");
         MethodUtil.ReplaceMethod(methods[2], methods[0]);

         // Call StaticClassA.A() from a  method that has already been jited
         Console.WriteLine("Call StaticClassA.A() from a  method that has already been jited");
         TestStaticReplaceJited();

         // Call StaticClassA.A() from a  method that has not been jited
         Console.WriteLine("Call StaticClassA.A() from a  method that has not been jited");
         TestStaticReplace();
      }

      [MethodImpl(MethodImplOptions.NoInlining)]
      private static void TestStaticReplace()
      {
         StaticClassA.A();
      }

      [MethodImpl(MethodImplOptions.NoInlining)]
      private static void TestStaticReplaceJited()
      {
         StaticClassA.A();
      }

      private static void InstanceTests()
      {
         MethodBase[] methods =
         {
            typeof(InstanceClassA).GetMethod("A", BindingFlags.Instance | BindingFlags.Public),
            typeof(InstanceClassA).GetMethod("B", BindingFlags.Instance | BindingFlags.Public),
            typeof(InstanceClassB).GetMethod("A", BindingFlags.Instance | BindingFlags.Public),
            typeof(InstanceClassB).GetMethod("B", BindingFlags.Instance | BindingFlags.Public)
         };

         // Jit TestStaticReplaceJited
         RuntimeHelpers.PrepareMethod(
            typeof(Program).GetMethod("TestInstanceReplaceJited", BindingFlags.Static | BindingFlags.NonPublic)
               .MethodHandle);

         // Replace StaticClassA.A() with StaticClassB.A()
         Console.WriteLine("Replacing InstanceClassA.A() with InstanceClassB.A()");
         MethodUtil.ReplaceMethod(methods[2], methods[0]);

         // Call StaticClassA.A() from a  method that has already been jited
         Console.WriteLine("Call InstanceClassA.A() from a  method that has already been jited");
         TestInstanceReplaceJited();

         // Call StaticClassA.A() from a  method that has not been jited
         Console.WriteLine("Call InstanceClassA.A() from a  method that has not been jited");
         TestInstanceReplace();
      }

      [MethodImpl(MethodImplOptions.NoInlining)]
      private static void TestInstanceReplace()
      {
         var a = new InstanceClassA();
         a.A();
      }

      [MethodImpl(MethodImplOptions.NoInlining)]
      private static void TestInstanceReplaceJited()
      {
         var a = new InstanceClassA();
         a.A();
      }

      private static void DynamicTests()
      {
         // Create dynamic method C in StaticClassA
         var dynamicMethod = CreateTestMethod(typeof(StaticClassA), "C");

         // Get methodbase for class 
         MethodBase method = typeof(StaticClassA).GetMethod("B", BindingFlags.Static | BindingFlags.Public);

         // Jit TestStaticReplaceJited
         RuntimeHelpers.PrepareMethod(typeof(Program)
            .GetMethod("TestDynamicReplaceJited", BindingFlags.Static | BindingFlags.NonPublic).MethodHandle);

         // Replace StaticClassA.B() with dynamic StaticClassA.C()
         Console.WriteLine("Replacing StaticClassA.B() with dynamic StaticClassA.C()");
         MethodUtil.ReplaceMethod(dynamicMethod, method);

         // Call StaticClassA.B() from a  method that has already been jited
         Console.WriteLine("Call StaticClassA.B() from a  method that has already been jited");
         TestDynamicReplaceJited();

         // Call StaticClassA.B() from a  method that has not been jited
         Console.WriteLine("Call StaticClassA.B() from a  method that has not been jited");
         TestDynamicReplace();
      }

      [MethodImpl(MethodImplOptions.NoInlining)]
      private static void TestDynamicReplace()
      {
         StaticClassA.B();
      }

      [MethodImpl(MethodImplOptions.NoInlining)]
      private static void TestDynamicReplaceJited()
      {
         StaticClassA.B();
      }

      private static DynamicMethod CreateTestMethod(Type type, string name)
      {
         var dynamicMethod =
            new DynamicMethod(
               name,
               MethodAttributes.Static | MethodAttributes.Public,
               CallingConventions.Standard,
               typeof(void),
               Type.EmptyTypes,
               type,
               false
            );

         // emit 
         var gen = dynamicMethod.GetILGenerator();
         gen.EmitWriteLine($"{type.Name}.{name}");
         gen.Emit(OpCodes.Ret);

         // Test the dynamic method to make sure it works.
         Console.WriteLine("Created new dynamic metbod {0}.{1}", type.Name, name);

         // Need to call create delegate 
         // var action = dynamicMethod.CreateDelegate(typeof(Action)) as Action;
         //  action();

         return dynamicMethod;
      }
   }
}