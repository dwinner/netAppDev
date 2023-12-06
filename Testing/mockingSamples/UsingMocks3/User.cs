namespace UsingMocks3;

public class User
{
   private string _name;

   public string Name
   {
      get => _name;
      set => _name = NormalizeName(value);
   }

   private string NormalizeName(string name)
   {
      var result = (name ?? "").Trim();

      if (result.Length > 50)
      {
         return result.Substring(0, 50);
      }

      return result;
   }
}