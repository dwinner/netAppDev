namespace Reimplementing;

internal static class Program
{
   private static void Main()
   {
      // A subclass can reimplement any interface member already implemented by a base class.
      // Reimplementation hijacks a member implementation (when called through the interface):

      // Calling the reimplemented member through the interface calls the subclass’s implementation:
      var r = new RichTextBox();
      r.Undo(); // RichTextBox.Undo      Case 1
      ((IUndoable)r).Undo(); // RichTextBox.Undo      Case 2
   }
}

public interface IUndoable
{
   void Undo();
}

public class TextBox : IUndoable
{
   void IUndoable.Undo() => Console.WriteLine("TextBox.Undo");
}

public class RichTextBox : TextBox, IUndoable
{
   public void Undo() => Console.WriteLine("RichTextBox.Undo");
}