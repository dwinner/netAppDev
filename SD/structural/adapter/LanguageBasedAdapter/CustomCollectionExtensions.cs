namespace LanguageBasedAdapter
{
   public static class CustomCollectionExtensions
   {
      public static void Add(this CustomCollection self,
         int value) => self.Insert(value);
   }
}