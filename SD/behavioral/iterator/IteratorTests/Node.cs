namespace IteratorTests;

public class Node<T>
   where T : notnull
{
   public Node(T value) => Value = value;

   public Node(T value, Node<T> left, Node<T> right)
   {
      Value = value;
      Left = left;
      Right = right;

      left.Parent = right.Parent = this;
   }

   public Node<T>? Left { get; }

   public Node<T>? Right { get; }

   public Node<T>? Parent { get; set; }

   public T Value { get; }

   public IEnumerable<T> PreOrder => Traverse(this).Select(node => node.Value);

   private static IEnumerable<Node<T>> Traverse(Node<T> current)
   {
      yield return current;
      if (current.Left != null)
      {
         foreach (var left in Traverse(current.Left))
         {
            yield return left;
         }
      }

      if (current.Right != null)
      {
         foreach (var right in Traverse(current.Right))
         {
            yield return right;
         }
      }
   }
}