namespace ArticleTests;

public class Comment
{
   public readonly string Author;
   public readonly DateTime DateCreated;
   public readonly string Text;

   public Comment(string text, string author, DateTime dateCreated)
   {
      Text = text;
      Author = author;
      DateCreated = dateCreated;
   }

   protected bool Equals(Comment other) =>
      string.Equals(Text, other.Text)
      && string.Equals(Author, other.Author)
      && DateCreated.Equals(other.DateCreated);

   public override bool Equals(object obj)
   {
      if (ReferenceEquals(null, obj))
      {
         return false;
      }

      if (ReferenceEquals(this, obj))
      {
         return true;
      }

      if (obj.GetType() != GetType())
      {
         return false;
      }

      return Equals((Comment)obj);
   }

   public override int GetHashCode()
   {
      unchecked
      {
         var hashCode = Text != null ? Text.GetHashCode() : 0;
         hashCode = (hashCode * 397) ^ (Author != null ? Author.GetHashCode() : 0);
         hashCode = (hashCode * 397) ^ DateCreated.GetHashCode();
         return hashCode;
      }
   }
}