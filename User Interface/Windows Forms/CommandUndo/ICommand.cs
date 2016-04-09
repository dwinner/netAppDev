namespace CommandUndo
{
   /// <summary>
   /// Интерфейс для команд пользователя
   /// </summary>
   public interface ICommand
   {
      void Execute();

      void Undo();

      string Name { get; }
   }
}
