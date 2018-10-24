using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android;
using Android.Content;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using FragmentV4=Android.Support.V4.App.Fragment;
using R=Doodlz.App.Resource;
using JavaObj = Java.Lang.Object;

namespace Doodlz.App
{
   /// <summary>
   /// Фрагмент, в котором отображается DoodleView
   /// </summary>
   public class MainActivityFragment : FragmentV4
   {
      private DoodleView _doodleView;  // Обработка событий касания и рисования
      private float _acceleration;
      private float _currentAcceleration;
      private float _lastAcceleration;
      private bool _dialogOnScreen = false;
      private ISensorEventListener _sensorEventListener;
      private const int AccelerationThreshold = 100000;  // Используется для обнаружения встряхивания устройства
      private const int SaveImagePermissionRequestCode = 1; // Используется для идентификации запросов на использование внещнего хранилища; необходимо для работы функций сохранения      

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {       
         base.OnCreateView(inflater, container, savedInstanceState);

         var view = inflater.Inflate(R.Layout.fragment_main,container,false);
         HasOptionsMenu = true;  // У фрагмента имеются команды меню
         _doodleView = view.FindViewById<DoodleView>(R.Id.doodleView);  // Получение ссылки на DoodleView

         // Инициализация параметров ускорения
         _acceleration = 0.00f;
         _currentAcceleration = SensorManager.GravityEarth;
         _lastAcceleration = SensorManager.GravityEarth;
         _sensorEventListener = new DefaultSensorEventListener(this);

         return view;
      }

      public override void OnResume()
      {
         base.OnResume();
         EnableAccelerometerListening();  // Прослушивание событий встряхивания
      }

      public override void OnPause()
      {
         base.OnPause();
         DisableAccelerometerListening(); // Прекращения прослушивания
      }      

      private void EnableAccelerometerListening()  // Включение прослушивания событий акселерометра
      {         
         var sensorManager = (SensorManager) Activity.GetSystemService(Context.SensorService);  // Получение объекта SensorManager
         sensorManager.RegisterListener(
            _sensorEventListener, sensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Normal);  // Регистрация для прослушивания событий акселерометра
      }

      private void DisableAccelerometerListening()
      {
         var sensorManager = (SensorManager)Activity.GetSystemService(Context.SensorService);  // Получение объекта SensorManager
         sensorManager.UnregisterListener(_sensorEventListener, sensorManager.GetDefaultSensor(SensorType.Accelerometer)); // Прекращение прослушивания событий акселерометра
      }

      private void ConfirmErase()   // Подтверждение стирания рисунка
      {
         EraseImageDialogFragment fragment=new EraseImageDialogFragment();
         fragment.Show(FragmentManager, "Erase Dialog"); // TODO: Move To String resource
      }

      /// <summary>
      /// Обработчик для событий акселерометра
      /// </summary>
      private sealed class DefaultSensorEventListener : JavaObj, ISensorEventListener
      {
         private readonly MainActivityFragment _fragment;

         public DefaultSensorEventListener(MainActivityFragment mainActivityFragment) => _fragment = mainActivityFragment;

         public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
         {            
         }

         public void OnSensorChanged(SensorEvent e)   // Проверка встряхивания по показаниям акселерометра
         {
            // На экране не должно быть других диалоговых окон
            if (_fragment._dialogOnScreen)
               return;

            // Получить значения x, y, z для SensorEvent
            var x = e.Values[0];
            var y = e.Values[1];
            var z = e.Values[2];

            // Сохранить предыдущие данные ускорения
            _fragment._lastAcceleration = _fragment._currentAcceleration;

            // Вычислить текущее ускорение
            _fragment._currentAcceleration = x * x + y * y + z * z;

            // Вычислить изменение ускорения
            _fragment._acceleration = _fragment._currentAcceleration *
                                      (_fragment._currentAcceleration - _fragment._lastAcceleration);

            // Если изменение превышает заданный порог, то вызвать стирание
            if (_fragment._acceleration>AccelerationThreshold)
            {
               _fragment.ConfirmErase();
            }
         }
      }
   }
}