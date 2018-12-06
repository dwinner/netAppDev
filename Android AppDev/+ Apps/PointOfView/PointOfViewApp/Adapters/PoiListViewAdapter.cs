using System.Collections.Generic;
using Android.App;
using Android.Locations;
using Android.Views;
using Android.Widget;
using Koush;
using PointOfViewApp.Poco;
using static PointOfViewApp.Resource;

namespace PointOfViewApp.Adapters
{
   public class PoiListViewAdapter : BaseAdapter<PointOfInterest>
   {
      private readonly Activity _context;
      private readonly List<PointOfInterest> _poiListData;

      public PoiListViewAdapter(Activity context, List<PointOfInterest> poiListData)
      {
         _context = context;
         _poiListData = poiListData;
      }

      public Location CurrentLocation { get; set; }

      public override int Count => _poiListData.Count;

      public override PointOfInterest this[int position] => _poiListData[position];

      public override long GetItemId(int position) => position;

      public override View GetView(int position, View convertView, ViewGroup parent)
      {
         // reuse an existing view, if one is available otherwise create a new one
         var view = convertView
                    ?? _context.LayoutInflater.Inflate(Layout.PoiListItem, null, false);

         // Get the UI-controls
         var poi = this[position];
         var nameTextView = view.FindViewById<TextView>(Id.nameTextView);
         var addressTextView = view.FindViewById<TextView>(Id.addrTextView);
         var poiImageView = view.FindViewById<ImageView>(Id.poiImageView);
         var distanceTextView = view.FindViewById<TextView>(Id.distanceTextView);

         // Fill the UI-controls
         nameTextView.Text = poi.Name;

         if (string.IsNullOrEmpty(poi.Address))
            addressTextView.Visibility = ViewStates.Gone;
         else
         {
            addressTextView.Text = poi.Address;
            UrlImageViewHelper.SetUrlDrawable(poiImageView, poi.Image, Drawable.ic_placeholder);
         }

         if (CurrentLocation != null && poi.Latitude.HasValue && poi.Longitude.HasValue)
         {
            var poiLocation = new Location(string.Empty)
            {
               Latitude = poi.Latitude.Value,
               Longitude = poi.Longitude.Value
            };
            var distance = CurrentLocation.DistanceTo(poiLocation) * 0.000621371F; // 6.21371E-4F
            distanceTextView.Text = $"{distance:0,0.00} miles";
         }
         else
            distanceTextView.Text = "??";

         return view;
      }
   }
}