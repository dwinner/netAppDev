using System;
using System.Threading.Tasks;
using BooksLib.Models;
using BooksLib.Repositories;
using BooksLib.Services;
using Moq;
using Xunit;

namespace BooksLib.Tests.Services;

public class BooksServiceTests : IDisposable
{
   private const string TestTitle = "Test Title";
   private const string UpdatedTestTitle = "Updated Test Title";
   private const string APublisher = "A Publisher";
   private readonly BooksService _booksService;

   private readonly Book _expectedBook = new()
   {
      BookId = 1,
      Title = TestTitle,
      Publisher = APublisher
   };

   private readonly Book _newBook = new()
   {
      BookId = 0,
      Title = TestTitle,
      Publisher = APublisher
   };

   private readonly Book _notInRepositoryBook = new()
   {
      BookId = 42,
      Title = TestTitle,
      Publisher = APublisher
   };

   private readonly Book _updatedBook = new()
   {
      BookId = 1,
      Title = UpdatedTestTitle,
      Publisher = APublisher
   };

   public BooksServiceTests()
   {
      Mock<IBooksRepository> mock = new();
      mock.Setup(repository => repository.AddAsync(_newBook)).ReturnsAsync(_expectedBook);
      mock.Setup(repository => repository.UpdateAsync(_notInRepositoryBook)).ReturnsAsync(null as Book);
      mock.Setup(repository => repository.UpdateAsync(_updatedBook)).ReturnsAsync(_updatedBook);

      _booksService = new BooksService(mock.Object);
   }

   public void Dispose()
   {
   }

   [Fact]
   public async Task GetBook_ReturnsExistingBook()
   {
      // arrange
      await _booksService.AddOrUpdateBookAsync(_newBook);
      // act
      var actualBook = _booksService.GetBook(1);
      // assert
      Assert.Equal(_expectedBook, actualBook);
   }

   [Fact]
   public void GetBook_ReturnsNullForNotExistingBook()
   {
      // arrange in constructor
      // act
      var actualBook = _booksService.GetBook(42);
      // assert
      Assert.Null(actualBook);
   }

   [Fact]
   public async Task AddOrUpdateBookAsync_ThrowsForNull()
   {
      // arrange
      Book? nullBook = null;
      // act and assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => _booksService.AddOrUpdateBookAsync(nullBook!));
   }

   [Fact]
   public async Task AddOrUpdateBook_AddedBookReturnsFromRepository()
   {
      // arrange in constructor
      // act
      var actualAdded = await _booksService.AddOrUpdateBookAsync(_newBook);

      // assert
      Assert.Equal(_expectedBook, actualAdded);
      Assert.Contains(_expectedBook, _booksService.Books);
   }

   [Fact]
   public async Task AddOrUpdateBook_UpdateBook()
   {
      // arrange
      await _booksService.AddOrUpdateBookAsync(_newBook);

      // act
      var updatedBook = await _booksService.AddOrUpdateBookAsync(_updatedBook);

      // assert
      Assert.Equal(_updatedBook, updatedBook);
      Assert.Contains(_updatedBook, _booksService.Books);
   }

   [Fact]
   public async Task AddOrUpdateBook_UpdateNotExistingBookThrows()
   {
      // arrange in constructor
      // act and assert
      await Assert.ThrowsAsync<InvalidOperationException>(
         () => _booksService.AddOrUpdateBookAsync(_notInRepositoryBook));
   }
}