using DataStructures.Flat;
using DataStructures.LinkBased;
using StrQueue = DataStructures.LinkBased.Queue1<string>;
using StrStack = DataStructures.LinkBased.Stack1<string>;

namespace Adt.Tests;

[TestFixture]
public class DataStructuresTests
{
   [Test]
   public void ResizingArrayStackTest()
   {
      var arrayStack = new ResizingArrayStack<string>();
      Assert.That(arrayStack.IsEmpty, Is.True);

      arrayStack.Push("first");
      arrayStack.Push("second");
      arrayStack.Push("third");
      arrayStack.Push("fourth");
      arrayStack.Push("fifth");

      var peekedElement = arrayStack.Peek();
      Assert.That(peekedElement, Is.EqualTo("first"));

      const int expectedSize = 5;
      Assert.That(arrayStack.Size, Is.EqualTo(expectedSize));

      var expected = new List<string>(arrayStack.Size);
      expected.AddRange(arrayStack);
      expected.ForEach(item =>
      {
         var actual = arrayStack.Pop();
         Assert.That(actual, Is.EqualTo(item));
      });

      Assert.Throws<InvalidOperationException>(() => arrayStack.Pop());
      Assert.Throws<InvalidOperationException>(() => arrayStack.Peek());
   }

   [Test]
   public void BagTest()
   {
      const int expectedSize = 5;
      var bag = new Bag<string>
      {
         "first",
         "second",
         "third",
         "fourth",
         "fifth"
      };

      Assert.Multiple(() =>
      {
         Assert.That(bag.Size, Is.EqualTo(expectedSize));
         Assert.That(bag.IsEmpty, Is.False);
      });

      var expected = new List<string>(bag.Size);
      expected.AddRange(bag);

      Assert.That(expected, Has.Count.EqualTo(expectedSize));
      Assert.Multiple(() =>
      {
         Assert.That(expected[0], Is.EqualTo("fifth"));
         Assert.That(expected[1], Is.EqualTo("fourth"));
         Assert.That(expected[2], Is.EqualTo("third"));
         Assert.That(expected[3], Is.EqualTo("second"));
         Assert.That(expected[4], Is.EqualTo("first"));
      });
   }

   [Test]
   public void StackTest()
   {
      var stack = new StrStack();

      // Check the stack is empty
      Assert.That(stack.IsEmpty, Is.True);
      Assert.Throws<InvalidOperationException>(() => stack.Peek());

      // Act: pushing elements
      stack.Push("first");
      stack.Push("second");
      stack.Push("third");
      stack.Push("fourth");
      stack.Push("fifth");

      const int expectedSize = 5;
      Assert.Multiple(() =>
      {
         Assert.That(stack.Peek(), Is.EqualTo("fifth"));
         Assert.That(stack.IsEmpty, Is.False);
         Assert.That(stack.Size, Is.EqualTo(expectedSize));
      });

      // Check stack's iterator
      var expectedStack = new List<string>(expectedSize);
      expectedStack.AddRange(stack);
      Assert.Multiple(() =>
      {
         Assert.That(expectedStack[4], Is.EqualTo("first"));
         Assert.That(expectedStack[3], Is.EqualTo("second"));
         Assert.That(expectedStack[2], Is.EqualTo("third"));
         Assert.That(expectedStack[1], Is.EqualTo("fourth"));
         Assert.That(expectedStack[0], Is.EqualTo("fifth"));
      });

      // Check popping
      for (var i = 0; i < expectedStack.Count; i++)
      {
         var stElement = stack.Pop();
         Assert.Multiple(() =>
         {
            Assert.That(stElement, Is.EqualTo(expectedStack[i]));
            Assert.That(stack.Size, Is.EqualTo(expectedStack.Count - (i + 1)));
         });
      }

      Assert.Throws<InvalidOperationException>(() => stack.Pop());
   }

