// ''

namespace DataStructures.LinkBased;

/// <summary>
///    Linked list node
/// </summary>
/// <typeparam name="TItem">Type</typeparam>
public sealed class LinkedListNode<TItem>
{
   /// <summary>
   ///    Constructs a new node with the specified value.
   /// </summary>
   /// <param name="value">Value</param>
   public LinkedListNode(TItem? value) => Value = value;

   /// <summary>
   ///    The node value
   /// </summary>
   public TItem? Value { get; }

   /// <summary>
   ///    The next node in the linked list (null if last node)
   /// </summary>
   public LinkedListNode<TItem>? Next { get; set; }

   /// <summary>
   ///    The previous node in the linked list (null if first node)
   /// </summary>
   public LinkedListNode<TItem>? Previous { get; set; }
}