using System;
using Android.App;
using System.Collections.Generic;
using Android.Widget;
using Android.Views;
using Android.Locations;

namespace POIApp
{
	public class POIListViewAdapter : BaseAdapter<PointOfInterest>
	{
		private readonly Activity context;
		private List<PointOfInterest> poiListData;

		public POIListViewAdapter (Activity _context, List<PointOfInterest> _list)
			:base()
		{
			this.context = _context;
			this.poiListData = _list;
		}

		public override int Count {
			get { return poiListData.Count; }
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override PointOfInterest this[int index] {
			get { return poiListData [index]; }
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView; 

			// re-use an existing view, if one is available otherwise create a new one
			if (view == null) {
				view = context.LayoutInflater.Inflate (Resource.Layout.POIListItem, null, false);
			}

			PointOfInterest poi = this [position];
			view.FindViewById<TextView>(Resource.Id.nameTextView).Text = poi.Name;

			if (String.IsNullOrEmpty (poi.Address)) {
				view.FindViewById<TextView> (Resource.Id.addrTextView).Visibility = ViewStates.Gone;
			} else {
				view.FindViewById<TextView>(Resource.Id.addrTextView).Text = poi.Address;
			}

			var imageView = view.FindViewById<ImageView> (Resource.Id.poiImageView);
			if (!String.IsNullOrEmpty (poi.Address)) {
				Koush.UrlImageViewHelper.SetUrlDrawable (imageView,  poi.Image, Resource.Drawable.ic_placeholder);
			}

			return view;
		}
	}
}