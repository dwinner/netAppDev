using FakeItEasy;
using NUnit.Framework;

namespace Introduction;

public class ClassToBeTestedTests
{
   [TestFixture]
   public class WhenTheClassToBeTestedIsDoingSomething
   {
      [SetUp]
      public void Given()
      {
         var sut = new ClassToBeTested(A.Fake<IDoSomething>());
      }
   }
}