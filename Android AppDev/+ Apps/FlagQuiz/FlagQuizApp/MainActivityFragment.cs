using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Java.Security;
using FragmentV4 =Android.Support.V4.App.Fragment;

namespace AppDevUnited.FlagQuizApp
{
   /// <summary>
   /// Логика приложения Flag Quiz
   /// </summary>
   public class MainActivityFragment : FragmentV4
   {
      private const string ErrorTag = nameof(MainActivityFragment);
      private const int FlagsInQuiz = 10;

      private List<string> _fileNameList; // Имена файлов с флагами
      private List<string> _quizCountriesList;  // Страны текущей викторины
      private ISet<string> _regionsSet;   // Регионы текущей викторины
      private string _correctAnswer;   // Правильная страна для текущего флага
      private int _totalGuesses; // Кол-во попыток
      private int _correctAnswers;  // Кол-во правильных ответов
      private int _guessRows; // Кол-во строк с кнопками вариантов
      private SecureRandom _random; // Генератор случайных чисел
      private Handler _handler;  // Для задержки загрузки следующего флага
      private Android.Views.Animations.Animation _shakeAnimation;   // Анимация неправильного ответа
      private LinearLayout _quizLinearLayout;   // Макет с викториной
      private TextView _questionNumberTextView;   // Номер текущего вопроса
      private ImageView _flagImageView;   // Для вывода флага
      private LinearLayout[] _quessLinearLayouts;  // Строки с кнопками
      private TextView _answerTextView;   // Для правильного ответа

      public override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         // Create your fragment here
      }

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
         // Use this to return your custom view for this Fragment
         // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

         return base.OnCreateView(inflater, container, savedInstanceState);
      }

      public void UpdateGuessRows(ISharedPreferences sharedPreferences)
      {
         throw new NotImplementedException();
      }

      public void UpdateRegions(ISharedPreferences sharedPreferences)
      {
         throw new NotImplementedException();
      }

      public void ResetQuiz()
      {
         throw new NotImplementedException();
      }
   }
}