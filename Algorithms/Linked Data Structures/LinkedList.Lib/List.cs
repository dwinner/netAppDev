using System.Text;

namespace LinkedList.Lib
{
   /// <summary>
   ///    Linked list
   /// </summary>
   /// <typeparam name="TItem">List item type</typeparam>
   public class List<TItem>
   {
      private const string DefaultListName = "list";
      private ListNode<TItem> _firstNode;
      private ListNode<TItem> _lastNode;
      private readonly string _name;

      public List(string listName) => _name = listName;

      public List() : this(DefaultListName)
      {
      }

      /// <summary>
      ///    Returns true if list is empty
      /// </summary>
      public bool IsEmpty => _firstNode == null;

      /// <summary>
      ///    Insert item at front of the list
      /// </summary>
      /// <remarks>
      ///    If list is empty, first node and last node will refer to the same item.
      ///    Otherwise, first node refers to the new node
      /// </remarks>
      /// <param name="itemToInsert">Item to insert</param>
      public void InsertAtFront(TItem itemToInsert) =>
         _firstNode = IsEmpty
            ? (_lastNode = new ListNode<TItem>(itemToInsert))
            : new ListNode<TItem>(itemToInsert, _firstNode);

      /// <summary>
      ///    Insert item at the end of list.
      ///    <remarks>
      ///       If list is empty, the first node and the last node will refer
      ///       to the same item. Otherwise, last node's Next property refers to the new node
      ///    </remarks>
      /// </summary>
      /// <param name="itemToInsert">Item to insert</param>
      public void InsertAtBack(TItem itemToInsert) =>
         _lastNode = IsEmpty
            ? (_firstNode = new ListNode<TItem>(itemToInsert))
            : (_lastNode.Next = new ListNode<TItem>(itemToInsert));

      /// <summary>
      ///    Remove the first node from the list
      /// </summary>
      /// <returns>Removed node</returns>
      public TItem RemoveFromFront()
      {
         if (IsEmpty) throw new EmptyListException(_name);

         var itemToRemove = _firstNode.Data;
         _firstNode = _firstNode == _lastNode ? (_lastNode = null) : _firstNode.Next;

         return itemToRemove;
      }

      /// <summary>
      ///    Remove last node from the list
      /// </summary>
      /// <returns>Removed item</returns>
      public TItem RemoveFromBack()
      {
         if (IsEmpty) throw new EmptyListException(_name);

         var itemToRemove = _lastNode.Data;
         if (_firstNode == _lastNode)
            _firstNode = _lastNode = null;
         else
         {
            var current = _firstNode;

            // loop while current.Next is not a last node
            while (current.Next != _lastNode) current = current.Next;

            // now the current is a new last node
            _lastNode = current;
            current.Next = null;
         }

         return itemToRemove;
      }

      public override string ToString()
      {
         var accumulator = new StringBuilder();
         if (IsEmpty)
            accumulator.AppendLine($"Empty {_name}");
         else
         {
            accumulator.Append($"The {_name} is: ");
            var current = _firstNode;
            while (current != null)
            {
               accumulator.Append($"{current.Data}");
               current = current.Next;
            }

            accumulator.AppendLine();
         }

         return accumulator.ToString();
      }
   }
}