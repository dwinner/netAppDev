using System;
using ComplexNumberLibrary;
using NUnit.Framework;

namespace ComplexNumberNUnit
{
    public class ComplexNumberTest
    {
       [TestFixtureSetUp]
       public void FixtureSetUp()
       {
          // Установка фикстуры
       }

       [TestFixtureTearDown]
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
          ComplexNumber a = new ComplexNumber(2, 0);
          ComplexNumber b = new ComplexNumber(3, 0);
          ComplexNumber c = a*b;
          Assert.That(c.Real, Is.EqualTo(6.0));
          Assert.That(c.Imaginary, Is.EqualTo(0.0));
       }

       [TestCase]
       public void MultiplyTest_RealAndImaginary()
       {
          ComplexNumber a = new ComplexNumber(2, 4);
          ComplexNumber b = new ComplexNumber(3, 5);
          ComplexNumber c = a*b;
          Assert.That(c.Real, Is.EqualTo(-14.0));
          Assert.That(c.Imaginary, Is.EqualTo(22.0));
       }
    }
}
