namespace CalculatorContract
{
   /// <summary>
   /// Интерфейс для экспорта
   /// </summary>
   public interface IOperation
   {
      string Name { get; }

      int NumberOperands { get; }
   }
}