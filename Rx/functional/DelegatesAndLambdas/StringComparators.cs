namespace DelegatesAndLambdas
{
   internal class StringComparators
   {
      public static bool CompareLength(string first, string second) => first.Length == second.Length;

      public static bool CompareContent(string first, string second) => first == second;
   }
}