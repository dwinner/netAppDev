using ComplexNumberLibrary;
using NUnit.Framework;

namespace ComplexNumberNUnit
{
   public class ComplexNumberTest
   {
      [OneTimeSetUp]
      public void FixtureSetUp()
      {
         // Установка фикстуры
      }

      [OneTimeTearDown]
      public void FixtureTearDown()
      {
         // Уничтожение фикстуры
      }

      [SetUp]
      public void SetUp()
      {
         // Установка для каждого теста
      }

      [TearDown]
      public void TearDown()
      {
         // Уничтожение для каждого теста
      }

      [TestCase]
      public void MultiplyTest_RealsOnly()
      {
         var a = new ComplexNumber(2, 0);
         var b = new ComplexNumber(3, 0);
         var c = a * b;
         Assert.That(c.Real, Is.EqualTo(6.0));
         Assert.That(c.Imaginary, Is.EqualTo(0.0));
      }

      [TestCase]
      public void MultiplyTest_RealAndImaginary()
      {
         var a = new ComplexNumber(2, 4);
         var b = new ComplexNumber(3, 5);
         var c = a * b;
         Assert.That(c.Real, Is.EqualTo(-14.0));
         Assert.That(c.Imaginary, Is.EqualTo(22.0));
      }
   }
}