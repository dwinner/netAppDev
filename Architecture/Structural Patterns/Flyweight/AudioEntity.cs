namespace Flyweight
{
   public class AudioEntity
   {
      public string TrackName { get; set; }
      public int Duration { get; set; }
      public int Bitrate { get; set; }
      public string TrackUrl { get; set; }
      public string Group { get; set; }
      public int Year { get; set; }
      public string RecordFormat { get; set; }
      public string Album { get; set; }
      public int Size { get; set; }
      public string Genre { get; set; }

      public override string ToString()
         => $"Album: {Album}, TrackName: {TrackName}";
   }
}