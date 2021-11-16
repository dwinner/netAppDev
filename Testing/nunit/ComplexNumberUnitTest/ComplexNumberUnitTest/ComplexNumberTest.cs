using System;
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
         ComplexNumber a = new ComplexNumber(2, 0);
         ComplexNumber b = new ComplexNumber(3, 0);
         ComplexNumber c = a*b;
         Assert.AreEqual(6, c.Real);
         Assert.AreEqual(0, c.Imaginary);
      }
   }
}
