namespace ReverseLinkedList
{
   internal class Node<T>
   {
      public Node(T val)
      {
         Value = val;
         Next = null;
      }

      public T Value { get; private set; }

      public Node<T> Next { get; set; }
   }
}