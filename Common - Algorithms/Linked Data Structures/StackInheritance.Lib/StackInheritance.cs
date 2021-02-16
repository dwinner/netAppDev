using LinkedList.Lib;

namespace StackInheritance.Lib
{
   /// <summary>
   ///    Custom stack implementation
   /// </summary>
   /// <typeparam name="TItem">Stack item</typeparam>
   public class StackInheritance<TItem> : List<TItem>
   {
      private const string DefaultStackName = "stack";

      public StackInheritance()
         : base(DefaultStackName)
      {
      }

      /// <summary>
      ///    Place item value at the top of stack by inserting at the front of the linked list
      /// </summary>
      /// <param name="anItem">Item to push</param>
      public void Push(TItem anItem) => InsertAtFront(anItem);

      /// <summary>
      ///    Remove item from the top of the stack by removing item at the front of the linked list
      /// </summary>
      /// <returns></returns>
      public TItem Pop() => RemoveFromFront();
   }
}