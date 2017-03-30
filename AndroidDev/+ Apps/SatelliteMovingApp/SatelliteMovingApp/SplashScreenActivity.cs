using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views.Animations;
using Android.Widget;
using JetBrains.Annotations;
using static SatelliteMovingApp.Resource;
using Animation = Android.Views.Animations.Animation;

namespace SatelliteMovingApp
{
   /// <summary>
   ///    Активность для экрана заставки
   /// </summary>
   [Activity(Label = nameof(SplashScreenActivity), MainLauncher = true, NoHistory = true)]
   [UsedImplicitly]
   public class SplashScreenActivity : Activity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         var splashView = FindViewById(Layout.SplashScreen);
         SetContentView(splashView);
         RunAnimation();
      }

      protected override void OnResume()
      {
         base.OnResume();
         RunAnimation();
      }

      protected override void OnPause()
      {
         base.OnPause();

         var topTitle = FindViewById<TextView>(Id.SplashTopTitleTextView);
         topTitle.ClearAnimation();
         var bottomTitle = FindViewById<TextView>(Id.SplashBottomTitleTextView);
         bottomTitle.ClearAnimation();
         var tableLayout = FindViewById<TableLayout>(Id.TableLayoutId);
         for (var i = 0; i < tableLayout.ChildCount; i++)
         {
            var rowView = tableLayout.GetChildAt(i);
            rowView.ClearAnimation();
         }
      }

      private void RunAnimation()
      {
         var topTitle = FindViewById<TextView>(Id.SplashTopTitleTextView);
         var firstFadeIn = AnimationUtils.LoadAnimation(this, Resource.Animation.FadeIn);
         topTitle.StartAnimation(firstFadeIn);

         var bottomTitle = FindViewById<TextView>(Id.SplashBottomTitleTextView);
         var secondFadeIn = AnimationUtils.LoadAnimation(this, Resource.Animation.FadeInAfteDelay);
         bottomTitle.StartAnimation(secondFadeIn);

         secondFadeIn.SetAnimationListener(new AnimationListenerImpl(this));

         var spinIn = AnimationUtils.LoadAnimation(this, Resource.Animation.GradualApproach);
         var controller = new LayoutAnimationController(spinIn) {Order = DelayOrder.Normal};
         var tableLayout = FindViewById<TableLayout>(Id.TableLayoutId);
         for (var i = 0; i < tableLayout.ChildCount; i++)
         {
            var rowView = tableLayout.GetChildAt(i) as TableRow;
            if (rowView != null)
               rowView.LayoutAnimation = controller;
         }
      }

      private sealed class AnimationListenerImpl : Animation.IAnimationListener
      {
         private readonly Activity _callingActivity;

         public AnimationListenerImpl(Activity anActivity)
         {
            _callingActivity = anActivity;
         }

         public void Dispose()
         {
         }

         [UsedImplicitly]
         public IntPtr Handle { get; }

         public void OnAnimationEnd(Animation animation)
         {
            _callingActivity.StartActivity(new Intent(_callingActivity, typeof(MenuScreenActivity)));
            _callingActivity.Finish();
         }

         public void OnAnimationRepeat(Animation animation)
         {
         }

         public void OnAnimationStart(Animation animation)
         {
         }
      }
   }
}