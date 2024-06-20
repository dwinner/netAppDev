namespace CommandSample;

public interface ICommand
{
   void Call();
   void Undo();
}