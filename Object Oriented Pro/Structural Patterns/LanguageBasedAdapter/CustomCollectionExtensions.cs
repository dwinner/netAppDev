namespace LanguageBasedAdapter
{
   public static class CustomCollectionExtensions
   {
      public static void Add(this CustomCollection @this,
         int value) => @this.Insert(value);
   }
}