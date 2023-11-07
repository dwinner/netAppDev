using System.Numerics;

namespace MiscThings;

/// <summary>
///    The <see cref="FastFourierTransform" /> class provides methods for computing the
///    FFT (Fast-Fourier Transform), inverse FFT, linear convolution,
///    and circular convolution of a complex array.
///    <p>
///       It is a bare-bones implementation that runs in <em>n</em> log <em>n</em> time,
///       where <em>n</em> is the length of the complex array. For simplicity,
///       <em>n</em> must be a power of 2.
///       Our goal is to optimize the clarity of the code, rather than performance.
///       It is not the most memory efficient implementation because it uses
///       objects to represent complex numbers and it re-allocates memory
///       for the subarray, instead of doing in-place or reusing a single
///       temporary array.
///    </p>
///    <p>
///       This computes correct results if all arithmetic performed is
///       without floating-point rounding error or arithmetic overflow.
///       In practice, there will be floating-point rounding error.
///    </p>
/// </summary>
public static class FastFourierTransform
{
   private static readonly Complex Zero = Complex.Zero;

   /// <summary>
   ///    Returns the FFT of the specified complex array.
   /// </summary>
   /// <param name="complexNumbers">The complex array</param>
   /// <returns>The FFT of the complex array <paramref name="complexNumbers" /></returns>
   /// <exception cref="ArgumentException">If the length of <paramref name="complexNumbers" /> is not a power of 2</exception>
   public static Complex[] ComputeFft(Complex[] complexNumbers)
   {
      var len = complexNumbers.Length;
      // base case
      if (len == 1)
      {
         return new[] { complexNumbers[0] };
      }

      // radix 2 Cooley-Tukey FFT
      if (len % 2 != 0)
      {
         throw new ArgumentException("n is not a power of 2", nameof(complexNumbers));
      }

      // fft of even terms
      var even = new Complex[len / 2];
      for (var k = 0; k < len / 2; k++)
      {
         even[k] = complexNumbers[2 * k];
      }

      var evenNums = ComputeFft(even);

      // fft of odd terms
      var odd = even; // reuse the array
      for (var k = 0; k < len / 2; k++)
      {
         odd[k] = complexNumbers[2 * k + 1];
      }

      var oddNums = ComputeFft(odd);

      // combine
      var combined = new Complex[len];
      for (var k = 0; k < len / 2; k++)
      {
         var kth = -2 * k * Math.PI / len;
         var wk = new Complex(Math.Cos(kth), Math.Sin(kth));
         combined[k] = evenNums[k] + wk * oddNums[k];
         combined[k + len / 2] = evenNums[k] - wk * oddNums[k];
      }

      return combined;
   }

   /// <summary>
   ///    Returns the inverse FFT of the specified complex array.
   /// </summary>
   /// <param name="x">the complex array</param>
   /// <returns>The inverse FFT of the complex array <paramref name="x" /></returns>
   public static Complex[] ComputeInverseFft(Complex[] x)
   {
      var len = x.Length;
      var y = new Complex[len];

      // take conjugate
      for (var i = 0; i < len; i++)
      {
         y[i] = Complex.Conjugate(x[i]);
      }

      // compute forward FFT
      y = ComputeFft(y);

      // take conjugate again
      for (var i = 0; i < len; i++)
      {
         y[i] = Complex.Conjugate(y[i]);
      }

      // divide by n
      for (var i = 0; i < len; i++)
      {
         y[i] = 1.0 / len * y[i];
      }

      return y;
   }

   /// <summary>
   ///    Returns the circular convolution of the two specified complex arrays.
   /// </summary>
   /// <param name="cpxArray1">One complex array</param>
   /// <param name="cpxArray2">The other complex array</param>
   /// <returns>The circular convolution of <paramref name="cpxArray1" /> and <paramref name="cpxArray2" /></returns>
   /// <exception cref="ArgumentException">
   ///    If the length of <paramref name="cpxArray1" /> does not equal the length of
   ///    <paramref name="cpxArray2" /> or if the length is not a power of 2
   /// </exception>
   public static Complex[] ApplyCircularConvolve(Complex[] cpxArray1, Complex[] cpxArray2)
   {
      // should probably pad x and y with 0s so that they have same length
      // and are powers of 2
      if (cpxArray1.Length != cpxArray2.Length)
      {
         throw new ArgumentException("Dimensions don't agree", nameof(cpxArray1));
      }

      var len1 = cpxArray1.Length;

      // compute FFT of each sequence
      var fft1 = ComputeFft(cpxArray1);
      var fft2 = ComputeFft(cpxArray2);

      // point-wise multiply
      var mult = new Complex[len1];
      for (var i = 0; i < len1; i++)
      {
         mult[i] = fft1[i] * fft2[i];
      }

      // compute inverse FFT
      return ComputeInverseFft(mult);
   }

   /// <summary>
   ///    Returns the linear convolution of the two specified complex arrays.
   /// </summary>
   /// <param name="cpxArray1">One complex array</param>
   /// <param name="cpxArray2">The other complex array</param>
   /// <returns>The linear convolution of <paramref name="cpxArray1" /> and <paramref name="cpxArray2" /></returns>
   public static Complex[] ApplyConvolve(Complex[] cpxArray1, Complex[] cpxArray2)
   {
      var array1 = new Complex[2 * cpxArray1.Length];
      for (var i = 0; i < cpxArray1.Length; i++)
      {
         array1[i] = cpxArray1[i];
      }

      for (var i = cpxArray1.Length; i < 2 * cpxArray1.Length; i++)
      {
         array1[i] = Zero;
      }

      var array2 = new Complex[2 * cpxArray2.Length];
      for (var i = 0; i < cpxArray2.Length; i++)
      {
         array2[i] = cpxArray2[i];
      }

      for (var i = cpxArray2.Length; i < 2 * cpxArray2.Length; i++)
      {
         array2[i] = Zero;
      }

      return ApplyCircularConvolve(array1, array2);
   }
}