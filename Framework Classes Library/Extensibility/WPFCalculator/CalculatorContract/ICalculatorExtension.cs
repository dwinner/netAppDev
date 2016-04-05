using System.Windows;

namespace CalculatorContract
{
   /// <summary>
   /// Интерфейс для экспорта
   /// </summary>
   public interface ICalculatorExtension
   {
      FrameworkElement Ui { get; }
   }
}