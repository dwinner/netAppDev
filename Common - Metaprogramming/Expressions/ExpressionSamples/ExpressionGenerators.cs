using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using static System.Linq.Expressions.Expression;

namespace ExpressionSamples
{
   public static class ExpressionGenerators
   {
      public static int AddWithDebugging(int a, int b)
      {
         var name = Guid.NewGuid().ToString("N");
         var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(name), AssemblyBuilderAccess.Run);
         var module = assembly.DefineDynamicModule(name, true);
         var type = module.DefineType(Guid.NewGuid().ToString("N"), TypeAttributes.Public);
         var methodName = Guid.NewGuid().ToString("N");
         var method =
            type.DefineMethod(
               methodName, MethodAttributes.Public | MethodAttributes.Static, typeof (int),
               new[] {typeof (int), typeof (int)});

         var generator = DebugInfoGenerator.CreatePdbGenerator();
         var document = SymbolDocument("AddDebug.txt");

         var x = Parameter(typeof (int));
         var y = Parameter(typeof (int));

         var addDebugInfo = DebugInfo(document, 6, 9, 6, 22);
         var add = Expression.Add(x, y);
         var addBlock = Block(addDebugInfo, add);

         var lambda = Lambda(addBlock, x, y);
         lambda.CompileToMethod(method, generator);

         var bakedType = type.CreateType();
         return (int) bakedType.GetMethod(methodName).Invoke(null, new object[] {a, b});
      }

      public static TOut Add<TOut, TInFirst, TInSecond>(TInFirst a, TInSecond b)
      {
         var x = Parameter(typeof (TInFirst));
         var y = Parameter(typeof (TInSecond));
         var add = Expression.Add(x, y);
         var lambda = Lambda(add, x, y);
         var func = lambda.Compile() as Func<TInFirst, TInSecond, TOut>;
         return func != null ? func(a, b) : default(TOut);
      }

      public static int AddWithHandlers(int a, int b)
      {
         var x = Parameter(typeof (int));
         var y = Parameter(typeof (int));
         var lambda = Lambda(TryCatch(Block(AddChecked(x, y)), Catch(typeof (OverflowException), Constant(0))), x, y);
         var func = lambda.Compile() as Func<int, int, int>;
         return func?.Invoke(a, b) ?? default(int);
      }

      public static int Branching(bool doSwitch)
      {
         var @switch = Parameter(typeof (bool));
         var conditional = Condition(@switch, Constant(1), Constant(0));
         var func = Lambda(conditional, @switch).Compile() as Func<bool, int>;
         return func?.Invoke(doSwitch) ?? default(int);
      }
   }
}