using DataStructures.Flat;
using MoreLinq;

namespace Adt.Tests;

[TestFixture]
public class ArrayListTests
{
   [Test]
   public void ArrayListTest()
   {
      var arrayList = new ArrayList<string>
      {
         "1",
         "2",
         "3",
         "4",
         "5"
      };

      const int expectedCount = 5;
      Assert.That(arrayList, Has.Count.EqualTo(expectedCount));

      arrayList.ForEach(item => Console.WriteLine(item));
   }
}