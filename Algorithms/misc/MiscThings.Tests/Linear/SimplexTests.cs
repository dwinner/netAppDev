using System.Collections;
using MiscThings.Linear;

namespace MiscThings.Tests.Linear;

[TestFixture]
public class SimplexTests
{
   public static IEnumerable LinearInput
   {
      get
      {
         yield return new TestCaseData(
            new[]
            {
               new double[] { -1, 1, 0 },
               new double[] { 1, 4, 0 },
               new double[] { 2, 1, 0 },
               new double[] { 3, -4, 0 },
               new double[] { 0, 0, 1 }
            },
            new double[] { 5, 45, 27, 24, 4 },
            new double[] { 1, 1, 1 },
            "Demo"
         );
         yield return new TestCaseData(
            new[]
            {
               new[] { 5.0, 15.0 },
               new[] { 4.0, 4.0 },
               new[] { 35.0, 20.0 }
            },
            new[] { 480.0, 160.0, 1190.0 },
            new[] { 13.0, 23.0 },
            "x0 = 12, x1 = 28, opt = 800"
         );
         yield return new TestCaseData(
            new[]
            {
               new[] { -2.0, -9.0, 1.0, 9.0 },
               new[] { 1.0, 1.0, -1.0, -2.0 }
            },
            new[] { 3.0, 2.0 },
            new[] { 2.0, 3.0, -1.0, -12.0 },
            "unbounded"
         );
         yield return new TestCaseData(
            new[]
            {
               new[] { 0.5, -5.5, -2.5, 9.0 },
               new[] { 0.5, -1.5, -0.5, 1.0 },
               new[] { 1.0, 0.0, 0.0, 0.0 }
            },
            new[] { 0.0, 0.0, 1.0 },
            new[] { 10.0, -57.0, -9.0, -24.0 },
            "degenerate - cycles if you choose most positive objective function coefficient"
         );
      }
   }

   [Test(Description = "LP solution via Simplex method")]
   [TestCaseSource(typeof(SimplexTests), nameof(LinearInput))]
   public void SimplexTest(double[][] A, double[] b, double[] c, string desc)
   {
      Console.WriteLine($"{desc}: ");
      var simplexLp = new Simplex(A, b, c);
      simplexLp.Solve();
      Console.WriteLine("Value = " + simplexLp.OptimalValue);
      var solution = simplexLp.SolutionVector;
      for (var i = 0; i < solution.Length; i++)
      {
         Console.WriteLine($"x[{i}] = {solution[i]}");
      }

      var dual = simplexLp.DualVector;
      for (var j = 0; j < dual.Length; j++)
      {
         Console.WriteLine($"y[{j}] = {dual[j]}");
      }
   }
}