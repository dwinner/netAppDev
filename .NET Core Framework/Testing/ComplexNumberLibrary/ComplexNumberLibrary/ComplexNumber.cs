namespace ComplexNumberLibrary
{
   public struct ComplexNumber
   {
      private double _real;
      private double _imaginary;

      public double Real
      {
         get { return _real; }
         set { _real = value; }
      }

      public double Imaginary
      {
         get { return _imaginary; }
         set { _imaginary = value; }
      }

      public ComplexNumber(double real, double imaginary)
      {
         _real = real;
         _imaginary = imaginary;
      }

      public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
      {
         return new ComplexNumber((a.Real * b.Real) - (a.Imaginary * b.Imaginary),
                                  (a.Real * b.Imaginary) + (b.Real * a.Imaginary));
      }
   }
}
