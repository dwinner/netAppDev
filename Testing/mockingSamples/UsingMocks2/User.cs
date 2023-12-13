namespace UsingMocks2;

public class User
{
   public string? Name { get; set; }

   public string NormalizeName(string? name)
   {
      var result = (name ?? string.Empty).Trim();
      return result.Length > 50
         ? result.Substring(0, 50)
         : result;
   }
}