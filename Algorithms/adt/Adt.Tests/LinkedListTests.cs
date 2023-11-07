using MoreLinq;

namespace Adt.Tests;

public class LinkedListTests
{
   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void LinkedListTest()
   {
      var linkedList = new DataStructures.LinkBased.LinkedList<string>();
      linkedList.AddLast("4");
      linkedList.AddLast("5");
      linkedList.AddFirst("3");
      linkedList.AddFirst("2");
      linkedList.AddFirst("1");

      Assert.That(linkedList, Has.Count.EqualTo(5));

      var last = linkedList.RemoveLast();
      Assert.Multiple(() =>
      {
         Assert.That(last, Is.EqualTo("5"));
         Assert.That(linkedList, Has.Count.EqualTo(4));
      });

      var first = linkedList.RemoveFirst();
      Assert.Multiple(() =>
      {
         Assert.That(first, Is.EqualTo("1"));
         Assert.That(linkedList, Has.Count.EqualTo(3));
      });

      var removed = linkedList.Remove("2");
      Assert.Multiple(() =>
      {
         Assert.That(removed, Is.True);
         Assert.That(linkedList, Has.Count.EqualTo(2));
      });

      removed = linkedList.Remove("7");
      Assert.Multiple(() =>
      {
         Assert.That(removed, Is.False);
         Assert.That(linkedList, Has.Count.EqualTo(2));
      });

      linkedList.ForEach(item => Console.WriteLine(item));
   }
}