namespace FloatingPointTolerance;

[TestFixture]
[DefaultFloatingPointTolerance(1)]
public class ToleranceTest
{
   [Test]
   public void ComparisonUsingDefaultFloatingPointToleranceFromFixture()
   {
      // Passes due to the DefaultFloatingPointToleranceAttribute from the fixture.
      Assert.That(1f, Is.EqualTo(2));
   }

   [Test]
   public void ComparisonOfIntegersDoNotUseTolerance()
   {
      // Fails as DefaultFloatingPointTolerance only effects comparisons
      // of floats and doubles.
      Assert.That(1, Is.EqualTo(2));
   }

   [Test]
   public void ComparisonUsingSpecificTolerance()
   {
      // Fails as 1 is not equal to 2 using the specified tolerance 0.
      Assert.That(1f, Is.EqualTo(2).Within(0));
   }

   [Test]
   [DefaultFloatingPointTolerance(2)]
   public void ComparisonUsingDefaultFloatingPointToleranceFromTest()
   {
      // Passes due to the  DefaultFloatingPointTolerance from the test.
      Assert.That(2f, Is.EqualTo(4));
   }
}