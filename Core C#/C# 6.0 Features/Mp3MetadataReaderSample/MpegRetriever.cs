using System.IO;
using System.Threading.Tasks;
using Id3;
using static System.IO.FileMode;
using static System.IO.FileAccess;

namespace Mp3MetadataReaderSample
{
   public static class MpegRetriever
   {
      public static TrackInfo GetTrackInfo(string mp3Path)
      {
         var trackInfo = new TrackInfo();

         using (var currentStream = new Mp3Stream(new FileStream(mp3Path, Open, Read)))
         {
            var id3Tag = currentStream.GetTag(Id3TagFamily.FileStartTag);
            trackInfo.Atrist = id3Tag.Artists?.Value;
            trackInfo.Title = id3Tag.Title?.Value;
            trackInfo.Bitrate = currentStream.Audio.Bitrate;
            trackInfo.Duration = currentStream.Audio.Duration;
            trackInfo.Frequency = currentStream.Audio.Frequency;
         }

         return trackInfo;
      }

      public static Task<TrackInfo> GetTrackInfoAsync(string mp3Path) => Task.Run(() => GetTrackInfo(mp3Path));
   }
}