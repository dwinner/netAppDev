using Android.Net;

namespace FlickrLocart_App.Model
{
   public static class GalleryItemExtensions
   {
      private const string FlickrPhotosEndPoint = "http://www.flickr.com/photos/";

      public static Uri GetPhotoPageUri(this GalleryItem galleryItem) =>
         Uri.Parse(FlickrPhotosEndPoint)
            .BuildUpon()
            .AppendPath(galleryItem.Owner)
            .AppendPath(galleryItem.Id)
            .Build();
   }
}