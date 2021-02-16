using System;
using System.Globalization;
using System.Windows.Markup;

namespace MarkupExtensionDemo
{
   /// <summary>
   /// Тип для расширения разметки
   /// </summary>
   [MarkupExtensionReturnType(typeof(string))]
   public class CalculatorExtension : MarkupExtension
   {
      public double X { get; set; }
      public double Y { get; set; }
      public Operation Operation { get; set; }

      public override object ProvideValue(IServiceProvider serviceProvider)
      {
         /*var provideValue = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
         if (provideValue != null)
         {
            var host = provideValue.TargetObject as FrameworkElement;
            var prop = provideValue.TargetProperty as DependencyProperty;
         }*/

         double result;
         switch (Operation)
         {
            case Operation.Add:
               result = X + Y;
               break;
            case Operation.Subtract:
               result = X - Y;
               break;
            case Operation.Multiply:
               result = X * Y;
               break;
            case Operation.Divide:
               result = Math.Abs(Y) > double.Epsilon ? X / Y : double.MaxValue;
               break;
            default:
               throw new ArgumentException("Invalid Operation");
         }

         return result.ToString(CultureInfo.InvariantCulture);
      }
   }

   public enum Operation
   {
      Add,
      Subtract,
      Multiply,
      Divide
   }
}