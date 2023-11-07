namespace Adt.Tests;

[TestFixture]
public class QueueTests
{
   [Test]
   public void QueueTest()
   {
      var queue = new DataStructures.LinkBased.Queue2<string>();
      queue.Enqueue("1");
      queue.Enqueue("2");
      queue.Enqueue("3");

      Assert.That(queue.Dequeue(), Is.EqualTo("1"));
      Assert.That(queue.Dequeue(), Is.EqualTo("2"));
      Assert.That(queue.Dequeue(), Is.EqualTo("3"));
   }
}