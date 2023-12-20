namespace AssertionSamples;

public class ConstraintTests
{
   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void AllItems_Test()
   {
      int[] iarray = { 1, 2, 3 };
      string[] sarray = { "a", "b", "c" };
      Assert.That(iarray, Is.All.Not.Null);
      Assert.That(sarray, Is.All.InstanceOf<string>());
      Assert.That(iarray, Is.All.GreaterThan(0));
      Assert.That(iarray, Has.All.GreaterThan(0));
   }

   [Test]
   public void And_Constraint_Test()
   {
      var actual = 2.3;
      Assert.That(actual, Is.GreaterThan(2.0).And.LessThan(3.0));
   }

   [Test]
   public void AnyOf_Test()
   {
      var actual = 42;
      Assert.That(actual, Is.AnyOf(0, -1, 42, 100));
   }

   [Test]
   public void AssignableFrom_Test()
   {
      var hello = "Hello";
      var five = 5;
      Assert.That(hello, Is.AssignableFrom(typeof(string)));
      Assert.That(five, Is.Not.AssignableFrom(typeof(string)));
   }

   [Test]
   public void AssignableTo_Test()
   {
      var hello = "Hello";
      var five = 5;
      Assert.That(hello, Is.AssignableTo(typeof(object)));
      Assert.That(five, Is.Not.AssignableTo(typeof(string)));
   }
}