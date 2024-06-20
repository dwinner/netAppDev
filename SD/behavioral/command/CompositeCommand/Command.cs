namespace CompositeCommand;

public abstract class Command : ICommand
{
   public abstract void Call();

   public abstract void Undo();

   public bool Success { get; set; }
}