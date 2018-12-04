using System;
using Android.Content;
using Android.Util;
using Android.Views;
using JObj = Java.Lang.Object;

namespace AppDevUnited.CannonGame.App
{
   public class CannonView : SurfaceView
   {
      public CannonView(Context context, IAttributeSet attrs)
         : base(context, attrs)
      {
      }

      public void StopGame()
      {
         throw new NotImplementedException();
      }

      public void ReleaseResources()
      {
         throw new NotImplementedException();
      }
   }
}