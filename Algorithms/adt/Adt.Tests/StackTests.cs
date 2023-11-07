using DataStructures.LinkBased;

namespace Adt.Tests;

[TestFixture]
public class StackTests
{
   [Test]
   public void StackTest()
   {
      var stack = new Stack2<string>();
      stack.Push("1");
      stack.Push("2");
      stack.Push("3");

      Assert.That(stack.Pop(), Is.EqualTo("3"));
      Assert.That(stack.Pop(), Is.EqualTo("2"));
      Assert.That(stack.Pop(), Is.EqualTo("1"));
   }
}