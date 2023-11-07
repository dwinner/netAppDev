using System.Collections;
using MiscThings.Linear;

namespace MiscThings.Tests.Linear;

[TestFixture]
public class GaussianEliminationTests
{
   public static IEnumerable LinearInput
   {
      get
      {
         yield return new TestCaseData(
            new[]
            {
               new double[] { 0, 1, 1 },
               new double[] { 2, 4, -2 },
               new double[] { 0, 3, 15 }
            },
            new double[] { 4, 2, 36 },
            "test 1 (3-by-3 system, nonsingular)"
         );
         yield return new TestCaseData(
            new[]
            {
               new double[] { 1, -3, 1 },
               new double[] { 2, -8, 8 },
               new double[] { -6, 3, -15 }
            },
            new double[] { 4, -2, 9 },
            "test 2 (3-by-3 system, nonsingular)"
         );
         yield return new TestCaseData(
            new[]
            {
               new double[] { 2, -3, -1, 2, 3 },
               new double[] { 4, -4, -1, 4, 11 },
               new double[] { 2, -5, -2, 2, -1 },
               new double[] { 0, 2, 1, 0, 4 },
               new double[] { -4, 6, 0, 0, 7 }
            },
            new double[] { 4, 4, 9, -6, 5 },
            "test 3 (5-by-5 system, no solutions)"
         );
         yield return new TestCaseData(
            new[]
            {
               new double[] { 2, -3, -1, 2, 3 },
               new double[] { 4, -4, -1, 4, 11 },
               new double[] { 2, -5, -2, 2, -1 },
               new double[] { 0, 2, 1, 0, 4 },
               new double[] { -4, 6, 0, 0, 7 }
            },
            new double[] { 4, 4, 9, -5, 5 },
            "test 4 (5-by-5 system, infinitely many solutions)"
         );
         yield return new TestCaseData(
            new[]
            {
               new double[] { 2, -1, 1 },
               new double[] { 3, 2, -4 },
               new double[] { -6, 3, -3 }
            },
            new double[] { 1, 4, 2 },
            "test 5 (3-by-3 system, no solutions)"
         );
         yield return new TestCaseData(
            new[]
            {
               new double[] { 1, -1, 2 },
               new double[] { 4, 4, -2 },
               new double[] { -2, 2, -4 }
            },
            new double[] { -3, 1, 6 },
            "test 6 (3-by-3 system, infinitely many solutions)"
         );
         yield return new TestCaseData(
            new[]
            {
               new double[] { 0, 1, 1 },
               new double[] { 2, 4, -2 },
               new double[] { 0, 3, 15 },
               new double[] { 2, 8, 14 }
            },
            new double[] { 4, 2, 36, 42 },
            "test 7 (4-by-3 system, full rank)"
         );
         yield return new TestCaseData(
            new[]
            {
               new double[] { 0, 1, 1 },
               new double[] { 2, 4, -2 },
               new double[] { 0, 3, 15 },
               new double[] { 2, 8, 14 }
            },
            new double[] { 4, 2, 36, 40 },
            "test 8 (4-by-3 system, no solution)"
         );
         yield return new TestCaseData(
            new[]
            {
               new double[] { 1, -3, 1, 1 },
               new double[] { 2, -8, 8, 2 },
               new double[] { -6, 3, -15, 3 }
            },
            new double[] { 4, -2, 9 },
            "test 9 (3-by-4 system, full rank)"
         );
      }
   }

   [Test(Description = "Linear eq. via Gaussian elimination")]
   [TestCaseSource(typeof(GaussianEliminationTests), nameof(LinearInput))]
   public void GaussianEliminationTest(double[][] matrix, double[] lenVector, string description)
   {
      Console.Write($"{description}: ");
      GaussianElimination elimination = new(matrix, lenVector);
      elimination.ApplyForward();
      var solution = elimination.ApplyPrimal();
      if (solution != null && elimination.IsFeasible)
      {
         foreach (var sItem in solution)
         {
            Console.Write("{0:F6} ", sItem);
         }
      }
      else
      {
         Console.WriteLine("System is infeasible");
      }
   }
}