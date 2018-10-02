using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
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

      public override int Count => _poiListData.Count;

      public override PointOfInterest this[int position] => _poiListData[position];

      public override long GetItemId(int position) => position;

      public override View GetView(int position, View convertView, ViewGroup parent)
      {
         var view = convertView
                    ?? _context.LayoutInflater.Inflate(Layout.PoiListItem, null, false);

         // Get the UI-controls
         var poi = this[position];
         var nameTextView = view.FindViewById<TextView>(Id.nameTextView);
         var addressTextView = view.FindViewById<TextView>(Id.addrTextView);
         //var poiImageView = view.FindViewById<ImageView>(Id.poiImageView);

         // Fill the UI-controls
         nameTextView.Text = poi.Name;
         if (string.IsNullOrEmpty(poi.Address))
         {            
            addressTextView.Visibility = ViewStates.Gone;
         }
         else
         {
            addressTextView.Text = poi.Address;
            //Koush.UrlImageViewHelper.SetUrlDrawable(poiImageView, poi.Image, Drawable.ic_placeholder);
         }                  

         return view;
      }
   }
}