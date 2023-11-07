using System.Diagnostics;

namespace MiscThings.Linear;

/// <summary>
///    Given an M-by-N matrix A, an M-length vector b, and an
///    N-length vector c, solve the  LP { max cx : Ax is less or equals b, x >= 0 }.
///    Assumes that b >= 0 so that x = 0 is a basic feasible solution.
///    Creates an (M+1)-by-(N+M+1) simplex tableaux with the
///    RHS in column M+N, the objective function in row M, and
///    slack variables in columns M through M+N-1.
/// </summary>
public sealed class Simplex
{
   // basis[i] = basic variable corresponding to row i, only needed to print out solution, not book
   private readonly int[] _basisVector;
   private readonly int _constraintsNum; // number of constraints
   private readonly double[][] _matrix; // tableaux
   private readonly double[] _optVector;
   private readonly double[] _rightVector;
   private readonly int _variablesNum; // number of original variables

   // sets up the simplex tableaux
   public Simplex(IReadOnlyList<double[]> matrix, double[] rightVector, double[] optVector)
   {
      _constraintsNum = rightVector.Length;
      _variablesNum = optVector.Length;
      _rightVector = rightVector;
      _optVector = optVector;
      _matrix = new double[_constraintsNum + 1][];
      for (var i = 0; i < _matrix.Length; i++)
      {
         _matrix[i] = new double[_variablesNum + _constraintsNum + 1];
      }

      for (var i = 0; i < _constraintsNum; i++)
      {
         for (var j = 0; j < _variablesNum; j++)
         {
            _matrix[i][j] = matrix[i][j];
         }
      }

      for (var i = 0; i < _constraintsNum; i++)
      {
         _matrix[i][_variablesNum + i] = 1.0;
      }

      for (var j = 0; j < _variablesNum; j++)
      {
         _matrix[_constraintsNum][j] = optVector[j];
      }

      for (var i = 0; i < _constraintsNum; i++)
      {
         _matrix[i][_constraintsNum + _variablesNum] = rightVector[i];
      }

      _basisVector = new int[_constraintsNum];
      for (var i = 0; i < _constraintsNum; i++)
      {
         _basisVector[i] = _variablesNum + i;
      }
   }

   /// <summary>
   ///    Optimal objective value
   /// </summary>
   public double OptimalValue => -_matrix[_constraintsNum][_constraintsNum + _variablesNum];

   /// <summary>
   ///    Primal solution vector
   /// </summary>
   public double[] SolutionVector
   {
      get
      {
         var sln = new double[_variablesNum];
         for (var i = 0; i < _constraintsNum; i++)
         {
            if (_basisVector[i] < _variablesNum)
            {
               sln[_basisVector[i]] = _matrix[i][_constraintsNum + _variablesNum];
            }
         }

         return sln;
      }
   }

   /// <summary>
   ///    Dual solution vector
   /// </summary>
   public double[] DualVector
   {
      get
      {
         var dualSln = new double[_constraintsNum];
         for (var i = 0; i < _constraintsNum; i++)
         {
            dualSln[i] = -_matrix[_constraintsNum][_variablesNum + i];
         }

         return dualSln;
      }
   }

   /// <summary>
   ///    Run simplex algorithm starting from initial BFS
   /// </summary>
   /// <exception cref="ArithmeticException">Linear program is unbounded</exception>
   public void Solve()
   {
      while (true)
      {
         // find entering column q
         var enteringCol = GetBland();
         if (enteringCol == -1)
         {
            break; // optimal
         }

         // find leaving row p
         var ratioRule = GetMinRatioRule(enteringCol);
         if (ratioRule == -1)
         {
#if DEBUG
            Debug.WriteLine("Linear program is unbounded");
#endif
            return;
         }

         // pivot
         Pivot(ratioRule, enteringCol);

         // update basis
         _basisVector[ratioRule] = enteringCol;
      }

      // check optimality conditions
#if TRACE
      Debug.WriteLineIf(!Check(_matrix, _rightVector, _optVector, out var errorMsg), errorMsg);
#endif
   }

   // lowest index of a non-basic column with a positive cost
   private int GetBland()
   {
      for (var j = 0; j < _constraintsNum + _variablesNum; j++)
      {
         if (_matrix[_constraintsNum][j] > 0)
         {
            return j;
         }
      }

      return -1; // optimal
   }

   // index of a non-basic column with most positive cost
   private int GetDantzig()
   {
      var qIdx = 0;
      for (var j = 1; j < _constraintsNum + _variablesNum; j++)
      {
         if (_matrix[_constraintsNum][j] > _matrix[_constraintsNum][qIdx])
         {
            qIdx = j;
         }
      }

      if (_matrix[_constraintsNum][qIdx] <= 0)
      {
         return -1; // optimal
      }

      return qIdx;
   }

