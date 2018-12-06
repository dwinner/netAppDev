namespace FlickrLocart_App.Model
{
   /// <summary>
   ///    Gallery item
   /// </summary>
   public class GalleryItem
   {
      /// <summary>
      ///    Id
      /// </summary>
      public string Id { get; set; }

      /// <summary>
      ///    Caption
      /// </summary>
      public string Caption { get; set; }

      /// <summary>
      ///    Url
      /// </summary>
      public string Url { get; set; }

      /// <summary>
      ///    Photo owner
      /// </summary>
      public string Owner { get; set; }

      /// <summary>
      ///    Latitude
      /// </summary>
      public double Lat { get; set; }

      /// <summary>
      ///    Longitude
      /// </summary>
      public double Lon { get; set; }

      public override string ToString() => Caption;
   }
}