using NUnit.Framework;

namespace FatThinAspects.UnitTesting
{
   [TestFixture]
   public class NormalCodeTest
   {
      [Test]
      public void TestReverse()
      {
         var myCode = new MyNormalCode();
         var result = myCode.Reverse("hello");
         Assert.That(result, Is.EqualTo("olleh"));
      }

      [Test]
      public void TestReverseAlt()
      {
         AspectSettings.On = false;

         var myCode = new MyNormalCode();
         var result = myCode.Reverse("hello");

         Assert.That(result, Is.EqualTo("olleh"));
      }
   }
}