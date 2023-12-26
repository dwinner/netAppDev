namespace AssertionSamples;

[TestFixture]
public class CollectionConstraintTests
{
   [Test]
   public void CollectionContains_Test()
   {
      int[] iarray = { 1, 2, 3 };
      string[] sarray = { "a", "b", "c" };
      Assert.That(iarray, Has.Member(3));
      Assert.That(sarray, Has.Member("b"));
      Assert.That(sarray, Contains.Item("c"));
      Assert.That(sarray, Has.No.Member("x"));
      Assert.That(iarray, Does.Contain(3));
   }

   [Test]
   public void CollectionEquivalent_Test()
   {
      int[] iarray = { 1, 2, 3 };
      string[] sarray = { "a", "b", "c" };
      Assert.That(new[] { "c", "a", "b" }, Is.EquivalentTo(sarray));
      Assert.That(new[] { 1, 2, 2 }, Is.Not.EquivalentTo(iarray));
   }

   [Test]
   public void CollectionOrdered_Test()
   {
      int[] iarray = { 1, 2, 3 };
      Assert.That(iarray, Is.Ordered);

      string[] sarray = { "c", "b", "a" };
      Assert.That(sarray, Is.Ordered.Descending);
   }

   [Test]
   public void PropertyBasedOrdering_Test()
   {
      string[] sarray = { "a", "aa", "aaa" };
      Assert.That(sarray, Is.Ordered.By("Length"));

      string[] sarray2 = { "aaa", "aa", "a" };
      Assert.That(sarray2, Is.Ordered.Descending.By("Length"));
   }

   [Test]
   public void CollectionSubset_Test()
   {
      int[] iarray = { 1, 3 };
      Assert.That(iarray, Is.SubsetOf(new[] { 1, 2, 3 }));
   }

   [Test]
   public void CollectionSuperset_Test()
   {
      int[] iarray = { 1, 2, 3 };
      Assert.That(iarray, Is.SupersetOf(new[] { 1, 3 }));
   }

   [Test]
   public void DictionaryContains_Test()
   {
      var idict = new Dictionary<int, int>
      {
         { 1, 4 },
         { 2, 5 }
      };

      Assert.That(idict, Contains.Key(1));
      Assert.That(idict, Does.ContainKey(2));
      Assert.That(idict, Does.Not.ContainKey(3));
      //Assert.That(mydict, Contains.Key(myOwnObject).Using(myComparer));
      //Assert.That(mydict, Does.ContainKey("Hello").WithValue("World"));
      Assert.That(idict, Contains.Value(4));
      Assert.That(idict, Does.ContainValue(5));
      Assert.That(idict, Does.Not.ContainValue(3));
      //Assert.That(mydict, Contains.Value(myOwnObject).Using(myComparer));
   }
}