   [Test]
   public void QueueTest()
   {
      var queue = new StrQueue();

      // Check the queue is empty
      Assert.That(queue.IsEmpty, Is.True);
      Assert.Throws<InvalidOperationException>(() => queue.Peek());

      // Act: enqueueing elements
      queue.Enqueue("first");
      queue.Enqueue("second");
      queue.Enqueue("third");
      queue.Enqueue("fourth");
      queue.Enqueue("fifth");

      const int expectedSize = 5;
      Assert.Multiple(() =>
      {
         // Check the queue is not empty
         Assert.That(queue.Peek(), Is.EqualTo("first"));
         Assert.That(queue.IsEmpty, Is.False);
         Assert.That(queue.Size, Is.EqualTo(expectedSize));
      });

      // Check queue's iterator
      var expectedQueue = new List<string?>(expectedSize);
      expectedQueue.AddRange(queue);
      Assert.Multiple(() =>
      {
         Assert.That(expectedQueue[0], Is.EqualTo("first"));
         Assert.That(expectedQueue[1], Is.EqualTo("second"));
         Assert.That(expectedQueue[2], Is.EqualTo("third"));
         Assert.That(expectedQueue[3], Is.EqualTo("fourth"));
         Assert.That(expectedQueue[4], Is.EqualTo("fifth"));
      });

      // Check dequeueing
      for (var i = 0; i < expectedQueue.Count; i++)
      {
         var qElement = queue.Dequeue();
         Assert.Multiple(() =>
         {
            Assert.That(qElement, Is.EqualTo(expectedQueue[i]));
            Assert.That(queue.Size, Is.EqualTo(expectedQueue.Count - (i + 1)));
         });
      }

      Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
   }

   [Test]
   public void VectorTest()
   {
      const double eps = double.Epsilon;

      // Testing constructing
      const int expectedLen = 3;
      var nullVector = new Vector(expectedLen);
      Assert.That(nullVector, Has.Length.EqualTo(expectedLen));
      Array.ForEach(nullVector.Data,
         item => Assert.That(Math.Abs(item - default(double)), Is.LessThan(eps)));

      // The direction is invalid for NULL-vector
      Assert.Throws<ArithmeticException>(() => nullVector.Direction());

      // Invalid indexes
      Assert.Throws<ArgumentOutOfRangeException>(() =>
      {
         var _ = nullVector[-1];
      });
      Assert.Throws<ArgumentOutOfRangeException>(() =>
      {
         var _ = nullVector[nullVector.Length];
      });

      double[] xData = { 1.0, 2.0, 3.0, 4.0 };
      double[] yData = { 5.0, 2.0, 4.0, 1.0 };
      var x = new Vector(xData);
      var y = new Vector(yData);

      // Check data content
      for (var i = 0; i < x.Data.Length; i++)
      {
         Assert.That(Math.Abs(x[i] - (i + 1)), Is.LessThan(eps));
      }

      // The dot product for inconsistant vectors
      Assert.Throws<ArgumentException>(() => nullVector.Dot(x));

      // The dot product for consistant vectors
      const double expectedDot = 22.0;
      Assert.That(Math.Abs(x.Dot(y) - expectedDot), Is.LessThan(eps));

      // The magnitude
      const double expectedMagnitude = 4.47;
      var magnitude = Math.Round(x.Magnitude(), 2);
      Assert.That(Math.Abs(magnitude - expectedMagnitude), Is.LessThan(eps));

      // The distance
      const double yToXExpectedDistance = 2.0;
      Assert.Multiple(() =>
      {
         Assert.That(double.IsNaN(x.DistanceTo(y)), Is.True);
         Assert.That(Math.Abs(y.DistanceTo(x) - yToXExpectedDistance), Is.LessThan(eps));
      });

      // The direction of Y
      const string expectedDirection = "1.0206207261596576 0.4082482904638631 0.8164965809277261 0.20412414523193154";
      var direction = y.Direction().ToString();
      Assert.That(direction, Is.EqualTo(expectedDirection));

      // The difference
      const string expectedDifference = "-4 0 -1 3";
      Assert.That(string.Equals((x - y).ToString(), expectedDifference), Is.True);

      // The sum
      const string expectedSum = "6 4 7 5";
      Assert.That(string.Equals((x + y).ToString(), expectedSum), Is.True);

      // The Mul By 2
      const string expectedMul = "2 4 6 8";
      Assert.That(string.Equals((x * 2).ToString(), expectedMul), Is.True);
   }
}