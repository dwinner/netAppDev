using DataStructures.Flat;

namespace Adt.Tests;

[TestFixture]
public class DequeTests
{
   [Test]
   public void CircularDequeTest()
   {
      IDeque<string> deque = new CircularArrayDequeImpl<string>();

      Assert.That(deque, Is.Empty);

      deque.EnqueueFirst("3");

      Assert.That(deque, Has.Count.EqualTo(1));

      deque.EnqueueLast("4");
      deque.EnqueueLast("5");
      deque.EnqueueFirst("2");
      deque.EnqueueFirst("1");

      Assert.That(deque.Count, Is.EqualTo(5));

      var firstPeeked = deque.PeekFirst();
      Assert.That(firstPeeked, Is.EqualTo("1"));

      var lastPeeked = deque.PeekLast();

      Assert.Multiple(() =>
      {
         Assert.That(lastPeeked, Is.EqualTo("5"));
         Assert.That(deque.DequeueLast(), Is.EqualTo("5"));
         Assert.That(deque.Count, Is.EqualTo(4));
      });

      Assert.Multiple(() =>
      {
         Assert.That(deque.DequeueFirst(), Is.EqualTo("1"));
         Assert.That(deque.Count, Is.EqualTo(3));
      });
   }

   [Test]
   public void HarmonicDequeHeadGrowTest()
   {
      IDeque<string> deque = new HarmonicArrayDequeImpl<string>();
      deque.EnqueueFirst("1");
      deque.EnqueueFirst("2");
      deque.EnqueueFirst("3");
      deque.EnqueueFirst("4");
      deque.EnqueueFirst("5");
      deque.EnqueueFirst("6");
      deque.EnqueueFirst("7");
      deque.EnqueueFirst("8");

      Assert.That(deque, Has.Count.EqualTo(8));
      var expectedSet = new HashSet<string> { "1", "2", "3", "4", "5", "6", "7", "8" };
      var actualSet = new HashSet<string>(deque);
      var setEquals = expectedSet.SetEquals(actualSet);
      Assert.That(setEquals, Is.True);

      var current = deque.DequeueFirst();
      Assert.That(current, Is.EqualTo("8"));

      current = deque.DequeueFirst();
      Assert.That(current, Is.EqualTo("7"));

      current = deque.DequeueFirst();
      Assert.That(current, Is.EqualTo("6"));

      current = deque.DequeueFirst();
      Assert.That(current, Is.EqualTo("5"));

      current = deque.DequeueFirst();
      Assert.That(current, Is.EqualTo("4"));

      current = deque.DequeueFirst();
      Assert.That(current, Is.EqualTo("3"));

      current = deque.DequeueFirst();
      Assert.That(current, Is.EqualTo("2"));

      current = deque.DequeueFirst();
      Assert.Multiple(() =>
      {
         Assert.That(current, Is.EqualTo("1"));
         Assert.That(deque.Count, Is.EqualTo(0));
      });

      Assert.Throws<InvalidOperationException>(() => deque.DequeueFirst());
   }

   [Test]
   public void HarmonicDequeTailGrowTest()
   {
      IDeque<string> deque = new HarmonicArrayDequeImpl<string>(8);
      deque.EnqueueLast("1");
      deque.EnqueueLast("2");
      deque.EnqueueLast("3");
      deque.EnqueueLast("4");
      deque.EnqueueLast("5");
      deque.EnqueueLast("6");
      deque.EnqueueLast("7");
      deque.EnqueueLast("8");

      Assert.That(deque.Count, Is.EqualTo(8));

      var current = deque.DequeueLast();
      Assert.That(current, Is.EqualTo("8"));

      current = deque.DequeueLast();
      Assert.That(current, Is.EqualTo("7"));

      current = deque.DequeueLast();
      Assert.That(current, Is.EqualTo("6"));

      current = deque.DequeueLast();
      Assert.That(current, Is.EqualTo("5"));

      current = deque.DequeueLast();
      Assert.That(current, Is.EqualTo("4"));

      current = deque.DequeueLast();
      Assert.That(current, Is.EqualTo("3"));

      current = deque.DequeueLast();
      Assert.That(current, Is.EqualTo("2"));

      current = deque.DequeueLast();
      Assert.Multiple(() =>
      {
         Assert.That(current, Is.EqualTo("1"));
         Assert.That(deque.Count, Is.EqualTo(0));
      });

      Assert.Throws<InvalidOperationException>(() => deque.DequeueLast());
   }

