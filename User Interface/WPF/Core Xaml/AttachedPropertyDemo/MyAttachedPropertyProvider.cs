using System.Windows;

namespace AttachedPropertyDemo
{
   /// <summary>
   /// Тип с присоединяемым свойством
   /// </summary>
   public class MyAttachedPropertyProvider : DependencyObject
   {
      public int MyProperty
      {
         get { return (int) GetValue(MyPropertyProperty); }
         set { SetValue(MyPropertyProperty, value); }
      }

      public static readonly DependencyProperty MyPropertyProperty = DependencyProperty.RegisterAttached("MyProperty",
         typeof (int), typeof (MyAttachedPropertyProvider));

      public static void SetMyProperty(UIElement element, int value)
      {
         element.SetValue(MyPropertyProperty, value);
      }

      public static int GetMyProperty(UIElement element)
      {
         return (int) element.GetValue(MyPropertyProperty);
      }
   }
}