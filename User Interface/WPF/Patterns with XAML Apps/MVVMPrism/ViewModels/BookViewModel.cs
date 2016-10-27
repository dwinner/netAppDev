using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Contracts;
using Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ViewModels.Events;

namespace ViewModels
{
   public class BookViewModel : BindableBase
   {
      private readonly IBooksRepository _booksRepository;
      private Book _book;
      private EditBookMode _mode;

      public BookViewModel(IBooksRepository booksRepository, IEventAggregator eventAggregator)
      {
         _booksRepository = booksRepository;
         eventAggregator.GetEvent<BookEvent>().Subscribe(info => SetBookAsync(info).ConfigureAwait(true));
         SaveBookCommand = new DelegateCommand(() => OnSaveBookAsync().ConfigureAwait(true));
      }

      public ICommand SaveBookCommand { get; }

      public Book Book
      {
         get { return _book; }
         set { SetProperty(ref _book, value); }
      }

      public EditBookMode Mode
      {
         get { return _mode; }
         set { SetProperty(ref _mode, value); }
      }

      private async Task SetBookAsync(BookInfo bookInfo)
      {
         if (bookInfo.BookId == 0)
         {
            Mode = EditBookMode.AddNew;
         }
         else // get an existing book from the repository
         {
            Mode = EditBookMode.Edit;
            Book = await _booksRepository.GetItemAsync(bookInfo.BookId).ConfigureAwait(true);
         }
      }

      private async Task OnSaveBookAsync()
      {
         switch (Mode)
         {
            case EditBookMode.Edit:
               await _booksRepository.UpdateAsync(Book).ConfigureAwait(true);
               break;
            case EditBookMode.AddNew:
               await _booksRepository.AddAsync(Book).ConfigureAwait(true);
               break;
            default:
               throw new ArgumentOutOfRangeException(nameof(Mode));
         }
      }
   }
}