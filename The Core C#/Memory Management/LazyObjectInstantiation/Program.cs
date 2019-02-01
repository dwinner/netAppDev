using System;

namespace LazyObjectInstantiation
{
   #region classes to represent a song and all songs.
   // Представляет одну композицию. 
   class Song
   {
      public string Artist { get; set; }
      public string TrackName { get; set; }
      public double TrackLength { get; set; }
   }

   // Представляет все композиции.
   class AllTracks
   {
      // 10000 композиций в плейере.
      private Song[] _allSongs = new Song[10000];

      public AllTracks()
      {
         // Гипотетическое заполнение массива объектов         
         Console.WriteLine("Filling up the songs!");
      }
   }
   #endregion

   #region Media player class
   class MediaPlayer
   {
      // Предполагаем, что эти методы делают нечто полезное.
      public void Play() { /* Play a song */ }
      public void Pause() { /* Pause the song */ }
      public void Stop() { /* Stop playback */ }

      // Использование лямбда-выражения для долбавления
      // дополнительного кода при создании объекта AllTracks.
      private readonly Lazy<AllTracks> _allSongs = new Lazy<AllTracks>(() =>
      {
         Console.WriteLine("Creating AllTracks object!");
         return new AllTracks();
      });

      public AllTracks GetAllTracks()
      {
         return _allSongs.Value;
      }
   }
   #endregion

   class Program
   {
      static void Main()
      {
         Console.WriteLine("***** Fun with Lazy Instantiation *****\n");

         // Никакого размещения объекта AllTracks!
         var myPlayer = new MediaPlayer();
         myPlayer.Play();

         // Размещение объекта AllTracks происходит только в случае вызова метода GetAllTracks().
         var yourPlayer = new MediaPlayer();
         AllTracks yourMusic = yourPlayer.GetAllTracks();

         Console.ReadLine();
      }
   }
}
