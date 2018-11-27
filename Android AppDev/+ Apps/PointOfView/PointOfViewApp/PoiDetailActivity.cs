using Android.App;
using Android.OS;
using Android.Support.V7.App;
using PointOfViewApp.Utils;
using FragmentV4 = Android.Support.V4.App.Fragment;
using LayoutRes = PointOfViewApp.Resource.Layout;
using IdRes = PointOfViewApp.Resource.Id;

namespace PointOfViewApp
{
   [Activity(Label = "PoiDetailActivity")]
   public class PoiDetailActivity : AppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(LayoutRes.PoiDetail);

         FragmentV4 detailFragment = new PoiDetailFragment {Arguments = new Bundle()};
         if (Intent.HasExtra(IntentKeys.PoiDetailKey))
         {
            var poiJson = Intent.GetStringExtra(IntentKeys.PoiDetailKey);
            detailFragment.Arguments.PutString(IntentKeys.PoiDetailKey, poiJson);
         }

         var fragmentTransaction = SupportFragmentManager.BeginTransaction();
         fragmentTransaction.Add(IdRes.poiDetailLayout, detailFragment);
         fragmentTransaction.Commit();
      }
   }
}