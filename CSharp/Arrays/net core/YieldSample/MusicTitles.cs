using System.Collections.Generic;

public class MusicTitles
{
   private readonly string[] _names =
   {
      "Tubular Bells",
      "Hergest Ridge",
      "Ommadawn",
      "Platinum"
   };

   public IEnumerator<string> GetEnumerator()
   {
      for (var i = 0; i < _names.Length; i++)
      {
         yield return _names[i];
      }
   }

   public IEnumerable<string> Reverse()
   {
      for (var i = _names.Length - 1; i >= 0; i--)
      {
         yield return _names[i];
      }
   }

   public IEnumerable<string> Subset(int index, int length)
   {
      for (var i = index; i < index + length; i++)
      {
         yield return _names[i];
      }
   }
}