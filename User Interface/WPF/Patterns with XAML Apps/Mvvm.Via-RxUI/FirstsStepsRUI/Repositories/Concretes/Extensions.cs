namespace FirstsStepsRUI.Repositories.Concretes
{
   public static class Extensions
   {
      public static bool IsValid(this string value)
      {
         return !value.IsInvalid();
      }

      public static bool IsInvalid(this string value)
      {
         return string.IsNullOrWhiteSpace(value);
      }
   }
}
