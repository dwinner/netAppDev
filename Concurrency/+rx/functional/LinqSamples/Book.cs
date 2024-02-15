namespace LinqSamples;

internal class Book
{
   public Book(string name, int authorId)
   {
      Name = name;
      AuthorId = authorId;
   }

   public string Name { get; set; }

   public int AuthorId { get; set; }
}