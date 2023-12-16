using FakeItEasy;

namespace AClassWithAnUnfakeableMethod;

[TestFixture]
public class WhenTryingToFakeAndUnfakeableMethod
{
   [SetUp]
   public void Given()
   {
      _sut = A.Fake<AClassWithAnUnfakeableMethod>();
      _sut.YouCantFakeMe();
   }

   private AClassWithAnUnfakeableMethod _sut = null!;

   [Test]
   public void YouWillGetAnException()
   {
      //A.CallTo(() => _sut.YouCantFakeMe()).MustHaveHappened();
   }
}