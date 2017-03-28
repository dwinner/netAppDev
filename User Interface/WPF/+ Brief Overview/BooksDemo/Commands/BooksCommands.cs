using System.Windows.Input;
using static System.Windows.Input.Key;
using static System.Windows.Input.ModifierKeys;

namespace BooksDemo.Commands
{
   public static class BooksCommands
   {
      public static ICommand ShowBook { get; } = new RoutedUICommand("Show Book", "ShowBook", typeof(BooksCommands));

      public static RoutedUICommand ShowBooksList { get; } = new RoutedUICommand("Show Books", "ShowBooks",
         typeof(BooksCommands), new InputGestureCollection(new[] {new KeyGesture(B, Alt)}));

      public static RoutedUICommand ShowBooksGrid { get; } = new RoutedUICommand("Show Books Grid", "ShowBooksGrid",
         typeof(BooksCommands));

      public static RoutedUICommand ShowAuthors { get; } = new RoutedUICommand("Show Authors", "ShowAuthors",
         typeof(BooksCommands));
   }
}