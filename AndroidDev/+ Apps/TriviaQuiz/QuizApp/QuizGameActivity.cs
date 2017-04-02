using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using DroidRes = Android.Resource;
using static QuizApp.QuizPreferences;

namespace QuizApp
{
   [Activity(Label = "@string/quiz_game_title")]
   public class QuizGameActivity : Activity
   {
      private readonly IDictionary<int, Question> _questions = new Dictionary<int, Question>(QuestionBatchSize);
      private ISharedPreferences _gameSettings;
      private ImageSwitcher _questionImageSwitcher;
      private TextSwitcher _questionTextSwitcher;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.Game);

         // Обработка кнопки Yes
         var yesButton = FindViewById<Button>(Resource.Id.Button_Yes);
         yesButton.Click += (sender, args) => HandleAnswerAndShowNextQuestion(true);

         // Обработка кнопки No
         var noButton = FindViewById<Button>(Resource.Id.Button_No);
         noButton.Click += (sender, args) => HandleAnswerAndShowNextQuestion();

         // Извлечение общих настроек приложения
         _gameSettings = GetSharedPreferences(GamePreferences, FileCreationMode.Private);

         // Загрузка вопросов
         var startQuestionNumber = _gameSettings.GetInt(GamePreferencesCurrentQuestion, default(int));

         // Если мы в только начинаем викторину, инициализируем общие настройкм
         if (startQuestionNumber == 0)
         {
            var editor = _gameSettings.Edit();
            editor.PutInt(GamePreferencesCurrentQuestion, 1).Commit();
            startQuestionNumber = 1;
         }

         LoadQuestionBatch(startQuestionNumber);

         var droidFadeIn = AnimationUtils.LoadAnimation(this, DroidRes.Animation.FadeIn);
         var droidFadeOut = AnimationUtils.LoadAnimation(this, DroidRes.Animation.FadeOut);

         // Установка Text Switcher
         _questionTextSwitcher = FindViewById<TextSwitcher>(Resource.Id.TextSwitcher_QuestionText);
         _questionTextSwitcher.InAnimation = droidFadeIn;
         _questionTextSwitcher.OutAnimation = droidFadeOut;
         _questionTextSwitcher.SetFactory(new QuizGameTextSwitcherFactory(this));

         // Установка Image Switcher
         _questionImageSwitcher = FindViewById<ImageSwitcher>(Resource.Id.ImageSwitcher_QuestionImage);
         _questionImageSwitcher.InAnimation = droidFadeIn;
         _questionImageSwitcher.OutAnimation = droidFadeOut;
         _questionImageSwitcher.SetFactory(new QuizGameImageSwitcherFactory(this));

         if (_questions.ContainsKey(startQuestionNumber))
         {
            _questionTextSwitcher.SetCurrentText(GetQuestionText(startQuestionNumber));
            _questionImageSwitcher.SetImageDrawable(GetQuestionImage(startQuestionNumber));
         }
         else
         {
            HandleNoQuestions();
         }
      }

      public override bool OnCreateOptionsMenu(IMenu menu)
      {
         base.OnCreateOptionsMenu(menu);

         MenuInflater.Inflate(Resource.Menu.GameOptions, menu);
         menu.FindItem(Resource.Id.help_menu_item).SetIntent(new Intent(this, typeof(QuizHelpActivity)));
         menu.FindItem(Resource.Id.settings_menu_item).SetIntent(new Intent(this, typeof(QuizSettingsActivity)));

         return true;
      }

      public override bool OnOptionsItemSelected(IMenuItem item)
      {
         base.OnOptionsItemSelected(item);
         StartActivity(item.Intent);
         return true;
      }

      private void HandleNoQuestions()
      {
         throw new NotImplementedException();
      }

      private Drawable GetQuestionImage(int questionNumber)
      {
         throw new NotImplementedException();
      }

      private string GetQuestionText(int questionNumber)
      {
         throw new NotImplementedException();
      }

      private void LoadQuestionBatch(int questionNumber)
      {
         throw new NotImplementedException();
      }

      private void HandleAnswerAndShowNextQuestion(bool yesAnswer = false)
      {
         throw new NotImplementedException();
      }

      #region Internal types

      /// <summary>
      ///    Класс для управления данными вопроса викторины
      /// </summary>
      private sealed class Question
      {
         public Question(int number, string text, string imageUrl)
         {
            Number = number;
            Text = text;
            ImageUrl = imageUrl;
         }

         /// <summary>
         ///    The number of this question
         /// </summary>
         public int Number { get; set; }

         /// <summary>
         ///    The text for this question
         /// </summary>
         public string Text { get; set; }

         /// <summary>
         ///    A valid image Url to display with this question
         /// </summary>
         public string ImageUrl { get; set; }
      }

      /// <summary>
      ///    Фабрика переключателя для использования с изображением вопроса. Создает следующий объект для анимации
      /// </summary>
      private sealed class QuizGameTextSwitcherFactory : ViewSwitcher.IViewFactory
      {
         private readonly QuizGameActivity _quizGameActivity;

         public QuizGameTextSwitcherFactory(QuizGameActivity quizGameActivity)
         {
            _quizGameActivity = quizGameActivity;
         }

         public void Dispose()
         {
         }

         // ReSharper disable UnassignedGetOnlyAutoProperty
         public IntPtr Handle { get; }
         // ReSharper restore UnassignedGetOnlyAutoProperty

         public View MakeView()
         {
            var textView = new TextView(_quizGameActivity)
            {
               Gravity = GravityFlags.Center,
               TextSize = _quizGameActivity.Resources.GetDimension(Resource.Dimension.game_question_size)
            };
#pragma warning disable 618
            textView.SetTextColor(_quizGameActivity.Resources.GetColor(Resource.Color.title_color));
            textView.SetShadowLayer(10, 5, 5, _quizGameActivity.Resources.GetColor(Resource.Color.title_glow));
#pragma warning restore 618

            return textView;
         }
      }

      private sealed class QuizGameImageSwitcherFactory : ViewSwitcher.IViewFactory
      {
         private readonly QuizGameActivity _quizGameActivity;

         public QuizGameImageSwitcherFactory(QuizGameActivity quizGameActivity)
         {
            _quizGameActivity = quizGameActivity;
         }

         public void Dispose()
         {
         }

         // ReSharper disable UnassignedGetOnlyAutoProperty
         public IntPtr Handle { get; }
         // ReSharper restore UnassignedGetOnlyAutoProperty

         public View MakeView()
         {
            var imageView = new ImageView(_quizGameActivity)
            {
               LayoutParameters =
                  new FrameLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent)
            };
            imageView.SetScaleType(ImageView.ScaleType.FitCenter);

            return imageView;
         }
      }

      #endregion
   }
}