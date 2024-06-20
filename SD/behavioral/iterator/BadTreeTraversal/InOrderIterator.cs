namespace BadTreeTraversal;

public class InOrderIterator<T> where T : notnull
{
   private readonly Node<T> _root;
   private bool _yieldedStart;

   public InOrderIterator(Node<T> root)
   {
      _root = Current = root;
      while (Current.Left != null)
      {
         Current = Current.Left;
      }
   }

   public Node<T> Current { get; set; }

   public void Reset()
   {
      Current = _root;
      _yieldedStart = true;
   }

   public bool MoveNext()
   {
      if (!_yieldedStart)
      {
         _yieldedStart = true;
         return true;
      }

      if (Current.Right != null)
      {
         Current = Current.Right;
         while (Current.Left != null)
         {
            Current = Current.Left;
         }

         return true;
      }

      var parent = Current.Parent;
      while (parent != null && Current == parent.Right)
      {
         Current = parent;
         parent = parent.Parent;
      }

      Current = parent;
      return Current != null;
   }
}