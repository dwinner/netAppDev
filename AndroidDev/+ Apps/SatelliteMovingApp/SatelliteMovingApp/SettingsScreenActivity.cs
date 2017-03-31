using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using JetBrains.Annotations;
using SatelliteMovingApp.Lib.Model;
using SatelliteMovingApp.Lib.Utils;
using static SatelliteMovingApp.Lib.Factory.ToastHandlerFactory;
using Exception = System.Exception;
using Math = System.Math;
using String = Java.Lang.String;

namespace SatelliteMovingApp
{
   /// <summary>
   ///    Активность для экрана установок параметров спутников
   /// </summary>
   [Activity(Label = nameof(SettingsScreenActivity))]
   public class SettingsScreenActivity : Activity
   {
      internal const string SettingsFileName = "satellites.dat";
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

      /// <summary>
      ///    Установка кнопки сохранения данных таблицы
      /// </summary>
      private void SetupSaveButton()
      {
         var saveButton = FindViewById<Button>(Resource.Id.SaveButtonId);
         saveButton.SetOnClickListener(new SaveButtonClickListener(this));
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

      private void SaveSettings(string settingsFileName)
      {
         try
         {
            using (var fOs = OpenFileOutput(settingsFileName, FileCreationMode.Private))
            {
               var binaryFormatter = new BinaryFormatter();
               binaryFormatter.Serialize(fOs, _motionParams);
            }
         }
         catch (IOException ioEx)
         {
            Log.Error(GetType().Name, ioEx.Message, ioEx);
         }
      }

      /// <summary>
      ///    Проверка спутников на столкновения с землей и друг с другом
      /// </summary>
      /// <returns>true, если столкновений не будет, false в противном случае</returns>
      private bool VerifyDistances()
      {
         if (_motionParams.Count == 0)
            return true;

#pragma warning disable 618
         var earth = Resources.GetDrawable(Resource.Drawable.globe);
         var satellite = Resources.GetDrawable(Resource.Drawable.action_satellite);
#pragma warning restore 618
         var maxEarth = Math.Max(earth.IntrinsicWidth, earth.IntrinsicHeight);
         var maxSatellite = Math.Max(satellite.IntrinsicWidth, satellite.IntrinsicHeight);

         _motionParams.Sort(
            (satellite1, satellite2) =>
               Float.FloatToIntBits(satellite1.Distance) - Float.FloatToIntBits(satellite2.Distance));

         var satObject = _motionParams[0];
         if (satObject.Distance < Math.Sqrt(2) / 2 * (maxEarth + maxSatellite)) // Спутник столкнется с землей
         {
            var errorMessage = String.Format(Resources.GetString(Resource.String.EarthCollideError),
               Math.Sqrt(2) / 2 * (maxEarth + maxSatellite));
            AlertDialogUtil.ShowSimpleDialog(this, errorMessage);
            return false;
         }

         for (var i = 1; i < _motionParams.Count; i++)
         {
            var currentSatellite = _motionParams[i];
            var prevSatellite = _motionParams[i - 1];
            var diffDistance = currentSatellite.Distance - prevSatellite.Distance;
            if (diffDistance < MinimumDistanceBetween)
            {
               // Расстояние между спутниками меньше минимально допустимого
               var errorMessage = String.Format(Resources.GetString(Resource.String.SatelliteCollideError),
                  currentSatellite.Distance, prevSatellite.Distance, MinimumDistanceBetween);
               AlertDialogUtil.ShowSimpleDialog(this, errorMessage);
               return false;
            }
         }

         return true;
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

      private sealed class SaveButtonClickListener : View.IOnClickListener
      {
         private readonly SettingsScreenActivity _owner;

         public SaveButtonClickListener(SettingsScreenActivity owner)
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
            if (_owner._motionParams.Count > 0)
               _owner._motionParams.Clear();

            // Сбор информации из текстовых полей ввода
            nextTableRow:
            for (var i = 0; i < _owner._settingsTable.ChildCount; i++)
            {
               var tableRow = _owner._settingsTable.GetChildAt(i) as TableRow;
               if (tableRow == null)
                  continue;

               long currentTime = -1;
               float currentDistance = -1;

               for (var j = 0; j < tableRow.ChildCount; j++)
               {
                  var currentView = tableRow.GetChildAt(j);
                  if (!(currentView is EditText))
                     goto nextTableRow;

                  var editText = (EditText) currentView;
                  var editTextId = editText.Id;
                  var currentText = editText.Text.Trim();
                  if (currentText.Length == 0)
                     goto nextTableRow;

                  if (editTextId % 2 != 0)
                  {
                     if (!long.TryParse(currentText, out currentTime) || currentTime <= 0)
                     {
                        var errorMessage = _owner.Resources.GetString(Resource.String.NegativeTimeError);
                        AlertDialogUtil.ShowSimpleDialog(_owner, errorMessage);
                        editText.RequestFocus();
                        return;
                     }
                  }
                  else
                  {
                     if (!float.TryParse(currentText, out currentDistance) || !(currentDistance > 0))
                     {
                        var errorMessage = _owner.Resources.GetString(Resource.String.NegativeTimeError);
                        AlertDialogUtil.ShowSimpleDialog(_owner, errorMessage);
                        editText.RequestFocus();
                        return;
                     }
                  }
               }

               _owner._motionParams.Add(new Satellite(currentTime, currentDistance));
            }

            if (_owner.VerifyDistances())
               _owner.SaveSettings(SettingsFileName);
         }
      }

      #endregion
   }
}