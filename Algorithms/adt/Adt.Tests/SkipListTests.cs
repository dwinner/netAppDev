using DataStructures.LinkBased;

namespace Adt.Tests;

[TestFixture]
public class SkipListTests
{
   [Test]
   public void SkipListTest()
   {
      var skipList = new SkipList<string> { "3", "1", "2", "5", "9", "6", "4", "7", "8", "10" };

      Assert.That(skipList, Has.Count.EqualTo(10));
      Assert.That(skipList, Does.Contain("7"));
      Assert.That(skipList, Does.Contain("3"));
      Assert.That(skipList, Does.Contain("4"));
      Assert.That(skipList, Does.Not.Contain("0"));
      Assert.That(skipList, Does.Not.Contain("11"));

      var tenRemoved = skipList.Remove("10");
      Assert.Multiple(() =>
      {
         Assert.That(tenRemoved, Is.True);
         Assert.That(skipList, Has.Count.EqualTo(9));
      });
      Assert.That(skipList, Does.Not.Contain("10"));

      var expected = new SortedSet<string> { "3", "2", "5", "9", "6", "4", "7", "8", "1" };
      var equals = expected.SetEquals(skipList);
      Assert.That(equals, Is.True);

      var skipArray = skipList.ToArray();
      for (var i = 0; i < skipArray.Length; i++)
      {
         var expectedValue = (i + 1).ToString();
         var actualValue = skipArray[i];
         Assert.That(expectedValue, Is.EqualTo(actualValue));
      }
   }
}