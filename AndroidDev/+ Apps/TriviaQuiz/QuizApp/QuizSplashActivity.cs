using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Res = QuizApp.Resource;

namespace QuizApp
{
   [Activity(Label = "@string/Splash", MainLauncher = true)]
   public class QuizSplashActivity : Activity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.Splash);
         StartAnimating();
      }

      protected override void OnResume()
      {
         base.OnResume();
         StartAnimating();
      }

      protected override void OnPause()
      {
         base.OnPause();

         var topLogo = FindViewById<TextView>(Resource.Id.TextViewTopTitle);
         topLogo.ClearAnimation();
         var bottomLogo = FindViewById<TextView>(Resource.Id.TextViewBottomTitle);
         bottomLogo.ClearAnimation();
         var table = FindViewById<TableLayout>(Resource.Id.TableLayout01);
         for (var i = 0; i < table.ChildCount; i++)
         {
            var childView = table.GetChildAt(i);
            childView.ClearAnimation();
         }
      }

      private void StartAnimating()
      {
         // Затемнение верхнего заголовка
         var topLogo = FindViewById<TextView>(Resource.Id.TextViewTopTitle);
         var firstFadeIn = AnimationUtils.LoadAnimation(this, Resource.Animation.FadeIn);
         topLogo.StartAnimation(firstFadeIn);

         // Затемнение нижнего заголовка после встроенной задержки
         var bottomLogo = FindViewById<TextView>(Resource.Id.TextViewBottomTitle);
         var secondFadeIn = AnimationUtils.LoadAnimation(this, Resource.Animation.FadeIn2);
         bottomLogo.StartAnimation(secondFadeIn);

         // Переход к главному меню, когда нижний заголовок завершит анимацию
         secondFadeIn.AnimationEnd += (sender, args) =>
         {
            // Анимация закончилась, переходим к экрану главного меню
            StartActivity(new Intent(this, typeof(QuizMenuActivity)));
            Finish();
         };

         // Загрузка анимаций для всех дочерних элементов табличного расположения
         var spinIn = AnimationUtils.LoadAnimation(this, Resource.Animation.CustomAnim);
         var controller = new LayoutAnimationController(spinIn) {Order = DelayOrder.Normal};
         var table = FindViewById<TableLayout>(Resource.Id.TableLayout01);
         for (var i = 0; i < table.ChildCount; i++)
            if (table.GetChildAt(i) is ViewGroup viewGroup)
               viewGroup.LayoutAnimation = controller;
      }
   }
}