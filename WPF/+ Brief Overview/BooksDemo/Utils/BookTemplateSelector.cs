using System.Windows;
using System.Windows.Controls;
using BooksDemo.Data;

namespace BooksDemo.Utils
{
   public sealed class BookTemplateSelector : DataTemplateSelector
   {
      public override DataTemplate SelectTemplate(object item, DependencyObject container)
      {
         var bookItem = item as Book;
         if (bookItem == null)
            return null;

         var frameworkElement = container as FrameworkElement;
         if (frameworkElement == null)
            return null;

         switch (bookItem.Publisher)
         {
            case "Wrox Press":
               return frameworkElement.TryFindResource("WroxTemplate") as DataTemplate;
            case "For Dummies":
               return frameworkElement.TryFindResource("DummiesTemplate") as DataTemplate;
            default:
               return frameworkElement.TryFindResource("BookTemplate") as DataTemplate;
         }
      }
   }
}