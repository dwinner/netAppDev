using System;
using static System.String;

namespace Mp3MetadataReaderSample
{
   public sealed class TrackInfo
   {
      public string Atrist { get; set; } = Empty;
      public string Title { get; set; } = Empty;
      public TimeSpan Duration { get; set; } = TimeSpan.Zero;
      public int Bitrate { get; set; }
      public int Frequency { get; set; }

      public override string ToString()
         => $"Atrist: {Atrist}, Title: {Title}, Duration: {Duration}, Bitrate: {Bitrate}, Frequency: {Frequency}";
   }
}