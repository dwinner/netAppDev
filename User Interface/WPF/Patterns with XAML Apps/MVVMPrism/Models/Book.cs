using Prism.Mvvm;

namespace Models
{
   public class Book : BindableBase
   {
      private string _publisher;

      private string _title;
      public int BookId { get; set; }

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
   }
}