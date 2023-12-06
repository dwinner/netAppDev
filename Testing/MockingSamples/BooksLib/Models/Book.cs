using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BooksLib.Models;

public class Book : ObservableObject
{
   private string? _publisher;

   private string? _title;
   public int BookId { get; set; }

   public string? Title
   {
      get => _title;
      set => SetProperty(ref _title, value);
   }

   public string? Publisher
   {
      get => _publisher;
      set => SetProperty(ref _publisher, value);
   }

   public override string ToString() => Title ?? string.Empty;
}