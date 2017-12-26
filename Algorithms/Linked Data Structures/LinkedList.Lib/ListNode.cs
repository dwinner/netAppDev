namespace LinkedList.Lib
{
   /// <summary>
   ///    Represents one node in a list
   /// </summary>
   /// <typeparam name="TNode">Type of a node</typeparam>
   internal class ListNode<TNode>
   {
      /// <summary>
      ///    Ctor to create ListNode
      /// </summary>
      /// <param name="data">Data value</param>
      /// <param name="next">The next node</param>
      public ListNode(TNode data, ListNode<TNode> next)
      {
         Data = data;
         Next = next;
      }

      /// <inheritdoc />
      public ListNode(TNode data) : this(data, null) => Data = data;

      /// <summary>
      ///    Node data
      /// </summary>
      public TNode Data { get; }

      /// <summary>
      ///    The next node
      /// </summary>
      public ListNode<TNode> Next { get; set; }
   }
}