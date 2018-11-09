using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using PointOfViewApp.Poco;
using PointOfViewApp.Services;
using PointOfViewApp.Utils;
using LayoutRes = PointOfViewApp.Resource.Layout;
using IdRes = PointOfViewApp.Resource.Id;
using MenuRes = PointOfViewApp.Resource.Menu;

// ReSharper disable AvoidAsyncVoid

namespace PointOfViewApp
{
   [Activity(Label = "PoiDetailActivity")]
   public class PoiDetailActivity : Activity
   {
      private EditText _addressEditText;
      private EditText _descriptionEditText;
      private EditText _latitudeEditText;
      private EditText _longitudeEditText;
      private EditText _nameEditText;
      private PointOfInterest _poi;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(LayoutRes.PoiDetail);

         // Initialize widgets
         _nameEditText = FindViewById<EditText>(IdRes.nameEditText);
         _descriptionEditText = FindViewById<EditText>(IdRes.descriptionEditText);
         _addressEditText = FindViewById<EditText>(IdRes.addressEditText);
         _latitudeEditText = FindViewById<EditText>(IdRes.latitudeEditText);
         _longitudeEditText = FindViewById<EditText>(IdRes.longitudeEditText);

         _poi = RetrievePoi();
         UpdateUi();
      }

      public override bool OnCreateOptionsMenu(IMenu menu)
      {
         MenuInflater.Inflate(MenuRes.poi_detail_menu, menu);
         return base.OnCreateOptionsMenu(menu);
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

      public override bool OnPrepareOptionsMenu(IMenu menu)
      {
         base.OnPrepareOptionsMenu(menu);

         // Disable delete for a new POI
         if (_poi.Id >= 0)
         {
            var deleteItem = menu.FindItem(IdRes.actionDelete);
            deleteItem.SetEnabled(false);
            deleteItem.SetVisible(false);
         }

         return true;
      }

      private void SavePoi()
      {
         // TODO: Use attributes for model validation
         // TODO: Localaize strings
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

         _poi.Name = _nameEditText.Text;
         _poi.Description = _descriptionEditText.Text;
         _poi.Address = _addressEditText.Text;
         _poi.Latitude = tempLatitude;
         _poi.Longitude = tempLongitude;

         CreateOrUpdatePoiAsync();
      }

      private PointOfInterest RetrievePoi() // Retrieve intent data
      {
         PointOfInterest poi;

         if (Intent.HasExtra(IntentKeys.PoiDetailKey))
         {
            var poiJson = Intent.GetStringExtra(IntentKeys.PoiDetailKey);
            poi = JsonConvert.DeserializeObject<PointOfInterest>(poiJson);
         }
         else
            poi = new PointOfInterest();

         return poi;
      }

      private void UpdateUi() // Update set up UI
      {
         _nameEditText.Text = _poi.Name;
         _descriptionEditText.Text = _poi.Description;
         _addressEditText.Text = _poi.Address;
         _latitudeEditText.Text = _poi.Latitude.ToString();
         _longitudeEditText.Text = _poi.Longitude.ToString();
      }

      private void ConfirmDelete(object sender, EventArgs e) => DeletePoiAsync();

      private async void DeletePoiAsync()
      {
         var service = new PoiService();
         if (!ConnectionUtils.IsConnected(this))
         {
            // TODO: localize
            var toast = Toast.MakeText(this, "Not connected to internet. Please check your device network settings.",
               ToastLength.Short);
            toast.Show();
            return;
         }

         var response = await service.DeletePoiAsync(_poi.Id).ConfigureAwait(false);
         if (!string.IsNullOrEmpty(response))
         {
            // TODO: localize
            var toast = Toast.MakeText(this, $"{_poi.Name} deleted", ToastLength.Short);
            toast.Show();
            Finish();
         }
         else
         {
            // TODO: localize
            var toast = Toast.MakeText(this, "Something went wrong!", ToastLength.Short);
            toast.Show();
         }
      }

      private async void CreateOrUpdatePoiAsync()
      {
         // TODO: localize
         var service = new PoiService();
         if (!ConnectionUtils.IsConnected(this))
         {
            var toast = Toast.MakeText(this, "Not connected to internet. Please check your device network settings.",
               ToastLength.Short);
            toast.Show();
            return;
         }

         var response = await service.CreateOrUpdateAsync(_poi).ConfigureAwait(false);
         if (!string.IsNullOrEmpty(response))
         {
            var toast = Toast.MakeText(this, $"{_poi.Name} saved.", ToastLength.Short);
            toast.Show();
            Finish();
         }
         else
         {
            var toast = Toast.MakeText(this, "Something went Wrong!", ToastLength.Short);
            toast.Show();
         }
      }

      // TODO: localize
      private void DeletePoi() => new AlertDialog.Builder(this)
         .SetTitle("Confirm delete")
         .SetCancelable(false)
         .SetPositiveButton("OK", ConfirmDelete)
         .SetNegativeButton("Cancel", (sender, args) => { })
         .SetMessage($"Are you sure you want to delete {_poi.Name}?")
         .Create()
         .Show();
   }
}