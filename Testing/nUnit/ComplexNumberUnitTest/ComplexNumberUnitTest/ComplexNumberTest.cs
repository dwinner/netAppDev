using ComplexNumberLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComplexNumberUnitTest
{
   [TestClass]
   public class ComplexNumberTest
   {
      [TestMethod]
      public void MultiplyMethodTest()
      {
         var a = new ComplexNumber(2, 0);
         var b = new ComplexNumber(3, 0);
         var c = a * b;
         Assert.AreEqual(6, c.Real);
         Assert.AreEqual(0, c.Imaginary);
      }
   }
}
