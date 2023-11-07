namespace Command
{
   /// <summary>
   ///    Интерфейс команды
   /// </summary>
   public interface IExecuteCommand
   {
      void Execute();

      void UnExecute();
   }
}