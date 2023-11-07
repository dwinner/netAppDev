using System.Diagnostics;

namespace MiscThings.Linear;

/// <summary>
///    The <see cref="GaussianElimination" /> data type provides methods
///    to solve a linear system of equations <em>Ax</em> = <em>b</em>,
///    where <em>A</em> is an <em>m</em>-by-<em>n</em> matrix
///    and <em>b</em> is a length <em>n</em> vector.
///    <p>
///       This is a bare-bones implementation that uses Gaussian elimination
///       with partial pivoting.
///       See
///       <a href="https://algs4.cs.princeton.edu/99scientific/GaussianEliminationLite.java.html">GaussianEliminationLite.java</a>
///       for a stripped-down version that assumes the matrix <em>A</em> is square
///       and nonsingular. See GaussJordanElimination for an alternate
///       implementation that uses Gauss-Jordan elimination.
///       For an industrial-strength numerical linear algebra library,
///       see <a href="http://math.nist.gov/javanumerics/jama/">JAMA</a>.
///    </p>
///    <p>
///       This computes correct results if all arithmetic performed is
///       without floating-point rounding error or arithmetic overflow.
///       In practice, there will be floating-point rounding error;
///       partial pivoting helps prevent accumulated floating-point rounding
///       errors from growing out of control (though it does not
///       provide any guarantees).
///    </p>
/// </summary>
public sealed class GaussianElimination
{
   private readonly int _colsNum; // number of columns
   private readonly double[] _lenVector;
   private readonly double[][] _matrix; // m-by-(n+1) augmented matrix
   private readonly int _rowsNum; // number of rows

   /// <summary>
   ///    Solves the linear system of equations <em>Ax</em> = <em>b</em>,
   ///    where <em>A</em> is an <em>m</em>-by-<em>n</em> matrix and <em>b</em>
   ///    is a length <em>m</em> vector.
   /// </summary>
   /// <param name="inputMatrix">The <em>m</em>-by-<em>n</em> constraint matrix</param>
   /// <param name="lenVector">The length <em>m</em> right-hand-side vector</param>
   /// <exception cref="ArgumentException">
   ///    If the dimensions disagree, i.e., the length of
   ///    <param name="lenVector"></param>
   ///    does not equal
   ///    <param name="inputMatrix"></param>
   /// </exception>
   public GaussianElimination(IReadOnlyList<double[]> inputMatrix, double[] lenVector)
   {
      _rowsNum = inputMatrix.Count;
      _colsNum = inputMatrix[0].Length;
      if (lenVector.Length != _rowsNum)
      {
         throw new ArgumentException("Dimensions disagree", nameof(lenVector));
      }

      _lenVector = lenVector;

      // build augmented matrix
      _matrix = new double[_rowsNum][];
      for (var i = 0; i < _rowsNum; i++)
      {
         _matrix[i] = new double[_colsNum + 1];
      }

      for (var i = 0; i < _rowsNum; i++)
      {
         for (var j = 0; j < _colsNum; j++)
         {
            _matrix[i][j] = inputMatrix[i][j];
         }
      }

      for (var i = 0; i < _rowsNum; i++)
      {
         _matrix[i][_colsNum] = lenVector[i];
      }
   }

   /// <summary>
   ///    Returns true if there exists a solution to the linear system of equations <em>Ax</em> = <em>b</em>.
   /// </summary>
   /// <value>
   ///    true if there is a solution to the linear system of equations <em>Ax</em> = <em>b</em>;
   ///    false otherwise
   /// </value>
   public bool IsFeasible => ApplyPrimal() != null;

   public void ApplyForward()
   {
      ForwardElimination();
#if TRACE
      Debug.WriteLineIf(
         !CertifySolution(_matrix, _lenVector, out var errorMessage), errorMessage);
#endif
   }

   // forward elimination
   private void ForwardElimination()
   {
      for (var idx = 0; idx < Math.Min(_rowsNum, _colsNum); idx++)
      {
         // find pivot row using partial pivoting
         var max = idx;
         for (var i = idx + 1; i < _rowsNum; i++)
         {
            if (Math.Abs(_matrix[i][idx]) > Math.Abs(_matrix[max][idx]))
            {
               max = i;
            }
         }

         // swap
         Swap(idx, max);

         // singular or nearly singular
         if (Math.Abs(_matrix[idx][idx]) <= double.Epsilon)
         {
            continue;
         }

         // pivot
         Pivot(idx);
      }
   }

   // swap row1 and row2
   private void Swap(int row1, int row2) =>
      (_matrix[row1], _matrix[row2]) = (_matrix[row2], _matrix[row1]);

   // pivot on a[p][p]
   private void Pivot(int pivotIdx)
   {
      for (var i = pivotIdx + 1; i < _rowsNum; i++)
      {
         var alpha = _matrix[i][pivotIdx] / _matrix[pivotIdx][pivotIdx];
         for (var j = pivotIdx; j <= _colsNum; j++)
         {
            _matrix[i][j] -= alpha * _matrix[pivotIdx][j];
         }
      }
   }

   /// <summary>
   ///    Returns a solution to the linear system of equations <em>Ax</em> = <em>b</em>.
   /// </summary>
   /// <returns>
   ///    a solution <em>x</em> to the linear system of equations <em>Ax</em> = <em>b</em>; {@code null} if no such solution
   /// </returns>
   public double[]? ApplyPrimal()
   {
      // back substitution
      var solution = new double[_colsNum];
      for (var i = Math.Min(_colsNum - 1, _rowsNum - 1); i >= 0; i--)
      {
         var sum = 0.0;
         for (var j = i + 1; j < _colsNum; j++)
         {
            sum += _matrix[i][j] * solution[j];
         }

         if (Math.Abs(_matrix[i][i]) > double.Epsilon)
         {
            solution[i] = (_matrix[i][_colsNum] - sum) / _matrix[i][i];
         }
         else if (Math.Abs(_matrix[i][_colsNum] - sum) > double.Epsilon)
         {
            return null;
         }
      }

      // redundant rows
      for (var i = _colsNum; i < _rowsNum; i++)
      {
         var sum = 0.0;
         for (var j = 0; j < _colsNum; j++)
         {
            sum += _matrix[i][j] * solution[j];
         }

         if (Math.Abs(_matrix[i][_colsNum] - sum) > double.Epsilon)
         {
            return null;
         }
      }

      return solution;
   }

   // check that Ax = b
   private bool CertifySolution(
      IReadOnlyList<double[]> matrix, IReadOnlyList<double> rVector, out string errorMessage)
   {
      if (!IsFeasible)
      {
         errorMessage = string.Empty;
         return true;
      }

      var primal = ApplyPrimal();
      for (var i = 0; i < _rowsNum; i++)
      {
         var sum = 0.0;
         for (var j = 0; j < _colsNum; j++)
         {
            sum += matrix[i][j] * primal![j];
         }

         if (Math.Abs(sum - rVector[i]) > double.Epsilon)
         {
            errorMessage = "not feasible";
            errorMessage += $"b[{i}] = {rVector[i]}, sum = {sum}";
            return false;
         }
      }

      errorMessage = string.Empty;
      return true;
   }
}