using System;
using ComplexNumberLibrary;
using NUnit.Framework;

namespace ComplexNumberNUnit
{
   public class ComplexNumberTest
   {
      [OneTimeSetUp]
      public void FixtureSetUp()
      {
         Console.WriteLine(nameof(FixtureSetUp));
      }

      [OneTimeTearDown]
      public void FixtureTearDown()
      {
         Console.WriteLine(nameof(FixtureTearDown));
      }

      [SetUp]
      public void SetUp()
      {
         Console.WriteLine(nameof(SetUp));
      }

      [TearDown]
      public void TearDown()
      {
         Console.WriteLine(nameof(TearDown));
      }

      [Test]
      public void MultiplyTest_RealsOnly()
      {
         var a = new ComplexNumber(2, 0);
         var b = new ComplexNumber(3, 0);
         var c = a * b;
         Assert.That(c.Real, Is.EqualTo(6.0));
         Assert.That(c.Imaginary, Is.EqualTo(0.0));
      }

      [Test]
      public void MultiplyTest_RealAndImaginary()
      {
         var a = new ComplexNumber(2, 4);
         var b = new ComplexNumber(3, 5);
         var c = a * b;
         Assert.That(c.Real, Is.EqualTo(-14.0));
         Assert.That(c.Imaginary, Is.EqualTo(22.0));
      }

      [Test(ExpectedResult = 4)]
      public int TestAdd()
      {
         return 2 + 2;
      }
   }
}