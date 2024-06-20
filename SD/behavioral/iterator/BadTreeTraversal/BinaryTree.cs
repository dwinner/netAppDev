namespace BadTreeTraversal;

public class BinaryTree<T>(Node<T> root)
   where T : notnull
{
   public IEnumerable<Node<T>> NaturalInOrder
   {
      get
      {
         foreach (var node in TraverseInOrder(root))
         {
            yield return node;
         }

         yield break;

         IEnumerable<Node<T>> TraverseInOrder(Node<T> current)
         {
            while (true)
            {
               if (current.Left != null)
               {
                  foreach (var left in TraverseInOrder(current.Left))
                  {
                     yield return left;
                  }
               }

               yield return current;

               if (current.Right != null)
               {
                  current = current.Right;
                  continue;
               }

               break;
            }
         }
      }
   }

   public InOrderIterator<T> GetEnumerator() => new(root);
}