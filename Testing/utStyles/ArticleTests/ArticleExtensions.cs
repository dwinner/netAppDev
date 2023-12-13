namespace ArticleTests;

public static class ArticleExtensions
{
   public static Article ShouldContainNumberOfComments(this Article article, int commentCount)
   {
      Assert.Equal(1, article.Comments.Count);
      return article;
   }

   public static Article WithComment(this Article article, string text, string author, DateTime dateCreated)
   {
      var comment =
         article.Comments.SingleOrDefault(x => x.Text == text && x.Author == author && x.DateCreated == dateCreated);
      Assert.NotNull(comment);
      return article;
   }
}