using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Java.IO;
using Java.Security;
using FragmentV4 =Android.Support.V4.App.Fragment;
using R = FlagQuiz.App.Resource;

namespace FlagQuiz.App
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
      private Animation _shakeAnimation;   // Анимация неправильного ответа
      private LinearLayout _quizLinearLayout;   // Макет с викториной
      private TextView _questionNumberTextView;   // Номер текущего вопроса
      private ImageView _flagImageView;   // Для вывода флага
      private LinearLayout[] _guessLinearLayouts;  // Строки с кнопками
      private TextView _answerTextView;   // Для правильного ответа

      public override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         // Create your fragment here
      }

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {         
         base.OnCreateView(inflater, container, savedInstanceState);                  
         var view = inflater.Inflate(Resource.Layout.fragment_main, container, false);
         _fileNameList=new List<string>();
         _random=new SecureRandom();
         _handler=new Handler();

         // Загрузка анимации для неправильных ответов
         _shakeAnimation = AnimationUtils.LoadAnimation(Activity, R.Animation.incorrect_shake);
         _shakeAnimation.RepeatCount = 3; // Анимация повторяется три раза

         // Получение ссылок на компоненты UI
         _quizLinearLayout = view.FindViewById<LinearLayout>(R.Id.quizLinearLayout);
         _questionNumberTextView = view.FindViewById<TextView>(R.Id.questionNumberTextView);
         _flagImageView = view.FindViewById<ImageView>(R.Id.flagImageView);
         _guessLinearLayouts=new LinearLayout[4]
         {
            view.FindViewById<LinearLayout>(R.Id.row1LinearLayout),
            view.FindViewById<LinearLayout>(R.Id.row2LinearLayout),
            view.FindViewById<LinearLayout>(R.Id.row3LinearLayout),
            view.FindViewById<LinearLayout>(R.Id.row4LinearLayout)
         };
         _answerTextView = view.FindViewById<TextView>(R.Id.answerTextView);

         // Настройка слушателей для кнопок ответов
         foreach (var layout in _guessLinearLayouts)
         {
            for (int column = 0; column < layout.ChildCount; column++)
            {
               if (layout.GetChildAt(column) is Button button)
               {
                  button.Click += (sender, args) =>
                  {
                     // TODO: Guess button listener
                  };
               }
            }
         }

         // Назначение текста questionNumberTextView
         _questionNumberTextView.Text = GetString(R.String.question, 1, FlagsInQuiz);

         return view;   // Представление фрагмента для вывода
      }

      /// <summary>
      /// Обновление guessRows на основании значения SharedPreferences
      /// </summary>
      /// <param name="sharedPreferences">Объект постоянного хранилища</param>
      public void UpdateGuessRows(ISharedPreferences sharedPreferences)
      {
         // Получение количества отображаемых вариантов ответа
         var choices = sharedPreferences.GetString(MainActivity.Choices, null);
         _guessRows = int.Parse(choices) / 2;

         // Все компоненты LinearLayout скрываются
         foreach (var layout in _guessLinearLayouts)
         {
            layout.Visibility = ViewStates.Gone;
         }

         // Отображение нужных компонентов LinearLayout
         for (int row = 0; row < _guessRows; row++)
         {
            _guessLinearLayouts[row].Visibility = ViewStates.Visible;
         }
      }

      public void UpdateRegions(ISharedPreferences sharedPreferences)
         => _regionsSet=new HashSet<string>(sharedPreferences.GetStringSet(MainActivity.Regions,null));

      public void ResetQuiz()
      {
         // Использование AssetManager для получения имен файлов изображений
         var assets = Activity.Assets;
         _fileNameList.Clear();

         // Перебрать все регионы
         try
         {
            foreach (var region in _regionsSet)
            {
               var paths = assets.List(region);
               Array.ForEach(paths, path => _fileNameList.Add(path.Replace(".png", string.Empty)));
            }
         }
         catch (IOException ioEx)
         {
            Log.Error(ErrorTag, "Error loading image file names", ioEx);
         }
         finally
         {
            _correctAnswers = 0;  // Сброс кол-ва правильных ответов
            _totalGuesses = 0;   // Сброс общего кол-ва попыток
            _quizCountriesList.Clear();   // Очистка предыдущего списка стран
         }

         int flagCounter = 0;
         int numberOfFlags = _fileNameList.Count;

         // Добавление FLAGS_IN_QUIZ случайных файлов в quizCountriesList
         while (flagCounter<=FlagsInQuiz)
         {
            var randomIndex = _random.NextInt(numberOfFlags);

            // Получение случайного имени файла
            var fileName = _fileNameList[randomIndex];

            // Если регион включен, но ещё не был выбран
            if (!_quizCountriesList.Contains(fileName))
            {
               _quizCountriesList.Add(fileName);   // Добавить файл в список
               ++flagCounter;
            }
         }

         LoadNextFlag();   // Запустить викторину загрузкой первого флага
      }

      private void LoadNextFlag()   // Загрузка следующего флага после правильного ответа
      {
         // Получение имени файла следующего флага и удаление его из списка
         var nextImage = _quizCountriesList[0];
         _quizCountriesList.RemoveAt(0);
         _correctAnswer = nextImage;   // Обновление правильного ответа
         _answerTextView.Text = string.Empty;   // Очистка _answerTextView

         // Отображение номера текущего вопроса
         _questionNumberTextView.Text = GetString(R.String.question, _correctAnswer + 1, FlagsInQuiz);

         // Извлечение региона из имени следующего изображения
         var region = nextImage.Substring(0,nextImage.IndexOf('-'));

         // Использование AssetManager для загрузки следующего изображения
         var assets = Activity.Assets;

         // Получение объекта InputStream для ресурса следующего флага и попытка использования InputStream
         try
         {
            using (var stream = assets.Open($"{region}/{nextImage}.png"))
            {
               // Загрузка графики в виде объекта Drawable и вывод на flagImageView
               var flag = Drawable.CreateFromStream(stream, nextImage);
               _flagImageView.SetImageDrawable(flag);
               Animate(false);   // Анимация появления флага на экране
            }
         }
         catch (IOException ioEx)
         {
            Log.Error(ErrorTag, $"Error loading {nextImage}", ioEx);
         }

         _fileNameList.Shuffle();   // Перестановка имён файлов

         // Помещение правильного ответа в конец fileNameList
         var correct = _fileNameList.IndexOf(_correctAnswer);
         _fileNameList.RemoveAt(correct);
         _fileNameList.Add(_correctAnswer);

         // Добавление 2, 4, 6 или 8 кнопок в зависимости от значения guessRows
         for (int row = 0; row < _guessRows; row++)
         {
            // Размещение кнопок в currentTableRow
            for (int column = 0; column < _guessLinearLayouts[row].ChildCount; column++)
            {
               // Получение ссылки на Button
               if (_guessLinearLayouts[row].GetChildAt(column) is Button newGuessButton)
               {
                  newGuessButton.Enabled = true;

                  // Назначение названия страны текстом newGuessButton
                  var fileName = _fileNameList[row*2+column];
                  newGuessButton.Text = GetCountryName(fileName);
               }
            }            
         }
         
         var nextRow = _random.NextInt(_guessRows);   // Случайная замена одной кнопки правильным ответом
         var nextCol = _random.NextInt(2);   // Выбор случайного столбца
         var randomRow = _guessLinearLayouts[nextRow];   // Получение строки
         var countryName = GetCountryName(_correctAnswer);
         if (randomRow.GetChildAt(nextCol) is Button choiceButton)
         {
            choiceButton.Text = countryName;
         }
      }

      private string GetCountryName(string fileName)
      {
         throw new NotImplementedException();
      }

      private void Animate(bool shouldAnimate)
      {
         throw new NotImplementedException();
      }
   }
}