   // find row p using min ratio rule (-1 if no such row)
   private int GetMinRatioRule(int colIdx)
   {
      var minRatioIdx = -1;
      for (var i = 0; i < _constraintsNum; i++)
      {
         if (_matrix[i][colIdx] <= 0)
         {
            continue;
         }

         if (minRatioIdx == -1)
         {
            minRatioIdx = i;
         }
         else if (_matrix[i][_constraintsNum + _variablesNum] / _matrix[i][colIdx]
                  < _matrix[minRatioIdx][_constraintsNum + _variablesNum] / _matrix[minRatioIdx][colIdx])
         {
            minRatioIdx = i;
         }
      }

      return minRatioIdx;
   }

   // pivot on entry (p, q) using Gauss-Jordan elimination
   private void Pivot(int pIdx, int qIdx)
   {
      // everything but row p and column q
      for (var i = 0; i <= _constraintsNum; i++)
      {
         for (var j = 0; j <= _constraintsNum + _variablesNum; j++)
         {
            if (i != pIdx && j != qIdx)
            {
               _matrix[i][j] -= _matrix[pIdx][j] * _matrix[i][qIdx] / _matrix[pIdx][qIdx];
            }
         }
      }

      // zero out column q
      for (var i = 0; i <= _constraintsNum; i++)
      {
         if (i != pIdx)
         {
            _matrix[i][qIdx] = 0.0;
         }
      }

      // scale row p
      for (var j = 0; j <= _constraintsNum + _variablesNum; j++)
      {
         if (j != qIdx)
         {
            _matrix[pIdx][j] /= _matrix[pIdx][qIdx];
         }
      }

      _matrix[pIdx][qIdx] = 1.0;
   }

   private bool Check(double[][] matrix, double[] rightVector, double[] optVector, out string errorMsg) =>
      IsPrimalFeasible(matrix, rightVector, out errorMsg)
      && IsDualFeasible(matrix, optVector, out errorMsg)
      && IsOptimal(rightVector, optVector, out errorMsg);

   // is the solution primal feasible?
   private bool IsPrimalFeasible(
      IReadOnlyList<double[]> matrix, IReadOnlyList<double> rightVector, out string errorMsg)
   {
      var sln = SolutionVector;

      // check that x >= 0
      for (var j = 0; j < sln.Length; j++)
      {
         if (sln[j] < 0.0)
         {
            errorMsg = $"x[{j}] = {sln[j]} is negative";
            return false;
         }
      }

      // check that Ax <= b
      for (var i = 0; i < _constraintsNum; i++)
      {
         var sum = 0.0;
         for (var j = 0; j < _variablesNum; j++)
         {
            sum += matrix[i][j] * sln[j];
         }

         if (sum > rightVector[i] + double.Epsilon)
         {
            errorMsg = "not primal feasible";
            errorMsg += $"b[{i}] = {rightVector[i]}, sum = {sum}";
            return false;
         }
      }

      errorMsg = string.Empty;
      return true;
   }

   // is the solution dual feasible?
   private bool IsDualFeasible(
      IReadOnlyList<double[]> matrix, IReadOnlyList<double> optVector, out string errorMsg)
   {
      var dual = DualVector;

      // check that y >= 0
      for (var i = 0; i < dual.Length; i++)
      {
         if (dual[i] < 0.0)
         {
            errorMsg = $"y[{i}] = {dual[i]} is negative";
            return false;
         }
      }

      // check that yA >= c
      for (var j = 0; j < _variablesNum; j++)
      {
         var sum = 0.0;
         for (var i = 0; i < _constraintsNum; i++)
         {
            sum += matrix[i][j] * dual[i];
         }

         if (sum < optVector[j] - double.Epsilon)
         {
            errorMsg = "not dual feasible";
            errorMsg += $"c[{j}] = {optVector[j]}, sum = {sum}";
            return false;
         }
      }

      errorMsg = string.Empty;
      return true;
   }

   // check that optimal value = cx = yb
   private bool IsOptimal(
      IReadOnlyList<double> rightVector, IReadOnlyList<double> optVector, out string errorMsg)
   {
      var sln = SolutionVector;
      var dual = DualVector;
      var value = OptimalValue;

      // check that value = cx = yb
      var value1St = sln.Select((t, j) => optVector[j] * t).Sum();
      var value2Nd = dual.Select((t, i) => t * rightVector[i]).Sum();
      if (Math.Abs(value - value1St) > double.Epsilon
          || Math.Abs(value - value2Nd) > double.Epsilon)
      {
         errorMsg = $"value = {value}, cx = {value1St}, yb = {value2Nd}";
         return false;
      }

      errorMsg = string.Empty;
      return true;
   }
}