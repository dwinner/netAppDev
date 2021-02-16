/**
 * Свойства зависимости
 */

using System;

namespace DependencyObjectDemo
{
   internal class Program
   {
      private static void Main()
      {
         var dependencyObject = new MyDependencyObject();
         dependencyObject.ValueChanged +=
            (sender, args) => Console.WriteLine("Value changed from {0} To {1}", args.OldValue, args.NewValue);

         dependencyObject.Value = 33;
         Console.WriteLine(dependencyObject.Maximum);
         dependencyObject.Value = 210;
         Console.WriteLine(dependencyObject.Value);

         object value = dependencyObject.GetValue(MyDependencyObject.ValueProperty);
         Console.WriteLine(value);
      }
   }
}