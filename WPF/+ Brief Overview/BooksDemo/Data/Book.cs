using System.Collections.Generic;

namespace BooksDemo.Data
{
   public sealed class Book : BindableObject
   {
      private const string DefaultTitle = "unknown";
      private const string DefaultPublisher = "unknown";
      private const string DefaultIsbn = "unknown";
      private readonly List<string> _authors = new List<string>();
      private string _isbn;
      private string _publisher;

      private string _title;

      public Book(string aTitle, string aPublisher, string anIsbn, params string[] authors)
      {
         _title = aTitle;
         _publisher = aPublisher;
         _isbn = anIsbn;
         _authors.AddRange(authors);
      }

      public Book()
         : this(DefaultTitle, DefaultPublisher, DefaultIsbn)
      {
      }

      public string Title
      {
         get { return _title; }
         set { SetProperty(ref _title, value); }
      }

      public string Publisher
      {
         get { return _publisher; }
         set { SetProperty(ref _publisher, value); }
      }

      public string Isbn
      {
         get { return _isbn; }
         set { SetProperty(ref _isbn, value); }
      }

      public string[] Authors => _authors.ToArray();

      public override string ToString() => Title;
   }
}