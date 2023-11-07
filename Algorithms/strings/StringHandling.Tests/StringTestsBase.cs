using System.Text.RegularExpressions;

namespace StringHandling.Tests;

public partial class StringTestsBase
{
   protected static async Task<string> GetContentAsync(string uriString)
   {
      using var httpClient = new HttpClient();
      var content = await httpClient.GetStringAsync(uriString)
         .ConfigureAwait(false);

      return content;
   }

   protected static string[] SplitByWords(string content)
   {
      var stringsToSort = WhiteSpaceRegex().Split(content)
         .Select(str => str.Trim())
         .Where(str => !string.IsNullOrEmpty(str))
         .ToArray();

      return stringsToSort;
   }

   [GeneratedRegex("\\s")]
   private static partial Regex WhiteSpaceRegex();
}