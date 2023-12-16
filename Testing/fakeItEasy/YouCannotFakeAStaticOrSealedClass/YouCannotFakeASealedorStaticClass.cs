using FakeItEasy;
using A = FakeItEasy.A;

namespace YouCannotFakeAStaticOrSealedClass;
//[TestFixture]
//public class WhenTryingToFakeAStaticClass
//{
//    [SetUp]
//    public void Given()
//    {
//        var sut = A.Fake<YouCannotFakeAStaticClass>();
//    }
//}

public static class YouCannotFakeAStaticClass;

public sealed class YouCannotFakeASealedClass
{
   public void DoSomething()
   {
      //some implementation
   }
}

[TestFixture]
public class WhenTryingToFakeASealedClass
{
   [SetUp]
   public void Given()
   {
      sut = A.Fake<YouCannotFakeASealedClass>();
      sut.DoSomething();
   }

   private YouCannotFakeASealedClass sut;

   [Test]
   public void YouWillGetAnException()
   {
      A.CallTo(() => sut.DoSomething()).MustHaveHappened(1, Times.Exactly);
   }
}