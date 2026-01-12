namespace TypeExtensions;

public static class StringExtensions
{
   extension(string @this)
   {
      public bool IsNumber => @this.All(@char => @char is > '0' and < '9');
      public int GetCharCount(char aChar) => @this.Count(@char => @char == aChar);
   }

   extension(string)
   {
      public static string Heaven => "Heaven";

      public static bool operator >(string first, string second) => string.GreaterThan(first, second);

      public static bool operator <(string first, string second) => !string.GreaterThan(first, second);

      private static bool GreaterThan(string first, string second) => first.Length > second.Length;
   }
}