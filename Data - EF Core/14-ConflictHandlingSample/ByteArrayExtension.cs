using System.Text;

namespace _14_ConflictHandlingSample
{
   public static class ByteArrayExtension
   {
      public static string StringOutput(this byte[] data)
      {
         var sb = new StringBuilder();
         foreach (var b in data)
         {
            sb.Append($"{b}.");
         }

         return sb.ToString();
      }
   }
}