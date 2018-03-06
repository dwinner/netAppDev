using System.Collections.Generic;

namespace CommandUndo
{
   public class CreateWidgetCommand : ICommand
   {
      private readonly ICollection<IWidget> _collection;
      private readonly IWidget _newWidget;

      public CreateWidgetCommand(ICollection<IWidget> collection, IWidget newWidget)
      {
         _collection = collection;
         _newWidget = newWidget;
      }

      public void Execute()
      {
         _collection.Add(_newWidget);
      }

      public void Undo()
      {
         _collection.Remove(_newWidget);
      }

      public string Name
      {
         get { return "Create new widget"; }
      }
   }
}
