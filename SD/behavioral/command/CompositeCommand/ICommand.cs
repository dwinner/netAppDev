namespace CompositeCommand;

public interface ICommand
{
   bool Success { get; }

   void Call();

   void Undo();
}