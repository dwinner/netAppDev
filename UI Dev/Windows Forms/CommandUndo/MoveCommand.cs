using System.Drawing;

namespace CommandUndo
{
   public class MoveCommand : ICommand
   {
      private readonly Point _originalLocation;
      private readonly Point _newLocation;
      private readonly IWidget _widget;

      public MoveCommand(IWidget widget, Point originalLocation, Point newLocation)
      {
         _widget = widget;
         _originalLocation = originalLocation;
         _newLocation = newLocation;
      }

      public void Execute()
      {
         _widget.Location = _newLocation;
      }

      public void Undo()
      {
         _widget.Location = _originalLocation;
      }

      public string Name
      {
         get { return "Move widget"; }
      }
   }
}
