using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using JetBrains.Annotations;
using SatelliteMovingApp.Lib.Model;
using static SatelliteMovingApp.Lib.Factory.ToastHandlerFactory;

namespace SatelliteMovingApp
{
   /// <summary>
   ///    Активность для экрана установок параметров спутников
   /// </summary>
   [Activity(Label = nameof(SettingsScreenActivity))]
   public class SettingsScreenActivity : Activity
   {
      private const string SettingsFileName = "satellites.dat";
      private const float MinimumDistanceBetween = 5F;
      private int _currentRecordId = -1;
      private List<Satellite> _motionParams;
      private TableLayout _settingsTable;

      public override bool OnCreateOptionsMenu(IMenu menu)
      {
         base.OnCreateOptionsMenu(menu);

         MenuInflater.Inflate(Resource.Menu.NavigateOptions, menu);
         menu.FindItem(Resource.Id.StartMenuItemId)
            .SetIntent(new Intent(this, typeof(StartScreenActivity)));
         menu.FindItem(Resource.Id.SettingsMenuItemId)
            .SetIntent(new Intent(this, typeof(SettingsScreenActivity)));
         menu.FindItem(Resource.Id.AboutMenuItemId)
            .SetIntent(new Intent(this, typeof(AboutScreenActivity)));

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

         SetContentView(Resource.Layout.SettingsScreen);
         _motionParams = new List<Satellite>(0x40);
         _settingsTable = FindViewById<TableLayout>(Resource.Id.SatellitesInfoTableLayoutId);
         SetupHeaderRows();
         RetrieveSettings(SettingsFileName);
         SetupAddButton();
         SetupDeleteButton();
         SetupSaveButton();
      }

      private void SetupSaveButton()
      {
         throw new NotImplementedException();
      }

      /// <summary>
      ///    Установка кнопки удаления строки из таблицы
      /// </summary>
      private void SetupDeleteButton()
      {
         var deleteButton = FindViewById<Button>(Resource.Id.DeleteButtonId);
         deleteButton.SetOnClickListener(new DelButtonClickListener(this));
      }

      /// <summary>
      ///    Установка кнопки добавления строки таблицы
      /// </summary>
      private void SetupAddButton()
      {
         var addButton = FindViewById<Button>(Resource.Id.AddButtonId);
         var deleteButton = FindViewById<Button>(Resource.Id.DeleteButtonId);
         deleteButton.Enabled = _currentRecordId != -1;
         addButton.SetOnClickListener(new AddButtonClickListener(this));
      }

      /// <summary>
      ///    Получение текущих установок для спутников
      /// </summary>
      /// <param name="settingsFileName">Файл настроек</param>
      private void RetrieveSettings(string settingsFileName)
      {
         try
         {
            using (var fIn = OpenFileInput(settingsFileName))
            {
               var binaryFormatter = new BinaryFormatter();
               var satellites = binaryFormatter.Deserialize(fIn) as List<Satellite>;
               if (satellites == null || satellites.Count == 0)
                  return;

               _motionParams.AddRange(satellites);
               _motionParams.ForEach(satellite =>
               {
                  var roundingTime = satellite.RoundingTime;
                  using (var tableRow = new TableRow(this))
                  {
                     int nextId;

                     using (var speedText = new EditText(this)
                     {
                        InputType = InputTypes.ClassNumber,
                        Text = roundingTime.ToString(),
                        Id = UniqueIdGen.NextId
                     })
                     {
                        speedText.SetMaxLines(1);
                        _currentRecordId = speedText.Id;
                        nextId = speedText.Id + 1;
                        tableRow.AddView(speedText);
                     }

                     using (var distanceText = new EditText(this)
                     {
                        InputType = InputTypes.ClassNumber | InputTypes.NumberFlagDecimal,
                        Text = satellite.Distance.ToString(CultureInfo.CurrentCulture),
                        Id = nextId
                     })
                     {
                        distanceText.SetMaxLines(1);
                        tableRow.AddView(distanceText);
                     }

                     _settingsTable.AddView(tableRow);
                  }
               });
            }
         }
         catch (Exception ex)
         {
            Log.Info(GetType().Name, ex.Message, ex);
         }
      }