   [Test]
   public void HarmonicDequeDualGrowTest()
   {
      IDeque<string> deque = new HarmonicArrayDequeImpl<string>();
      deque.EnqueueFirst("1");
      deque.EnqueueLast("2");
      deque.EnqueueFirst("3");
      deque.EnqueueLast("4");
      deque.EnqueueFirst("5");
      deque.EnqueueLast("6");
      deque.EnqueueFirst("7");
      deque.EnqueueLast("8");

      Assert.That(deque, Has.Count.EqualTo(8));

      var item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("7"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("8"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("5"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("6"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("3"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("4"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("1"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("2"));

      Assert.Throws<InvalidOperationException>(() => deque.DequeueFirst());
      Assert.Throws<InvalidOperationException>(() => deque.DequeueLast());
   }

   [Test]
   public void HarmonicDequeFromHeadDecreaseLeftTest()
   {
      IDeque<string> deque = new HarmonicArrayDequeImpl<string>();
      deque.EnqueueFirst("1");
      deque.EnqueueFirst("2");
      deque.EnqueueFirst("3");
      deque.EnqueueFirst("4");
      deque.EnqueueFirst("5");
      deque.EnqueueFirst("6");
      deque.EnqueueFirst("7");
      deque.EnqueueFirst("8");

      var item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("8"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("7"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("6"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("5"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("4"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("3"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("2"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("1"));
   }

   [Test]
   public void HarmonicDequeFromTailDecreaseLeftTest()
   {
      IDeque<string> deque = new HarmonicArrayDequeImpl<string>();
      deque.EnqueueLast("1");
      deque.EnqueueLast("2");
      deque.EnqueueLast("3");
      deque.EnqueueLast("4");
      deque.EnqueueLast("5");
      deque.EnqueueLast("6");
      deque.EnqueueLast("7");
      deque.EnqueueLast("8");

      var item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("1"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("2"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("3"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("4"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("5"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("6"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("7"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("8"));
   }

   [Test]
   public void HarmonicDequeFromDualDecreaseLeftTest()
   {
      IDeque<string> deque = new HarmonicArrayDequeImpl<string>();
      deque.EnqueueFirst("1");
      deque.EnqueueLast("2");
      deque.EnqueueFirst("3");
      deque.EnqueueLast("4");
      deque.EnqueueFirst("5");
      deque.EnqueueLast("6");
      deque.EnqueueFirst("7");
      deque.EnqueueLast("8");

      var item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("7"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("5"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("3"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("1"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("2"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("4"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("6"));

      item = deque.DequeueFirst();
      Assert.That(item, Is.EqualTo("8"));

      Assert.That(deque, Is.Empty);
      Assert.Throws<InvalidOperationException>(() => deque.DequeueLast());
      Assert.Throws<InvalidOperationException>(() => deque.DequeueFirst());
   }

   [Test]
   public void HarmonicDequeFromHeadDecreaseRightTest()
   {
      IDeque<string> deque = new HarmonicArrayDequeImpl<string>();
      deque.EnqueueFirst("1");
      deque.EnqueueFirst("2");
      deque.EnqueueFirst("3");
      deque.EnqueueFirst("4");

      var item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("1"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("2"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("3"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("4"));
   }

   [Test]
   public void HarmonicDequeFromTailDecreaseRightTest()
   {
      IDeque<string> deque = new HarmonicArrayDequeImpl<string>();
      deque.EnqueueLast("1");
      deque.EnqueueLast("2");
      deque.EnqueueLast("3");
      deque.EnqueueLast("4");

      var item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("4"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("3"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("2"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("1"));
   }

   [Test]
   public void HarmonicDequeFromDualDecreaseRightTest()
   {
      IDeque<string> deque = new HarmonicArrayDequeImpl<string>();
      deque.EnqueueFirst("1");
      deque.EnqueueLast("2");
      deque.EnqueueFirst("3");
      deque.EnqueueLast("4");
      deque.EnqueueFirst("5");
      deque.EnqueueLast("6");
      deque.EnqueueFirst("7");
      deque.EnqueueLast("8");

      var item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("8"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("6"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("4"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("2"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("1"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("3"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("5"));

      item = deque.DequeueLast();
      Assert.That(item, Is.EqualTo("7"));

      Assert.That(deque, Is.Empty);
      Assert.Throws<InvalidOperationException>(() => deque.DequeueLast());
      Assert.Throws<InvalidOperationException>(() => deque.DequeueFirst());
   }

   [Test]
   public void HarmonicDequePeekFirstTest()
   {
      IDeque<string> deque = new HarmonicArrayDequeImpl<string>();
      deque.EnqueueFirst("1");
      deque.EnqueueFirst("2");
      deque.EnqueueLast("3");
      deque.EnqueueLast("4");

      Assert.That(deque, Has.Count.EqualTo(4));
      var peekedItem = deque.PeekFirst();
      Assert.That(peekedItem, Is.EqualTo("2"));
      Assert.That(deque, Has.Count.EqualTo(4));
   }

   [Test]
   public void HarmonicDequePeekLastTest()
   {
      IDeque<string> deque = new HarmonicArrayDequeImpl<string>();
      deque.EnqueueFirst("1");
      deque.EnqueueFirst("2");
      deque.EnqueueLast("3");
      deque.EnqueueLast("4");

      Assert.That(deque, Has.Count.EqualTo(4));
      var peekedItem = deque.PeekLast();
      Assert.That(peekedItem, Is.EqualTo("4"));
      Assert.That(deque, Has.Count.EqualTo(4));
   }
}