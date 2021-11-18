using System;

internal class Book : IEquatable<Book>
{
   public Book(string title, string publisher) => (Title, Publisher) = (title, publisher);

   public string Title { get; }

   public string Publisher { get; }

   protected virtual Type EqualityContract { get; } = typeof(Book);

   public virtual bool Equals(Book? other) => this == other;

   public override string ToString() => Title;

   public override bool Equals(object? obj) => this == obj as Book;

   public override int GetHashCode() => Title.GetHashCode() ^ Publisher.GetHashCode();

   public static bool operator ==(Book? left, Book? right) =>
      left?.Title == right?.Title && left?.Publisher == right?.Publisher &&
      left?.EqualityContract == right?.EqualityContract;

   public static bool operator !=(Book? left, Book? right) => !(left == right);
}