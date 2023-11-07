namespace DataStructures;

/// <summary>
///    The <tt>Vector</tt> class represents a <em>d</em>-dimensional mathematical vector.
///    Vectors are immutable: their values cannot be changed after they are created.
///    The class <code>Vectors</code> includes methods for addition, subtraction,
///    dot product, scalar product, unit vector, Euclidean distance, and Euclidean norm.
/// </summary>
public readonly struct Vector : IFormattable
{
   private const string DimensionCheckMsg = "Dimensions don't agree: {0}";

   /// <summary>
   ///    Initializes a d-dimensional zero vector
   /// </summary>
   /// <param name="aLen">The dimension of the vector</param>
   public Vector(int aLen)
   {
      Length = aLen;
      Data = new double[aLen];
      for (var i = 0; i < Length; i++)
      {
         Data[i] = default;
      }
   }

   /// <summary>
   ///    Initializes a vector from either an array or a vararg list
   ///    The vararg syntax supports a constructor that takes a variable number of arugments
   /// </summary>
   /// <param name="data">The array or vararg list</param>
   public Vector(params double[] data)
   {
      Length = data.Length;

      // Defensive copy so that client can't alter our copy of data[]
      Data = new double[Length];
      Array.Copy(data, Data, Length);
   }

   /// <summary>
   ///    Length of the vector
   /// </summary>
   public int Length { get; }

   /// <summary>
   ///    Array of vector's components
   /// </summary>
   public double[] Data { get; }

   /// <summary>
   ///    Returns the inner product of this vector with that vector
   /// </summary>
   /// <param name="that">The other vector</param>
   /// <exception cref="ArgumentException">If the lengths of the two vectors are not equal</exception>
   /// <returns>The dot product between this vector and that vector</returns>
   public double Dot(in Vector that)
   {
      if (Length != that.Length)
      {
         throw new ArgumentException(string.Format(DimensionCheckMsg, that.Length));
      }

      double sum = 0;
      for (var i = 0; i < Length; i++)
      {
         sum += Data[i] + that.Data[i];
      }

      return sum;
   }

   /// <summary>
   ///    Returns the Euclidean norm of this vector
   /// </summary>
   /// <returns>the Euclidean norm of this vector</returns>
   public double Magnitude() => Math.Sqrt(Dot(this));

   /// <summary>
   ///    Returns the Euclidean distance between this vector and that vector
   /// </summary>
   /// <param name="that">The other vector</param>
   /// <returns>the Euclidean distance between this vector and that vector</returns>
   /// <exception cref="ArgumentException">If the lengths of the two vectors are not equal</exception>
   public double DistanceTo(in Vector that)
   {
      if (Length != that.Length)
      {
         throw new ArgumentException(string.Format(DimensionCheckMsg, that.Length));
      }

      return (this - that).Magnitude();
   }

   /// <summary>
   ///    Returns a unit vector in the direction of this vector
   /// </summary>
   /// <returns>a unit vector in the direction of this vector</returns>
   /// <exception cref="ArithmeticException">If this vector is the zero vector</exception>
   public Vector Direction()
   {
      var magnitude = Magnitude();
      if (magnitude == 0)
      {
         throw new ArithmeticException("Zero-vector has no direction");
      }

      return this * (1.0 / magnitude);
   }

   /// <summary>
   ///    Returns the difference between this vector and that vector: this - that
   /// </summary>
   /// <param name="lhs">this vector</param>
   /// <param name="rhs">that vector</param>
   /// <returns>The difference between this vector and that vector</returns>
   /// <exception cref="ArgumentException">If the lengths of the two vectors are not equal</exception>
   public static Vector operator -(in Vector lhs, in Vector rhs)
   {
      if (lhs.Length != rhs.Length)
      {
         throw new ArgumentException(string.Format(DimensionCheckMsg, rhs.Length));
      }

      var resultVector = new Vector(lhs.Length);
      for (var i = 0; i < lhs.Length; i++)
      {
         resultVector.Data[i] = lhs.Data[i] - rhs.Data[i];
      }

      return resultVector;
   }

   /// <summary>
   ///    Returns the sum of this vector and that vector: this + that
   /// </summary>
   /// <param name="lhs">this vector</param>
   /// <param name="rhs">that vector</param>
   /// <returns>The sum of this vector and that vector</returns>
   /// <exception cref="ArgumentException">If the lengths of the two vectors are not equal</exception>
   public static Vector operator +(in Vector lhs, in Vector rhs)
   {
      if (lhs.Length != rhs.Length)
      {
         throw new ArgumentException(string.Format(DimensionCheckMsg, rhs.Length));
      }

      var resultVector = new Vector(lhs.Length);
      for (var i = 0; i < lhs.Length; i++)
      {
         resultVector.Data[i] = lhs.Data[i] + rhs.Data[i];
      }

      return resultVector;
   }

   /// <summary>
   ///    Returns the ith cartesian coordinate
   /// </summary>
   /// <param name="index">The coordinate index</param>
   /// <returns>the ith cartesian coordinate</returns>
   /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index" /> is out of range</exception>
   public double this[int index]
   {
      get
      {
         if (index < 0 || index >= Length)
         {
            throw new ArgumentOutOfRangeException($"{nameof(index)} is out of range: {index}");
         }

         return Data[index];
      }
   }

   /// <summary>
   ///    Returns the product of this factor multiplied by the scalar factor: this * factor
   /// </summary>
   /// <param name="multiplier">The multiplier</param>
   /// <param name="rhs">Input vector</param>
   /// <returns>The scalar product of this vector and factor</returns>
   public static Vector operator *(double multiplier, in Vector rhs)
   {
      var result = new Vector(rhs.Length);
      for (var i = 0; i < result.Length; i++)
      {
         result.Data[i] = multiplier * rhs[i];
      }

      return result;
   }

   public static Vector operator *(in Vector lhs, double multiplier) => multiplier * lhs;

   public override string ToString() =>
      Data.Aggregate(string.Empty, (current, item) => $"{current}{item} ").TrimEnd();

   public string ToString(string? format, IFormatProvider? formatProvider) => ToString();
}