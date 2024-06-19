namespace UserNamesFlyweight;

public class User2
{
   private static readonly List<string> Strings = new();
   private readonly int[] _names;

   public User2(string fullName)
   {
      _names = fullName.Split(' ').Select(GetOrAdd).ToArray();
      return;

      int GetOrAdd(string s)
      {
         var idx = Strings.IndexOf(s);
         if (idx != -1)
         {
            return idx;
         }

         Strings.Add(s);
         return Strings.Count - 1;
      }
   }

   public string FullName => string.Join(" ", _names.Select(i => Strings[i]));
}