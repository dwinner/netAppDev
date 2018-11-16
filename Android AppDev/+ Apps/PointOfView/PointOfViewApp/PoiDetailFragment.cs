using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using PointOfViewApp.Orm;
using PointOfViewApp.Poco;
using PointOfViewApp.Services;
using PointOfViewApp.Utils;
using FragmentV4 = Android.Support.V4.App.Fragment;
using ResLayout = PointOfViewApp.Resource.Layout;
using IdRes = PointOfViewApp.Resource.Id;
using MenuRes = PointOfViewApp.Resource.Menu;

// ReSharper disable AvoidAsyncVoid
// BUG: The usage of Toast raises unhandled exception in JVM runtime
namespace PointOfViewApp
{
   public class PoiDetailFragment : FragmentV4
   {
      private Activity _activity;
      private EditText _addressEditText;
      private EditText _descriptionEditText;
      private PointOfInterest _interest;
      private EditText _latitudeEditText;
      private EditText _longitudeEditText;
      private EditText _nameEditText;

      public override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         if (Arguments != null && Arguments.ContainsKey(IntentKeys.PoiDetailKey))
         {
            var poiJson = Arguments.GetString(IntentKeys.PoiDetailKey);
            _interest = JsonConvert.DeserializeObject<PointOfInterest>(poiJson);
         }
         else
            _interest = new PointOfInterest();
      }

      public override void OnAttach(Context context)
      {
         base.OnAttach(context);
         _activity = (Activity) context;
      }

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
         var view = inflater.Inflate(ResLayout.PoiDetailFragment, container, false);

         // Initialize widgets
         _nameEditText = view.FindViewById<EditText>(IdRes.nameEditText);
         _descriptionEditText = view.FindViewById<EditText>(IdRes.descriptionEditText);
         _addressEditText = view.FindViewById<EditText>(IdRes.addressEditText);
         _latitudeEditText = view.FindViewById<EditText>(IdRes.latitudeEditText);
         _longitudeEditText = view.FindViewById<EditText>(IdRes.longitudeEditText);

         UpdateUi();
         HasOptionsMenu = true;

         return view;
      }

      public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
      {
         inflater.Inflate(MenuRes.poi_detail_menu, menu);
         base.OnCreateOptionsMenu(menu, inflater);
      }

      public override bool OnOptionsItemSelected(IMenuItem item)
      {
         switch (item.ItemId)
         {
            case IdRes.actionSave:
               SavePoi();
               return true;

            case IdRes.actionDelete:
               DeletePoi();
               return true;

            default:
               return base.OnOptionsItemSelected(item);
         }
      }

      public override void OnPrepareOptionsMenu(IMenu menu)
      {
         base.OnPrepareOptionsMenu(menu);

         if (_interest.Id <= 0) // Disable delete for a new POI
         {
            var deleteItem = menu.FindItem(IdRes.actionDelete);
            deleteItem.SetEnabled(false);
            deleteItem.SetVisible(false);
         }
      }

      private void UpdateUi() // Update set up UI
      {
         _nameEditText.Text = _interest.Name;
         _descriptionEditText.Text = _interest.Description;
         _addressEditText.Text = _interest.Address;
         _latitudeEditText.Text = _interest.Latitude.ToString();
         _longitudeEditText.Text = _interest.Longitude.ToString();
      }

      internal async void DeletePoiAsync()
      {
         var service = new PoiService();
         if (!ConnectionUtils.IsConnected(_activity))
         {
            var toast = Toast.MakeText(_activity,
               "Not connected to internet. Please check your device network settings.",
               ToastLength.Short);
            toast.Show();
            return;
         }

         var response = await service.DeletePoiAsync(_interest.Id).ConfigureAwait(false);
         if (!string.IsNullOrEmpty(response))
         {
            var toast = Toast.MakeText(_activity, $"{_interest.Name} deleted", ToastLength.Short);
            toast.Show();
            SqLiteDbManager.Instance.Delete(_interest.Id);
            if (!PoiListActivity.IsDualMode) _activity.Finish();
         }
         else
         {
            var toast = Toast.MakeText(_activity, "Something went wrong!", ToastLength.Short);
            toast.Show();
         }
      }

      private void SavePoi()
      {
         var errors = false;
         if (string.IsNullOrEmpty(_nameEditText.Text))
         {
            _nameEditText.Error = "Name cannot be empty";
            errors = true;
         }
         else
            _nameEditText.Error = null;


         double? tempLatitude = null;
         if (!string.IsNullOrEmpty(_latitudeEditText.Text))
            try
            {
               tempLatitude = double.Parse(_latitudeEditText.Text);
               if ((tempLatitude > 90) | (tempLatitude < -90))
               {
                  _latitudeEditText.Error = "Latitude must be a decimal value between -90 and 90";
                  errors = true;
               }
               else
                  _latitudeEditText.Error = null;
            }
            catch
            {
               _latitudeEditText.Error = "Latitude must be valid decimal number";
               errors = true;
            }

         double? tempLongitude = null;
         if (!string.IsNullOrEmpty(_longitudeEditText.Text))
            try
            {
               tempLongitude = double.Parse(_longitudeEditText.Text);
               if ((tempLongitude > 180) | (tempLongitude < -180))
               {
                  _longitudeEditText.Error = "Longitude must be a decimal value between -180 and 180";
                  errors = true;
               }
               else
                  _longitudeEditText.Error = null;
            }
            catch
            {
               _longitudeEditText.Error = "Longitude must be valid decimal number";
               errors = true;
            }

         if (errors) return;

         _interest.Name = _nameEditText.Text;
         _interest.Description = _descriptionEditText.Text;
         _interest.Address = _addressEditText.Text;
         _interest.Latitude = tempLatitude;
         _interest.Longitude = tempLongitude;

         CreateOrUpdatePoiAsync();
      }

      private async void CreateOrUpdatePoiAsync()
      {
         var service = new PoiService();
         if (!ConnectionUtils.IsConnected(_activity))
         {
            var toast = Toast.MakeText(_activity,
               "Not connected to internet. Please check your device network settings.",
               ToastLength.Short);
            toast.Show();
            return;
         }

         var response = await service.CreateOrUpdateAsync(_interest).ConfigureAwait(false);
         if (!string.IsNullOrEmpty(response))
         {
            var toast = Toast.MakeText(_activity, $"{_interest.Name} saved.", ToastLength.Short);
            toast.Show();
            SqLiteDbManager.Instance.Save(_interest);
            if (!PoiListActivity.IsDualMode) _activity.Finish();
         }
         else
         {
            var toast = Toast.MakeText(_activity, "Something went Wrong!", ToastLength.Short);
            toast.Show();
         }
      }

      private void DeletePoi()
      {
         var transaction = FragmentManager.BeginTransaction();
         var bundle = new Bundle();
         bundle.PutString("name", _interest.Name);
         var dialogFragment = new DeleteDialogFragment {Arguments = bundle};
         dialogFragment.SetTargetFragment(this, 0);
         dialogFragment.Show(transaction, "dialog");
      }
   }
}