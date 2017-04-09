using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
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
      private Button _noButton;
      private ImageSwitcher _questionImageSwitcher;
      private TextSwitcher _questionTextSwitcher;
      private Button _yesButton;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.Game);

         // Обработка кнопки Yes
         _yesButton = FindViewById<Button>(Resource.Id.Button_Yes);
         _yesButton.Click += (sender, args) => HandleAnswerAndShowNextQuestion(true);

         // Обработка кнопки No
         _noButton = FindViewById<Button>(Resource.Id.Button_No);
         _noButton.Click += (sender, args) => HandleAnswerAndShowNextQuestion();

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
         // TODO: FIX THE _questionTextSwitcher.SetFactory(new QuizGameTextSwitcherFactory(this));         

         // Установка Image Switcher
         _questionImageSwitcher = FindViewById<ImageSwitcher>(Resource.Id.ImageSwitcher_QuestionImage);
         _questionImageSwitcher.InAnimation = droidFadeIn;
         _questionImageSwitcher.OutAnimation = droidFadeOut;
         // TODO: FIX THE _questionImageSwitcher.SetFactory(new QuizGameImageSwitcherFactory(this));

         if (_questions.ContainsKey(startQuestionNumber))
         {
            var questionText = GetQuestionText(startQuestionNumber);            
            _questionTextSwitcher.SetCurrentText(questionText);
            var questionImage = GetQuestionImage(startQuestionNumber);
            _questionImageSwitcher.SetImageDrawable(questionImage);
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

      /// <summary>
      ///    Вспомогательный метод, отвечающий за ситуацию отсутствия вопросов
      /// </summary>
      private void HandleNoQuestions()
      {
         _questionTextSwitcher?.SetText(Resources.GetText(Resource.String.no_questions));
         _questionImageSwitcher?.SetImageResource(Resource.Drawable.NoQuestion);
         _yesButton.Enabled = false;
         _noButton.Enabled = false;
      }

      /// <summary>
      ///    Извлекает объект Drawable для вопроса
      /// </summary>
      /// <param name="questionNumber">Номер вопроса</param>
      /// <returns>Объект Drawable</returns>
      private Drawable GetQuestionImage(int questionNumber)
      {
         Drawable image;

         try
         {
            using (var webClient = new WebClient())
            {
               var imageUri = new Uri(GetQuestionImageUri(questionNumber));
               var imageData = webClient.DownloadData(imageUri);
               using (var stream = new MemoryStream(imageData))
               {
                  var bitmap = BitmapFactory.DecodeStream(stream);
                  image = new BitmapDrawable(bitmap);
               }
            }
         }
         catch (FormatException formatException)
         {
            Log.Error(GetType().Name, formatException.Message, formatException);
            throw;
         }
         catch (Exception ex)
         {
            Log.Error(GetType().Name, ex.Message, "Maybe decoding Bitmap stream failed");
#pragma warning disable 618
            image = Resources.GetDrawable(Resource.Drawable.NoQuestion);
#pragma warning restore 618
         }

         return image;
      }

      /// <summary>
      ///    Возвращает строку Uri для изображения по номеру вопроса
      /// </summary>
      /// <param name="questionNumber">Номер вопроса</param>
      /// <returns>Uri-изображения</returns>
      private string GetQuestionImageUri(int questionNumber)
         =>
            _questions.TryGetValue(questionNumber, out Question currentQuestion)
               ? currentQuestion.ImageUri
               : string.Empty;

      /// <summary>
      ///    Возвращает строку с текстом вопроса по его номеру
      /// </summary>
      /// <param name="questionNumber">Номер вопроса</param>
      /// <returns>Текст вопроса</returns>
      private string GetQuestionText(int questionNumber)
         => _questions.TryGetValue(questionNumber, out Question currentQuestion) ? currentQuestion.Text : string.Empty;

      /// <summary>
      ///    Загрузка XML в поле класса
      /// </summary>
      /// <param name="startQuestionNumber">Номер вопроса</param>
      private void LoadQuestionBatch(int startQuestionNumber)
      {
         _questions.Clear();
         using (
            var questionsXmlReader = startQuestionNumber < 16
               ? Resources.GetXml(Resource.Xml.SampleQuestions)
               : Resources.GetXml(Resource.Xml.SampleQuestions2))
         {
            // Ищем записи для score из XML
            while (questionsXmlReader.Read())
            {
               if (questionsXmlReader.NodeType != XmlNodeType.Element)
                  continue;

               // Получаем имя тэга
               var tagName = questionsXmlReader.Name;
               if (tagName == XmlTagQuestion)
               {
                  var questionNumber =
                     int.Parse(questionsXmlReader.MoveToAttribute(XmlTagQuestionAttributeNumber)
                        ? questionsXmlReader.Value
                        : "0");
                  var questionText = questionsXmlReader.MoveToAttribute(XmlTagQuestionAttributeText)
                     ? questionsXmlReader.Value
                     : string.Empty;
                  var questionImageUri = questionsXmlReader.MoveToAttribute(XmlTagQuestionAttributeImageurl)
                     ? questionsXmlReader.Value
                     : string.Empty;
                  _questions.Add(questionNumber, new Question(questionText, questionImageUri));
               }
            }
         }
      }

      /// <summary>
      ///    Вспомогательный метод для записи ответа и загрузки нового вопроса
      /// </summary>
      /// <param name="yesAnswer">true, если пользователь дал положительный ответ, false в противном случае</param>
      private void HandleAnswerAndShowNextQuestion(bool yesAnswer = false)
      {
         var currentScore = _gameSettings.GetInt(GamePreferencesScore, default(int));
         var nextQuestionNumber = _gameSettings.GetInt(GamePreferencesCurrentQuestion, 1) + 1;
         var editor = _gameSettings.Edit();
         editor.PutInt(GamePreferencesCurrentQuestion, nextQuestionNumber);

         // Учитываем ответ в случае Yes
         if (yesAnswer)
            editor.PutInt(GamePreferencesScore, currentScore + 1);

         editor.Commit();

         if (!_questions.ContainsKey(nextQuestionNumber))
            LoadQuestionBatch(nextQuestionNumber);

         if (_questions.ContainsKey(nextQuestionNumber))
         {
            _questionTextSwitcher?.SetText(GetQuestionText(nextQuestionNumber));
            _questionImageSwitcher?.SetImageDrawable(GetQuestionImage(nextQuestionNumber));
         }
         else
         {
            HandleNoQuestions();
         }
      }

      #region Internal types

      /// <summary>
      ///    Класс для управления данными вопроса викторины
      /// </summary>
      private sealed class Question
      {
         public Question(string text, string imageUri)
         {
            Text = text;
            ImageUri = imageUri;
         }

         /// <summary>
         ///    The text for this question
         /// </summary>
         public string Text { get; }

         /// <summary>
         ///    A valid image Url to display with this question
         /// </summary>
         public string ImageUri { get; }
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