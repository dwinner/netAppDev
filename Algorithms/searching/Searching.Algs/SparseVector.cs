// A sparse vector, implementing using a symbol table.

using System.Text;

namespace Searching.Algs;

/// <summary>
///    The <see cref="SparseVector" /> class represents a <em>d</em>-dimensional mathematical vector.
///    Vectors are mutable: their values can be changed after they are created.
///    It includes methods for addition, subtraction,
///    dot product, scalar product, unit vector, and Euclidean norm.
///    <p>
///       The implementation is a symbol table of indices and values for which the vector
///       coordinates are nonzero. This makes it efficient when most of the vector coordinates
///       are zero.
///    </p>
/// </summary>
public sealed class SparseVector
{
   private readonly Dictionary<int, double> _symbolTable; // the vector, represented by index-value pairs

   /// <summary>
   ///    Initializes a d-dimensional zero vector.
   /// </summary>
   /// <param name="dimension">The dimension of the vector</param>
   public SparseVector(int dimension)
   {
      Dimension = dimension;
      _symbolTable = new Dictionary<int, double>();
   }

   /// <summary>
   ///    Initializes a d-dimensional vector with coordinates
   /// </summary>
   /// <param name="dimension">The dimension of the vector</param>
   /// <param name="symbolTable">Coordinates index -> value</param>
   public SparseVector(int dimension, Dictionary<int, double> symbolTable)
   {
      Dimension = dimension;
      _symbolTable = symbolTable;
   }

   /// <summary>
   ///    The number of nonzero entries in this vector
   /// </summary>
   public int Size => _symbolTable.Count;

   /// <summary>
   ///    The dimension of this vector
   /// </summary>
   public int Dimension { get; }

   /// <summary>
   ///    Sets/Gets the ith coordinate of this vector to the specified value.
   /// </summary>
   /// <param name="index">Index</param>
   /// <param name="value">Value</param>
   /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index" /> is out of range</exception>
   public double this[int index]
   {
      set
      {
         if (index < 0 || index >= Dimension)
         {
            throw new ArgumentOutOfRangeException(nameof(index));
         }

         if (value == 0.0)
         {
            _symbolTable.Remove(index);
         }
         else
         {
            _symbolTable[index] = value;
            //_symbolTable.Add(index, value);
         }
      }
      get
      {
         if (index < 0 || index >= Dimension)
         {
            throw new ArgumentOutOfRangeException(nameof(index));
         }

         return _symbolTable.TryGetValue(index, out var value) ? value : default;
      }
   }

   /// <summary>
   ///    Returns the magnitude of this vector.
   ///    <remarks>This is also known as the L2 norm or the Euclidean norm</remarks>
   /// </summary>
   /// <value>The magnitude of this vector</value>
   public double Magnitude => Math.Sqrt(Dot(this));

   /// <summary>
   ///    Returns the inner product of this vector with the specified vector.
   /// </summary>
   /// <param name="thatVector">The other vector</param>
   /// <returns>The inner product of this vector with the specified vector</returns>
   /// <exception cref="InvalidOperationException">If the lengths of the two vectors are not equal</exception>
   public double Dot(SparseVector thatVector)
   {
      if (Dimension != thatVector.Dimension)
      {
         throw new InvalidOperationException("Vector lengths disagree");
      }

      var sum = 0.0;

      // iterate over the vector with the fewest nonzeros
      sum += _symbolTable.Count <= thatVector._symbolTable.Count
         ? _symbolTable.Keys
            .Where(key => thatVector._symbolTable.ContainsKey(key))
            .Sum(key => this[key] * thatVector[key])
         : thatVector._symbolTable.Keys
            .Where(key => _symbolTable.ContainsKey(key))
            .Sum(key => this[key] * thatVector[key]);

      return sum;
   }

   /// <summary>
   ///    Returns the inner product of this vector with the specified array.
   /// </summary>
   /// <param name="thatArray">The array</param>
   /// <returns>The inner product of this vector with the specified array.</returns>
   public double Dot(double[] thatArray) =>
      _symbolTable.Keys.Sum(key => thatArray[key] * this[key]);

   public static SparseVector operator *(in SparseVector vector, double alpha) => alpha * vector;

   /// <summary>
   ///    Returns the scalar-vector product of this vector with the specified scalar
   /// </summary>
   /// <param name="alpha">The scalar</param>
   /// <param name="vector">Primary sparse vector</param>
   /// <returns>The scalar-vector product of this vector with the specified scalar</returns>
   public static SparseVector operator *(double alpha, in SparseVector vector)
   {
      var scaledVector = new SparseVector(vector.Dimension, vector._symbolTable);
      foreach (var key in scaledVector._symbolTable.Keys)
      {
         scaledVector[key] = alpha * scaledVector[key];
      }

      return scaledVector;
   }

   /// <summary>
   ///    Calculate the sum of vectors.
   /// </summary>
   /// <param name="lhs">1st vector</param>
   /// <param name="rhs">2nd vector</param>
   /// <returns>The sum of vectors</returns>
   /// <exception cref="InvalidOperationException">If the dimensions of the two vectors are not equal</exception>
   public static SparseVector operator +(in SparseVector lhs, in SparseVector rhs)
   {
      if (lhs.Dimension != rhs.Dimension)
      {
         throw new InvalidOperationException("Vector lengths disagree");
      }

      var vector = new SparseVector(lhs.Dimension);
      foreach (var key in lhs._symbolTable.Keys)
      {
         vector[key] = lhs[key];
      }

      foreach (var key in rhs._symbolTable.Keys)
      {
         vector[key] = rhs[key] + vector[key];
      }

      return vector;
   }

   public override string ToString()
   {
      StringBuilder strBuilder = new();
      foreach (var key in _symbolTable.Keys)
      {
         strBuilder.Append($"({key}, {_symbolTable[key]}) ");
      }

      return strBuilder.ToString();
   }
}