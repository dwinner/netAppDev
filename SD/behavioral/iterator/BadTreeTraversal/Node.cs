namespace BadTreeTraversal;

public class Node<T> where T : notnull
{
   public Node(T value) => Value = value;

   public Node(T value, Node<T> left, Node<T> right)
   {
      Value = value;
      Left = left;
      Right = right;

      left.Parent = right.Parent = this;
   }

   public T Value { get; }

   public Node<T>? Left { get; }

   public Node<T>? Right { get; }

   public Node<T>? Parent { get; set; }
}