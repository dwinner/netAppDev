/**
 * Вложенный строитель объектов
 */

using System;

namespace InnerBuilder
{
   class Program
   {
      static void Main()
      {
         AudioEntity audioEntity = new AudioEntity.Builder("I'll be the one", 220, 192, 11)
            .BuildAlbum("Back street")
            .BuildGenre("Pop")
            .BuildGroup("Back street boys")
            .BuildRate(5)
            .BuildRecordFormat("mp3")
            .BuildTrackUrl("http://www.zaicev.net")
            .BuildYear(199)
            .Build();

         Console.WriteLine(audioEntity);
         Console.ReadKey();
      }
   }
}