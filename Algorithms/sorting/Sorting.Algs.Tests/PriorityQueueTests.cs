using MoreLinq;
using Sorting.Algs.PrioQ;

namespace Sorting.Algs.Tests;

[TestFixture]
public class PriorityQueueTests
{
   [Test]
   public void IndexMaxPqTest()
   {
      string[] strings =
      {
         "it",
         "was",
         "the",
         "best",
         "of",
         "times",
         "it",
         "was",
         "the",
         "worst"
      };

      var prioQueue = new IndexMaxPrioQueue<string>(strings.Length);
      for (var i = 0; i < strings.Length; i++)
      {
         prioQueue.Insert(i, strings[i]);
      }

      foreach (var qIndex in prioQueue)
      {
         Console.WriteLine($"{qIndex} {strings[qIndex]}");
      }

      Console.WriteLine();

      // increase or decrease the key
      for (var i = 0; i < strings.Length; i++)
      {
         if (i % 2 == 0)
         {
            prioQueue.IncreaseKey(i, string.Format("{0}{0}", strings[i]));
         }
         else
         {
            prioQueue.DecreaseKey(i, strings[i].Substring(0, 1));
         }
      }

      // delete and print each key
      while (!prioQueue.IsEmpty)
      {
         var key = prioQueue.MaxKey;
         var deletedIdx = prioQueue.DelMax();
         Console.WriteLine($"{deletedIdx} {key}");
      }

      Console.WriteLine();

      // reinsert the same strings
      for (var i = 0; i < strings.Length; i++)
      {
         prioQueue.Insert(i, strings[i]);
      }

      // delete them in random order
      var perm = new int[strings.Length];
      for (var i = 0; i < strings.Length; i++)
      {
         perm[i] = i;
      }

      perm = perm.Shuffle().ToArray();
      foreach (var idx in perm)
      {
         var key = prioQueue.GetKeyOf(idx);
         prioQueue.Delete(idx);
         Console.WriteLine($"{idx} {key}");
      }
   }

   [Test]
   public void IndexMinPqTest()
   {
      string[] strings =
      {
         "it",
         "was",
         "the",
         "best",
         "of",
         "times",
         "it",
         "was",
         "the",
         "worst"
      };

      var prioQueue = new IndexMinPrioQueue<string>(strings.Length);
      for (var i = 0; i < strings.Length; i++)
      {
         prioQueue.Insert(i, strings[i]);
      }

      // delete and print each key
      while (!prioQueue.IsEmpty)
      {
         var idx = prioQueue.DelMin();
         Console.WriteLine($"{idx} {strings[idx]}");
      }

      Console.WriteLine();

      // reinsert the same strings
      for (var i = 0; i < strings.Length; i++)
      {
         prioQueue.Insert(i, strings[i]);
      }

      // print each key using the iterator
      foreach (var qIdx in prioQueue)
      {
         Console.WriteLine($"{qIdx} {strings[qIdx]}");
      }

      while (!prioQueue.IsEmpty)
      {
         prioQueue.DelMin();
      }
   }

   [Test]
   public void MaxPqTest()
   {
      var prioQueue = new MaxPrioQueue<string>();
      prioQueue.Insert("one");
      prioQueue.Insert("two");
      prioQueue.Insert("three");
      prioQueue.Insert("four");
      prioQueue.Insert("five");
      prioQueue.Insert("six");
      prioQueue.Insert("seven");
      prioQueue.Insert("eight");
      prioQueue.Insert("nine");
      prioQueue.Insert("ten");
      prioQueue.Insert("eleven");
      prioQueue.Insert("tvelve");

      Assert.That(prioQueue.IsMaxHeap(), Is.True);

      prioQueue.ForEach(qItem => Console.WriteLine(qItem));
      while (!prioQueue.IsEmpty)
      {
         var item = prioQueue.DelMax();
         Console.WriteLine("Deleted item is: {0}", item);
      }
   }

   [Test]
   public void MinPqTest()
   {
      var prioQueue = new MinPrioQueue<string>();
      prioQueue.Insert("first");
      prioQueue.Insert("second");
      prioQueue.Insert("third");
      prioQueue.Insert("fourth");
      prioQueue.Insert("fifth");
      prioQueue.Insert("aaaaaa");

      Assert.That(prioQueue.IsMinHeap(), Is.True);

      prioQueue.ForEach(qItem => Console.WriteLine(qItem));
      while (!prioQueue.IsEmpty)
      {
         var item = prioQueue.DelMin();
         Console.WriteLine("Deleted item is: {0}", item);
      }
   }

   [Test]
   public void TransactionTest()
   {
      var prioQueue = new MinPrioQueue<Transaction>(1, Transaction.DefaultComparer);
      prioQueue.Insert(new Transaction("Thompson", DateTime.Parse("2/27/2000"), 4747.08));
      prioQueue.Insert(new Transaction("vonNeumann", DateTime.Parse("2/12/1994"), 4732.35));
      prioQueue.Insert(new Transaction("vonNeumann", DateTime.Parse("1/11/1999"), 4409.74));
      prioQueue.Insert(new Transaction("Hoare", DateTime.Parse("8/18/1992"), 4381.21));
      prioQueue.Insert(new Transaction("vonNeumann", DateTime.Parse("3/26/2002"), 4121.85));

      Assert.That(prioQueue.IsMinHeap(), Is.True);

      prioQueue.ForEach(Console.WriteLine);
      while (!prioQueue.IsEmpty)
      {
         var transaction = prioQueue.DelMin();
         Console.WriteLine($"Enqueued: {transaction}");
      }
   }
}