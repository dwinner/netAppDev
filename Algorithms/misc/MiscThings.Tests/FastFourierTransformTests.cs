using System.Numerics;

namespace MiscThings.Tests;

[TestFixture]
public class FastFourierTransformTests
{
   private const int Len = 0x10;

   [Test]
   public void ComputeFftTest()
   {
      var x = new Complex[Len];
      var random = new Random(Environment.TickCount);

      // original data
      for (var i = 0; i < Len; i++)
      {
         var real = random.NextDouble();
         var rndInt = random.Next();
         if (rndInt % 2 == 0)
         {
            real = -real;
         }

         x[i] = new Complex(real, 0);
      }

      Show(x, "Original");

      // FFT of original data
      var y = FastFourierTransform.ComputeFft(x);
      Show(y, "y = fft(x)");

      // take inverse FFT
      var z = FastFourierTransform.ComputeInverseFft(y);
      Show(z, "z = ifft(y)");

      // circular convolution of x with itself
      var c = FastFourierTransform.ApplyCircularConvolve(x, x);
      Show(c, "c = cconvolve(x, x)");

      // linear convolution of x with itself
      var d = FastFourierTransform.ApplyConvolve(x, x);
      Show(d, "d = convolve(x, x)");
   }

   private static void Show(Complex[] complexArray, string title)
   {
      Console.WriteLine(title);
      Console.WriteLine("-------------------------------------");
      Array.ForEach(complexArray, cpx => Console.WriteLine(cpx));
      Console.WriteLine();
   }
}