namespace Command
{
   public interface ISaver
   {
      void Redo(int levels);

      void Undo(int levels);
   }
}