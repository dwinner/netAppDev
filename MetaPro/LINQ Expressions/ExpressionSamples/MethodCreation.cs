using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using static System.Linq.Expressions.Expression;
using static System.Reflection.Emit.OpCodes;

namespace ExpressionSamples
{
   internal static class MethodCreation
   {
      // f(x) = ((3 * x) / 2) + 4
      internal static Tuple<Func<double, double>, TimeSpan> CreateViaExpression()
      {
         var stopwatch = Stopwatch.StartNew();
         var parameter = Parameter(typeof (double));
         var method =
            Lambda(Add(Divide(Multiply(Constant(3d), parameter), Constant(2d)), Constant(4d)), parameter).Compile() as
               Func<double, double>;
         stopwatch.Stop();

         return new Tuple<Func<double, double>, TimeSpan>(method, stopwatch.Elapsed);
      }

      internal static Tuple<Func<double, double>, TimeSpan> CreateViaDynamicMethod()
      {
         var stopwatch = Stopwatch.StartNew();
         var method = new DynamicMethod("m", typeof (double), new[] {typeof (double)});
         method.DefineParameter(1, ParameterAttributes.In, "x");
         var generator = method.GetILGenerator();
         generator.Emit(Ldc_R8, 3d);
         generator.Emit(Ldarg_0);
         generator.Emit(Mul);
         generator.Emit(Ldc_R8, 2d);
         generator.Emit(Div);
         generator.Emit(Ldc_R8, 4d);
         generator.Emit(OpCodes.Add);
         generator.Emit(Ret);
         var compiledMethod = method.CreateDelegate(typeof (Func<double, double>)) as Func<double, double>;
         stopwatch.Stop();

         return new Tuple<Func<double, double>, TimeSpan>(compiledMethod, stopwatch.Elapsed);
      }
   }
}