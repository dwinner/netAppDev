using System;
using Android.App;
using Android.Views;
using Android.Widget;
using JetBrains.Annotations;

namespace SatelliteMovingApp.Lib.Factory
{
   /// <summary>
   ///    Фабрика обработчиков для генерации всплывающих сообщений
   /// </summary>
   public static class ToastHandlerFactory
   {
      /// <summary>
      ///    Метод-генератор интерфейса для методов обратного вызова всплывающих сообщений в центре экрана
      /// </summary>
      /// <param name="anActivity">Активность-собственник</param>
      /// <param name="aHelpText">Текст сообщения</param>
      /// <returns>Тост по центру</returns>
      public static View.IOnClickListener CreateCenterToast(Activity anActivity, string aHelpText)
         => new ToastClickListenerImpl(anActivity, aHelpText);

      private sealed class ToastClickListenerImpl : View.IOnClickListener
      {
         private readonly string _helpText;
         private readonly Activity _ownerActivity;

         public ToastClickListenerImpl(Activity ownerActivity, string helpText)
         {
            _ownerActivity = ownerActivity;
            _helpText = helpText;
         }

         public void Dispose()
         {
         }

         public void OnClick(View v)
         {
            var helpToast = Toast.MakeText(_ownerActivity, _helpText, ToastLength.Long);
            helpToast.SetGravity(GravityFlags.Center, 0, 0);
            helpToast.Show();
         }

         [UsedImplicitly]
         public IntPtr Handle { get; }
      }
   }
}