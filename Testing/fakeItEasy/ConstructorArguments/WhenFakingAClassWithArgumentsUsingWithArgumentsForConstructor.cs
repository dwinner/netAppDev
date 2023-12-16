namespace ConstructorArguments;

[TestFixture]
public class WhenFakingAClassWithArgumentsUsingWithArgumentsForConstructor
{
   [SetUp]
   public void Given()
   {
      _sut = A.Fake<AClassWithArguments>(fakeOptions =>
         fakeOptions.WithArgumentsForConstructor(() => new AClassWithArguments(A<string>.Ignored)));
      _sut.AFakeableMethod();
   }

   private AClassWithArguments _sut = null!;

   [Test]
   public void ACallToAFakeableMethodMustHaveHappened()
   {
      A.CallTo(() => _sut.AFakeableMethod()).MustHaveHappened();
   }
}