using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Contracts;
using Contracts.Events;
using Framework;
using Models;

namespace ViewModels
{
   public class BookViewModel : ViewModelBase, IDisposable
   {
      private readonly IBooksService _booksService;
      private Book _book;

      public BookViewModel(IBooksService booksService)
      {
         _booksService = booksService;
         SaveBookCommand = new DelegateCommand(() => OnSaveBookAsync().ConfigureAwait(true));
         EventAggregator<BookInfoEvent>.Instance.Event += LoadBook;
      }

      public ICommand SaveBookCommand { get; }

      public Book Book
      {
         get { return _book; }
         set { SetProperty(ref _book, value); }
      }

      public void Dispose() => EventAggregator<BookInfoEvent>.Instance.Event -= LoadBook;

      private void LoadBook(object sender, BookInfoEvent bookInfo)
         => Book = bookInfo.BookId == 0 ? new Book() : _booksService.GetBook(bookInfo.BookId);

      private async Task OnSaveBookAsync() => Book = await _booksService.AddOrUpdateBookAsync(Book).ConfigureAwait(true);
   }
}