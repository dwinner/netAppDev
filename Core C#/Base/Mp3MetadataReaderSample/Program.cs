// Новые языковые констукции C# 6.0 (Roslyn)

using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using static System.IO.Directory;
using static Mp3MetadataReaderSample.MpegRetriever;

namespace Mp3MetadataReaderSample
{
   internal static class Program
   {
      private const string Mp3Dir = "Mp3Files";
      private const string Mp3Ext = "*.mp3";

      private static void Main()
      {
         var mp3Files = EnumerateFiles(Mp3Dir, Mp3Ext);
         var tracks = mp3Files.Select(GetTrackInfo).ToList();
         tracks.ForEach(WriteLine);

         // nameof: удобно при отражении и чтении атрибутов
         WriteLine(nameof(TrackInfo.Title));
         WriteLine(nameof(TrackInfo.Atrist));
         WriteLine(nameof(TrackInfo.Bitrate));
         WriteLine(nameof(TrackInfo.Duration));
         WriteLine(nameof(TrackInfo.Frequency));

         // Index initializers + Exception filters
         try
         {
            var trackMap = new Dictionary<int, TrackInfo>
            {
               [0] = tracks[0],
               [1] = tracks[1],
               [2] = tracks[2],
               [3] = tracks[3] // NOTE: вбросит исключение ArgumentOutOfRangeException
            };
            WriteLine(trackMap);
         }
         catch (ArgumentOutOfRangeException argOutEx) when (!Log(argOutEx))
         {
            /* NOTE:
            Если Log(argOutEx) = true, тогда мы "проглотим" исключение,
            в противном случае оно останется необработанным и будет дальше раскручивать стек.
            Однако логирование произойдет в любом случае
            */
            //WriteLine(argOutEx);
         }
      }

      private static bool Log(Exception exception)
      {
         WriteLine($"Log: {exception.Message}");
         return !(exception is ArgumentOutOfRangeException);
      }
   }
}