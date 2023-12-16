namespace AClassWithAFakeableMethod;

[TestFixture]
public class WhenTryingToFakeAFakeableMethod
{
   [SetUp]
   public void Given()
   {
      _sut = A.Fake<AClassWithAFakeableMethod>();
      _sut.YouCanFakeMe();
   }

   private AClassWithAFakeableMethod _sut = null!;

   [Test]
   public void YouCanFakeThisMethod()
   {
      A.CallTo(() => _sut.YouCanFakeMe()).MustHaveHappened();
   }
}