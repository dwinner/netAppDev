namespace IteratorTests;

[TestFixture]
public class IteratorTests
{
   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void IteratorTest()
   {
      var node = new Node<char>('a',
         new Node<char>('b',
            new Node<char>('c'),
            new Node<char>('d')),
         new Node<char>('e'));
      Assert.That(new string(node.PreOrder.ToArray()), Is.EqualTo("abcde"));
   }
}