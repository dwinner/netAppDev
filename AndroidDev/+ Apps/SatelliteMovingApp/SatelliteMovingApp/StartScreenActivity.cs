using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using SatelliteMovingApp.Lib.Model;
using SatelliteMovingApp.Lib.Utils;
using static SatelliteMovingApp.Lib.Factory.ViewAnimationFactory;

namespace SatelliteMovingApp
{
   /// <summary>
   ///    Активность для экрана вращения спутников вокруг земли
   /// </summary>
   [Activity(Label = nameof(StartScreenActivity))]
   public class StartScreenActivity : Activity
   {
      private SortedDictionary<Satellite, View> _satelliteMap;

      public override bool OnCreateOptionsMenu(IMenu menu)
      {
         base.OnCreateOptionsMenu(menu);

         MenuInflater.Inflate(Resource.Menu.NavigateOptions, menu);
         menu.FindItem(Resource.Id.StartMenuItemId).SetIntent(new Intent(this, typeof(StartScreenActivity)));
         menu.FindItem(Resource.Id.SettingsMenuItemId).SetIntent(new Intent(this, typeof(SettingsScreenActivity)));
         menu.FindItem(Resource.Id.AboutMenuItemId).SetIntent(new Intent(this, typeof(AboutScreenActivity)));

         return true;
      }

      public override bool OnOptionsItemSelected(IMenuItem item)
      {
         base.OnOptionsItemSelected(item);
         StartActivity(item.Intent);
         return true;
      }

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         var satellites = RetrieveCurrentSettings(SettingsScreenActivity.SettingsFileName);
         if (satellites.Count == 0)
         {
            SetContentView(Resource.Layout.StartScreen);
            return;
         }

         var satelliteCount = satellites.Count + 1;
         var views = CreateViewArray(satelliteCount);

         IComparer<Satellite> satelliteComparer = new SatelliteDistanceComparer();
         _satelliteMap = new SortedDictionary<Satellite, View>(satelliteComparer)
         {
            {new Satellite(), views[0]}
         };

         for (var i = 1; i < satelliteCount; i++)
            _satelliteMap.Add(satellites[i - 1], views[i]);

         ViewGroup orbitLayout = new OrbitLayout(_satelliteMap, this);
         orbitLayout.SetBackgroundResource(Resource.Drawable.background);
         SetContentView(orbitLayout);
         StartViewAnimation(_satelliteMap);
      }

      protected override void OnPause()
      {
         base.OnPause();

         if (_satelliteMap == null || _satelliteMap.Count == 0)
            return;

         var viewCollection = _satelliteMap.Values;
         foreach (var view in viewCollection)
            view.ClearAnimation();
      }

      protected override void OnResume()
      {
         base.OnResume();
         if (_satelliteMap != null && _satelliteMap.Count > 0)
            StartViewAnimation(_satelliteMap);
      }

      /// <summary>
      ///    Старт анимаций представления
      /// </summary>
      /// <param name="satelliteMap">Карта параметров анимации и представлений</param>
      private void StartViewAnimation(IDictionary<Satellite, View> satelliteMap)
      {
         if (satelliteMap == null || satelliteMap.Count == 0)
            return;

         foreach (var entryPair in satelliteMap.Skip(1))
         {
            var satellite = entryPair.Key;
            var view = entryPair.Value;
            view.StartAnimation(CreateOrbitalRotation(satellite.Distance, satellite.Angle, satellite.RoundingTime));
         }
      }

      /// <summary>
      ///    Создание массива представлений земли и спутников
      /// </summary>
      /// <param name="size">Размер массива</param>
      /// <returns>Массив представлений</returns>
      private View[] CreateViewArray(int size)
      {
         var views = new View[size];
         var earthImageView = new ImageView(this);
         earthImageView.SetImageResource(Resource.Drawable.globe);
         views[0] = earthImageView;

         for (var i = 1; i < size; i++)
         {
            var satelliteImageView = new ImageView(this);
            satelliteImageView.SetImageResource(Resource.Drawable.action_satellite);
            views[i] = satelliteImageView;
         }

         return views;
      }

      /// <summary>
      ///    Получение списка текущих параметров спутников
      /// </summary>
      /// <param name="settingsFileName">Имя файла с текущими параметрами</param>
      /// <returns>Список объектов Satellite</returns>
      private List<Satellite> RetrieveCurrentSettings(string settingsFileName)
      {
         try
         {
            using (var fIn = OpenFileInput(settingsFileName))
            {
               var xmlSerializer = new XmlSerializer(typeof(Satellite));
               var satellites = xmlSerializer.Deserialize(fIn) as List<Satellite>;
               if (satellites == null || satellites.Count == 0)
               {
                  var helpToast = Toast.MakeText(this, Resources.GetString(Resource.String.SatellitesHaveNot),
                     ToastLength.Long);
                  helpToast.SetGravity(GravityFlags.Center, 0, 0);
                  helpToast.Show();
               }

               return satellites ?? Enumerable.Empty<Satellite>().ToList();
            }
         }
         catch (Exception ex)
         {
            Log.Error(GetType().Name, ex.Message, ex);
            return Enumerable.Empty<Satellite>().ToList();
         }
      }

      private sealed class SatelliteDistanceComparer : IComparer<Satellite>
      {
         // ReSharper disable PossibleNullReferenceException
         public int Compare(Satellite x, Satellite y)
            => Float.FloatToIntBits(x.Distance) - Float.FloatToIntBits(y.Distance);

         // ReSharper restore PossibleNullReferenceException
      }
   }
}