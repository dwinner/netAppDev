namespace ComplexNumberLibrary
{
   public struct ComplexNumber
   {
      public double Real { get; set; }

      public double Imaginary { get; set; }

      public ComplexNumber(double real, double imaginary)
      {
         Real = real;
         Imaginary = imaginary;
      }

      public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b) =>
         new ComplexNumber(a.Real * b.Real - a.Imaginary * b.Imaginary,
            a.Real * b.Imaginary + b.Real * a.Imaginary);
   }
}