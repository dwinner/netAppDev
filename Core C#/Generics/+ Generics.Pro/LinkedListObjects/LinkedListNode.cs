namespace LinkedListObjects
{
   /// <summary>
   /// Узел связного списка
   /// </summary>
   public class LinkedListNode
   {
      public object Value { get; private set; }

      public LinkedListNode Next { get; internal set; }

      public LinkedListNode Prev { get; internal set; }

      public LinkedListNode(object value)
      {
         Value = value;
      }      
   }
}