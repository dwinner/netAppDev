using System;
using System.Windows;

namespace DependencyObjectDemo
{
   /// <summary>
   /// Класс со свойствами зависимости
   /// </summary>
   public class MyDependencyObject : UIElement
   {
      public int Value
      {
         get { return (int)GetValue(ValueProperty); }
         set { SetValue(ValueProperty, value); }
      }

      public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (int),
         typeof (MyDependencyObject), new PropertyMetadata(0, OnValueChanged, CoerceValue));     

      public int Minimum
      {
         get { return (int)GetValue(MinimumProperty); }
         set { SetValue(MinimumProperty, value); }
      }

      public DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(int),
         typeof(MyDependencyObject), new PropertyMetadata(0));

      public int Maximum
      {
         get { return (int)GetValue(MaximumProperty); }
         set { SetValue(MaximumProperty, value); }
      }

      public DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(int),
         typeof(MyDependencyObject), new PropertyMetadata(100));

      private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args) // Обратный вызов при изменении свойства зависимости
      {
         var control = (MyDependencyObject) obj;
         var e = new RoutedPropertyChangedEventArgs<int>((int) args.OldValue, (int) args.NewValue, ValueChangedEvent);
         control.OnValueChanged(e);
      }      

      public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged",
         RoutingStrategy.Bubble, typeof (RoutedPropertyChangedEventArgs<int>), typeof (MyDependencyObject));

      public event RoutedPropertyChangedEventHandler<int> ValueChanged
      {
         add
         {
            AddHandler(ValueChangedEvent, value);
         }
         remove
         {
            RemoveHandler(ValueChangedEvent, value);
         }
      }

      protected virtual void OnValueChanged(RoutedPropertyChangedEventArgs<int> args)
      {
         RaiseEvent(args);
      }

      private static object CoerceValue(DependencyObject element, object value)  // Проверка дмапазона значения при изменении свойства зависимости
      {
         var newValue = (int) value;
         var control = (MyDependencyObject) element;
         newValue = Math.Max(control.Minimum, Math.Min(control.Maximum, newValue));
         return newValue;
      }
   }
}