using System.Collections;

namespace ParameterizedSamples;

[TestFixture(typeof(ArrayList))]
[TestFixture(typeof(List<int>))]
public class GenericListTests<TList>
   where TList : IList, new()
{
   [SetUp]
   public void CreateList()
   {
      _list = new TList();
   }

   private IList _list = null!;

   [Test]
   public void CanAddToList()
   {
      _list.Add(1);
      _list.Add(2);
      _list.Add(3);
      Assert.That(_list, Has.Count.EqualTo(3));
   }
}