using System;
using System.Collections.Generic;
using System.Globalization;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Provider;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Text;
using Java.Util;
using Newtonsoft.Json;
using PointOfViewApp.Orm;
using PointOfViewApp.Poco;
using PointOfViewApp.Services;
using PointOfViewApp.Utils;
using Env = System.Environment;
using FragmentV4 = Android.Support.V4.App.Fragment;
using ResLayout = PointOfViewApp.Resource.Layout;
using IdRes = PointOfViewApp.Resource.Id;
using MenuRes = PointOfViewApp.Resource.Menu;
using JObj = Java.Lang.Object;
using AdrUri = Android.Net.Uri;
using Environment = Android.OS.Environment;
using File = System.IO.File;
using JFile = Java.IO.File;

// ReSharper disable AvoidAsyncVoid
// BUG: The usage of Toast raises unhandled exception in JVM runtime

namespace PointOfViewApp
{
   public class PoiDetailFragment : FragmentV4
   {
      private const string ProgressDialogTag = "progress_dialog";
      private const int CapturePhoto = 100; // TODO: Had better use enum instead
      private const string ApplicationAuthority = "AppDevUnited.PoiApp.FileProvider";

      private Activity _activity;
      private EditText _addressEditText;
      private string _currentPhotoPath;
      private EditText _descriptionEditText;
      private PointOfInterest _interest;
      private EditText _latitudeEditText;
      private ImageButton _locationImageButton;
      private ILocationListener _locationListener;
      private LocationManager _locationManager;
      private EditText _longitudeEditText;
      private ImageButton _mapImageButton;
      private EditText _nameEditText;
      private ImageButton _poiImageButton;
      private ImageView _poiImageView;

      public override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         _locationManager = (LocationManager) Activity.GetSystemService(Context.LocationService);
         _locationListener = new LocationListenerImpl(this);
         SetInterest();
      }

      private void SetInterest()
      {
         if (Arguments != null && Arguments.ContainsKey(IntentKeys.PoiDetailKey))
         {
            var poiJson = Arguments.GetString(IntentKeys.PoiDetailKey);
            _interest = JsonConvert.DeserializeObject<PointOfInterest>(poiJson);
         }
         else
         {
            _interest = new PointOfInterest();
         }
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
         _locationImageButton = view.FindViewById<ImageButton>(IdRes.locationImageButton);
         _locationImageButton.Click += OnGetLocation;
         _mapImageButton = view.FindViewById<ImageButton>(IdRes.mapImageButton);
         _mapImageButton.Click += OnGetMap;
         _poiImageView = view.FindViewById<ImageView>(IdRes.poiImageView);
         _poiImageButton = view.FindViewById<ImageButton>(IdRes.photoImageButton);
         _poiImageButton.Click += OnNewPhoto;

         HasOptionsMenu = true;
         SetInterest();
         UpdateUi();

         return view;
      }

      private void OnNewPhoto(object sender, EventArgs e)
      {
         if (_interest.Id <= 0)
         {
            Toast.MakeText(_activity, "You must save the POI before attaching a photo.", ToastLength.Short).Show();
            return;
         }
         
         var cameraIntent = new Intent(MediaStore.ActionImageCapture);
         if (cameraIntent.ResolveActivity(Activity.PackageManager) != null)
         {
            JFile photoFile;
            try
            {
               photoFile = CreateImageFile(_interest.Id);
            }
            catch (IOException)
            {
               Toast.MakeText(Context, "Photo file cannot be created, please try again", ToastLength.Long).Show();
               return;
            }

            if (photoFile != null)
            {
               var photoUri = FileProvider.GetUriForFile(Context, ApplicationAuthority, photoFile);
               cameraIntent.PutExtra(MediaStore.ExtraOutput, photoUri);
               cameraIntent.PutExtra(MediaStore.ExtraSizeLimit, 0x100000);
               StartActivityForResult(cameraIntent, CapturePhoto);
            }
         }
      }

      private JFile CreateImageFile(int poiId)
      {
         var timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").Format(new Date());
         var imageFileName = $"JPEG_{timeStamp}_{poiId}";
         var storageDir = Activity.GetExternalFilesDir(Environment.DirectoryPictures);
         var image = JFile.CreateTempFile(imageFileName, ".jpg", storageDir);
         _currentPhotoPath = image.AbsolutePath;

         return image;
      }

      private void OnGetMap(object sender, EventArgs e)
      {
         var geoUri = AdrUri.Parse(string.IsNullOrEmpty(_addressEditText.Text)
            ? $"geo:{_interest.Latitude},{_interest.Longitude}"
            : $"geo:0,0?q={_addressEditText.Text}");

         var mapIntent = new Intent(Intent.ActionView, geoUri);
         var packageManager = Activity.PackageManager;
         var activities = packageManager.QueryIntentActivities(mapIntent, 0);
         if (activities.Count == 0)
         {
            Toast.MakeText(_activity, "No map app available", ToastLength.Short).Show();
         }
         else
         {
            StartActivity(mapIntent);
         }
      }

