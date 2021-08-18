using System.Windows;
using BooksDemo.Data;

namespace BooksDemo.UserControls
{
   public partial class BookUserControl
   {
      public BookUserControl()
      {
         InitializeComponent();

         #region Программная привязка к элементам

         //var binding = new Binding {Path = new PropertyPath("Value"), Source = ResizeSlider};
         //BindingOperations.SetBinding(BookGridTransform, ScaleTransform.ScaleXProperty, binding);
         //BindingOperations.SetBinding(BookGridTransform, ScaleTransform.ScaleYProperty, binding);

         #endregion
      }

      private void OnShowBook(object sender, RoutedEventArgs e)
      {
         var currentBook = DataContext as Book;
         if (currentBook != null)
            MessageBox.Show(currentBook.Title, currentBook.Isbn);
      }

      private void OnChangeBook(object sender, RoutedEventArgs e)
      {
         var currentBook = DataContext as Book;
         if (currentBook != null)
         {
            currentBook.Title = "Professional C# 5";
            currentBook.Isbn = "978-0-470-31442-5";
         }
      }
   }
}