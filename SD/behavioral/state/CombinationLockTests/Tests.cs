namespace CombinationLockTests;

[TestFixture]
public class Tests
{
   [Test]
   public void TestSuccess()
   {
      var cl = new CombinationLock([1, 2, 3, 4, 5]);
      Assert.That(cl.Status, Is.EqualTo("LOCKED"));
      cl.EnterDigit(1);
      Assert.That(cl.Status, Is.EqualTo("1"));
      cl.EnterDigit(2);
      Assert.That(cl.Status, Is.EqualTo("12"));
      cl.EnterDigit(3);
      Assert.That(cl.Status, Is.EqualTo("123"));
      cl.EnterDigit(4);
      Assert.That(cl.Status, Is.EqualTo("1234"));
      cl.EnterDigit(5);
      Assert.That(cl.Status, Is.EqualTo("OPEN"));
   }

   [Test]
   public void TestFailure()
   {
      var cl = new CombinationLock([1, 2, 3]);
      Assert.That(cl.Status, Is.EqualTo("LOCKED"));
      cl.EnterDigit(1);
      Assert.That(cl.Status, Is.EqualTo("1"));
      cl.EnterDigit(2);
      Assert.That(cl.Status, Is.EqualTo("12"));
      cl.EnterDigit(5);
      Assert.That(cl.Status, Is.EqualTo("ERROR"));
   }
}