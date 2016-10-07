/**
 * Вложенный строитель объектов
 */

using System;

namespace InnerBuilder
{
   internal static class Program
   {
      private static void Main()
      {
         var audioEntity = new AudioEntity.Builder("I'll be the one", 220, 192, 11)
            .BuildAlbum("Back street")
            .BuildGenre("Pop")
            .BuildGroup("Back street boys")
            .BuildRate(5)
            .BuildRecordFormat("mp3")
            .BuildTrackUrl("http://www.zaicev.net")
            .BuildYear(199)
            .Build();

         Console.WriteLine(audioEntity.Genre);
         Console.WriteLine(audioEntity.Rate);

         Console.WriteLine(audioEntity);
         Console.ReadKey();
      }
   }
}