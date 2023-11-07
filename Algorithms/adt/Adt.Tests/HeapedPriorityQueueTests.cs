using DataStructures.HeapBased;

namespace Adt.Tests;

[TestFixture]
public class HeapedPriorityQueueTests
{
   [SetUp]
   public void Setup()
   {
      _priorityQueue = new PriorityQueue<string>();
      _priorityQueue.Enqueue("1");
      _priorityQueue.Enqueue("2");
      _priorityQueue.Enqueue("3");
      _priorityQueue.Enqueue("4");
      _priorityQueue.Enqueue("5");
   }

   private PriorityQueue<string> _priorityQueue = null!;

   [Test]
   public void EnqueueTest()
   {
      _priorityQueue.Enqueue("6");
      Assert.That(_priorityQueue.Count, Is.EqualTo(6));

      _priorityQueue.Clear();
      Assert.That(_priorityQueue.Count, Is.EqualTo(0));
   }

   [Test]
   public void DequeueTest()
   {
      var initCount = _priorityQueue.Count;
      while (_priorityQueue.Count > 0)
      {
         var deqValue = _priorityQueue.Dequeue();
         Assert.That(initCount.ToString(), Is.EqualTo(deqValue));
         initCount--;
      }

      Assert.Throws<InvalidOperationException>(() => _priorityQueue.Dequeue());
   }
}