      private void OnGetLocation(object sender, EventArgs e)
      {
         var criteria = new Criteria {Accuracy = Accuracy.NoRequirement, PowerRequirement = Power.NoRequirement};
         _locationManager.RequestSingleUpdate(criteria, _locationListener, null);
         var transaction = FragmentManager.BeginTransaction();
         var dialogFragment = new ProgressDialogFragment();
         dialogFragment.Show(transaction, ProgressDialogTag);
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

      public override void OnActivityResult(int requestCode, int resultCode, Intent data)
      {
         switch (requestCode)
         {
            case CapturePhoto:
               if (resultCode == (int) Result.Ok && !string.IsNullOrEmpty(_currentPhotoPath)
                                                 && File.Exists(_currentPhotoPath))
               {
                  var imageFile = new JFile(_currentPhotoPath);
                  using (var bitmap = BitmapFactory.DecodeFile(imageFile.Path))
                  {
                     _poiImageView.SetImageBitmap(bitmap);
                  }
               }
               else
               {
                  Toast.MakeText(Activity, "No picture captured.", ToastLength.Short).Show();
               }

               break;

            default:
               base.OnActivityResult(requestCode, resultCode, data);
               break;
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

         var response = await service.DeletePoiAsync(_interest.Id, _currentPhotoPath).ConfigureAwait(false);
         if (!string.IsNullOrEmpty(response))
         {
            var toast = Toast.MakeText(_activity, $"{_interest.Name} deleted", ToastLength.Short);
            toast.Show();
            SqLiteDbManager.Instance.Delete(_interest.Id);
            if (!PoiListActivity.IsDualMode)
            {
               _activity.Finish();
            }
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
         {
            _nameEditText.Error = null;
         }

         double? tempLatitude = null;
         if (!string.IsNullOrEmpty(_latitudeEditText.Text))
         {
            try
            {
               tempLatitude = double.Parse(_latitudeEditText.Text);
               if ((tempLatitude > 90) | (tempLatitude < -90))
               {
                  _latitudeEditText.Error = "Latitude must be a decimal value between -90 and 90";
                  errors = true;
               }
               else
               {
                  _latitudeEditText.Error = null;
               }
            }
            catch
            {
               _latitudeEditText.Error = "Latitude must be valid decimal number";
               errors = true;
            }
         }

         double? tempLongitude = null;
         if (!string.IsNullOrEmpty(_longitudeEditText.Text))
         {
            try
            {
               tempLongitude = double.Parse(_longitudeEditText.Text);
               if ((tempLongitude > 180) | (tempLongitude < -180))
               {
                  _longitudeEditText.Error = "Longitude must be a decimal value between -180 and 180";
                  errors = true;
               }
               else
               {
                  _longitudeEditText.Error = null;
               }
            }
            catch
            {
               _longitudeEditText.Error = "Longitude must be valid decimal number";
               errors = true;
            }
         }

         if (errors)
         {
            return;
         }

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
            if (!PoiListActivity.IsDualMode)
            {
               _activity.Finish();
            }
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

         // Create and show the dialog
         var bundle = new Bundle();
         bundle.PutString(IntentKeys.DeleteDialogTag, _interest.Name);
         var dialogFragment = new DeleteDialogFragment {Arguments = bundle};
         dialogFragment.SetTargetFragment(this, 0);

         // Add fragment
         dialogFragment.Show(transaction, "dialog");
      }

      private sealed class LocationListenerImpl : JObj, ILocationListener
      {
         private readonly PoiDetailFragment _fragment;

         public LocationListenerImpl(PoiDetailFragment fragment) => _fragment = fragment;

         public void OnLocationChanged(Location location)
         {
            // Remove progress dialog fragment            
            var transaction = _fragment.FragmentManager.BeginTransaction();
            var dialogFragment =
               (ProgressDialogFragment) _fragment.FragmentManager.FindFragmentByTag(ProgressDialogTag);
            if (dialogFragment != null)
            {
               transaction.Remove(dialogFragment).Commit();
            }

            _fragment._latitudeEditText.Text = location.Latitude.ToString(CultureInfo.CurrentCulture);
            _fragment._longitudeEditText.Text = location.Longitude.ToString(CultureInfo.CurrentCulture);

            IList<Address> addresses;
            using (var geocoder = new Geocoder(_fragment._activity))
            {
               addresses = geocoder.GetFromLocation(location.Latitude, location.Longitude, 5);
            }

            if (addresses.Count > 0)
            {
               UpdateAddressFields(addresses[0]);
            }
         }

         public void OnProviderDisabled(string provider)
         {
         }

         public void OnProviderEnabled(string provider)
         {
         }

         public void OnStatusChanged(string provider, Availability status, Bundle extras)
         {
         }

         private void UpdateAddressFields(Address address)
         {
            if (string.IsNullOrEmpty(_fragment._nameEditText.Text))
            {
               _fragment._nameEditText.Text = address.FeatureName;
            }

            if (string.IsNullOrEmpty(_fragment._addressEditText.Text))
            {
               for (var addrLineIdx = 0; addrLineIdx < address.MaxAddressLineIndex; addrLineIdx++)
               {
                  if (!string.IsNullOrEmpty(_fragment._addressEditText.Text))
                  {
                     _fragment._addressEditText.Text += Env.NewLine;
                  }

                  _fragment._addressEditText.Text += address.GetAddressLine(addrLineIdx);
               }
            }
         }
      }
   }
}