      /// <summary>
      ///    Установка заголовков таблицы
      /// </summary>
      private void SetupHeaderRows()
      {
         using (var headerRow = new TableRow(this))
         {
            // Заголовок таблицы для скорости
            using (
               var speedAboutButton = new Button(this) {Text = Resources.GetString(Resource.String.SpeedTableHeader)})
            {
               var speedHelpText = Resources.GetString(Resource.String.SpeedHelpText);
               speedAboutButton.SetOnClickListener(CreateCenterToast(this, speedHelpText));
               headerRow.AddView(speedAboutButton);
            }

            // Заголовок таблицы для расстояния
            using (
               var distanceAboutButton = new Button(this)
               {
                  Text = Resources.GetString(Resource.String.DistanceTableHeader)
               })
            {
               var distanceHelpText = Resources.GetString(Resource.String.DistanceHelpText);
               distanceAboutButton.SetOnClickListener(CreateCenterToast(this, distanceHelpText));
               headerRow.AddView(distanceAboutButton);
            }

            _settingsTable.AddView(headerRow);
         }
      }

      #region Internal types

      private static class UniqueIdGen
      {
         private static int _currentId = -1;

         internal static int NextId => _currentId += 2;

         internal static int PrevId => _currentId -= 2;
      }

      private sealed class AddButtonClickListener : View.IOnClickListener
      {
         private readonly SettingsScreenActivity _owner;

         public AddButtonClickListener(SettingsScreenActivity owner)
         {
            _owner = owner;
         }

         public void Dispose()
         {
         }

         [UsedImplicitly]
         public IntPtr Handle { get; }

         public void OnClick(View view)
         {
            var deleteButton = _owner.FindViewById<Button>(Resource.Id.DeleteButtonId);
            deleteButton.Enabled = _owner._currentRecordId != -1;

            using (var row = new TableRow(_owner))
            {
               int nextId;
               using (
                  var speedEditText = new EditText(_owner)
                  {
                     InputType = InputTypes.ClassNumber,
                     Hint = _owner.Resources.GetString(Resource.String.SpeedHint),
                     Id = UniqueIdGen.NextId
                  })
               {
                  speedEditText.SetMaxLines(1);
                  _owner._currentRecordId = speedEditText.Id;
                  nextId = speedEditText.Id + 1;
                  row.AddView(speedEditText);
               }

               using (
                  var distanceEditText = new EditText(_owner)
                  {
                     InputType = InputTypes.ClassNumber | InputTypes.NumberFlagDecimal,
                     Hint = _owner.Resources.GetString(Resource.String.DistanceHint),
                     Id = nextId
                  })
               {
                  distanceEditText.SetMaxLines(1);
                  row.AddView(distanceEditText);
               }

               _owner._settingsTable.AddView(row);
            }
         }
      }

      private sealed class DelButtonClickListener : View.IOnClickListener
      {
         private readonly SettingsScreenActivity _owner;

         public DelButtonClickListener(SettingsScreenActivity owner)
         {
            _owner = owner;
         }

         public void Dispose()
         {
         }

         [UsedImplicitly]
         public IntPtr Handle { get; }

         public void OnClick(View v)
         {
            var deleteButton = _owner.FindViewById<Button>(Resource.Id.DeleteButtonId);
            if (_owner._currentRecordId == -1)
            {
               deleteButton.Enabled = false;
            }
            else
            {
               _owner._settingsTable.RemoveViewAt(_owner._settingsTable.ChildCount - 1);
               _owner._currentRecordId = UniqueIdGen.PrevId;
            }
         }
      }

      #endregion
   }
}