using System.Linq;
using System.Text.RegularExpressions;

namespace CaplAutoCompletion
{
    public static class StringExtensions
    {
        public static string StripTags(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
            {
                return @this;
            }

            string[] patterns =
            {
                @"<(.|\n)*?>",
                @"<script.*?</script>"
            };

            return patterns.Aggregate(@this, (current, pattern) => Regex.Replace(current, pattern, string.Empty));
        }
    }
}