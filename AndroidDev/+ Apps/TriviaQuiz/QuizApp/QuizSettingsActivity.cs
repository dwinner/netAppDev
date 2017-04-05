using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Text.Format;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using static QuizApp.QuizPreferences;
using Uri = Android.Net.Uri;

namespace QuizApp
{
   [Activity(Label = "@string/settings")]
   public class QuizSettingsActivity : Activity
   {
      private const int DateDialogId = 0;
      private const int PasswordDialogId = 1;
      private const int TakeAvatarCameraRequest = 1;
      private const int TakeAvatarGalleryRequest = 2;
      private const string DebugTag = nameof(QuizSettingsActivity);
      private ISharedPreferences _gameSettings;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.Settings);

         // Извлекаем общие настройки приложения
         _gameSettings = GetSharedPreferences(GamePreferences, FileCreationMode.Private);

         // Инициализируем UI-элементы активности
         InitAvatar();
         InitNicknameEntry();
         InitEmailEntry();
         InitPasswordChooser();
         InitDatePicker();
         InitGenderSpinner();
      }

      protected override void OnDestroy()
      {
         Log.Debug(DebugTag,
            $"Nickname is: {_gameSettings.GetString(GamePreferencesNickname, "Not set")}");
         Log.Debug(DebugTag,
            $"Email is: {_gameSettings.GetString(GamePreferencesEmail, "Not set")}");
         Log.Debug(DebugTag,
            $"Gender (M=1, F=2, U=0) is: {_gameSettings.GetInt(GamePreferencesGender, 0)}");
         Log.Debug(DebugTag,
            $"Password is: {_gameSettings.GetString(GamePreferencesPassword, "Not set")}");
         Log.Debug(DebugTag,
            $"DOB is: {DateFormat.Format("MMMM dd, yyyy", _gameSettings.GetLong(GamePreferencesDob, default(long)))}");

         base.OnDestroy();
      }

      [Obsolete("deprecated")]
      protected override Dialog OnCreateDialog(int id)
      {
         switch (id)
         {
            case DateDialogId:
               var dateOfBirth = FindViewById<TextView>(Resource.Id.TextView_DOB_Info);
               var datePicker = new DatePickerDialog(this, (sender, e) =>
               {
                  var editor = _gameSettings.Edit();
                  editor.PutLong(GamePreferencesDob, e.Date.ToBinary()).Commit();
                  dateOfBirth.Text = e.Date.ToString("d");
               }, 0, 0, 0);
               return datePicker;

            case PasswordDialogId:
               var inflater = (LayoutInflater) GetSystemService(LayoutInflaterService);
               var layout = inflater.Inflate(Resource.Layout.PasswordDialog, FindViewById<ViewGroup>(Resource.Id.root));
               var passwordEditBox = layout.FindViewById<EditText>(Resource.Id.EditText_Pwd1);
               var confirmPasswordEditBox = layout.FindViewById<EditText>(Resource.Id.EditText_Pwd2);
               var errorTextView = layout.FindViewById<TextView>(Resource.Id.TextView_PwdProblem);
               confirmPasswordEditBox.AfterTextChanged += (sender, args) =>
               {
                  var password = passwordEditBox.Text;
                  var confirmPassword = confirmPasswordEditBox.Text;
                  errorTextView.SetText(password == confirmPassword
                     ? Resource.String.settings_pwd_equal
                     : Resource.String.settings_pwd_not_equal);
               };

               // Настраиваем и создаем AlertDialog
               return new AlertDialog.Builder(this)
                  .SetView(layout)
                  .SetTitle(Resource.String.settings_button_pwd)
                  .SetNegativeButton(Android.Resource.String.Cancel,
                     (sender, args) => { RemoveDialog(PasswordDialogId); /* Не кэшируем диалог */ })
                  .SetPositiveButton(Android.Resource.String.Ok,
                     (sender, args) =>
                     {
                        var passwordInfo = FindViewById<TextView>(Resource.Id.TextView_Password_Info);
                        var password = passwordEditBox.Text;
                        var confirmPassword = confirmPasswordEditBox.Text;
                        if (password == confirmPassword)
                        {
                           _gameSettings.Edit().PutString(GamePreferencesPassword, password).Commit();
                           passwordInfo.SetText(Resource.String.settings_pwd_set);
                        }
                        else
                        {
                           Log.Debug(DebugTag, "Password do not match. Not saving. Keeping old password (if set).");
                        }

                        RemoveDialog(PasswordDialogId); // Не кэшируем диалог
                     })
                  .Create();

            default:
               return null;
         }
      }

      [Obsolete("deprecated")]
      protected override void OnPrepareDialog(int id, Dialog dialog)
      {
         base.OnPrepareDialog(id, dialog);

         switch (id)
         {
            case DateDialogId:
               // Инициализация диалога DatePickerDialog
               var dateDialog = (DatePickerDialog) dialog;

               // Проверка существования параметра даты рождения
               var dateOfBirth = _gameSettings.Contains(GamePreferencesDob)
                  ? DateTime.FromBinary(_gameSettings.GetLong(GamePreferencesDob, default(long)))
                  : DateTime.Now;

               // Установка даты в DatePicker датой рождения или текущей датой
               dateDialog.UpdateDate(dateOfBirth);
               break;
         }
      }

      protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
      {
         switch (requestCode)
         {
            case TakeAvatarCameraRequest:
               if (resultCode == Result.Ok)
               {
                  // Обработка отснятой фотографии
                  var dataExtras = data.Extras;
                  var cameraPic = dataExtras.Get("data") as Bitmap;
                  if (cameraPic != null)
                     SaveAvatar(cameraPic);
               }
               break;

            case TakeAvatarGalleryRequest:
               if (resultCode == Result.Ok)
               {
                  // Обработка выбранного изображения
                  var photoUri = data.Data;
                  if (photoUri != null)
                  {
                     const int maxLen = 75;
                     // Полный размер изображения обычно велик. Масштабируем его для изображения аватара
                     var galleryPic = MediaStore.Images.Media.GetBitmap(ContentResolver, photoUri);
                     var scaledGalleryPic = CreateScaledBitmapKeepingAspectRatio(galleryPic, maxLen);
                     SaveAvatar(scaledGalleryPic);
                  }
               }
               break;
         }
      }

      /// <summary>
      ///    Масштабирование изображения с сохранением пропорций исходного
      /// </summary>
      /// <param name="bitmap">Растровое изображение</param>
      /// <param name="maxLen">Квадрат масштабирования</param>
      /// <returns>Новое растровое изображение</returns>
      private static Bitmap CreateScaledBitmapKeepingAspectRatio(Bitmap bitmap, int maxLen)
      {
         var originalHeight = bitmap.Height;
         var originalWidth = bitmap.Width;

         // Масштабируем в квадрате 75px
         var scaledWidth = originalWidth >= originalHeight
            ? maxLen
            : (int) (maxLen * (originalWidth / (float) originalHeight));
         var scaledHeight = originalHeight >= originalWidth
            ? maxLen
            : (int) (maxLen * (originalHeight / (float) originalWidth));

         // Создание масштабированного растра
         var scaledGalleryPic = Bitmap.CreateScaledBitmap(bitmap, scaledWidth, scaledHeight, true);
         return scaledGalleryPic;
      }

      /// <summary>
      ///    Сохранение аватара
      /// </summary>
      /// <param name="bitmap">Растровое изображение</param>
      private void SaveAvatar(Bitmap bitmap)
      {
         const string avatarFilename = "avatar.jpg";
         bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, OpenFileOutput(avatarFilename, FileCreationMode.Private));
         var imageUri = Uri.FromFile(new File(FilesDir, avatarFilename));
         _gameSettings.Edit().PutString(GamePreferencesAvatar, imageUri.Path).Commit();

         // Обновление экрана настроек
         var avatarButton = FindViewById<ImageButton>(Resource.Id.ImageButton_Avatar);
         var avatartUri = _gameSettings.GetString(GamePreferencesAvatar, default(string));
         if (avatartUri != default(string))
         {
            var parsedAvatarUri = Uri.Parse(avatartUri);
            avatarButton.SetImageURI(null); // NOTE: Force no cache
            avatarButton.SetImageURI(parsedAvatarUri);
         }
      }

      private void InitGenderSpinner()
      {
         // Заполняем элемент управления Spinner
         var genderSpinner = FindViewById<Spinner>(Resource.Id.Spinner_Gender);
         var spinnerAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.genders,
            Android.Resource.Layout.SimpleSpinnerItem);
         spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
         genderSpinner.Adapter = spinnerAdapter;
         if (_gameSettings.Contains(GamePreferencesGender))
            genderSpinner.SetSelection(_gameSettings.GetInt(GamePreferencesGender, default(int)));

         genderSpinner.ItemSelected +=
            (sender, args) => _gameSettings.Edit().PutInt(GamePreferencesGender, args.Position).Commit();
      }

      /// <summary>
      ///    Инициализация элементов управления для установки/получения даты
      /// </summary>
      private void InitDatePicker()
      {
         // Установка существующего значения даты в TextView
         var dobInfo = FindViewById<TextView>(Resource.Id.TextView_DOB_Info);
         dobInfo.Text = _gameSettings.Contains(GamePreferencesDob)
            ? DateTime.FromBinary(_gameSettings.GetLong(GamePreferencesDob, default(long))).ToString("d")
            : Resources.GetString(Resource.String.settings_dob_not_set);

         // Вызов диалогового окна выбора даты
         var pickDateButton = FindViewById<Button>(Resource.Id.Button_DOB);
#pragma warning disable 618
         pickDateButton.Click += (sender, args) => ShowDialog(DateDialogId);
#pragma warning restore 618
      }

      /// <summary>
      ///    Инициализация элементов управления для установки/получения пароля
      /// </summary>
      private void InitPasswordChooser()
      {
         // Установка информации о существовании пароля
         var passwordInfo = FindViewById<TextView>(Resource.Id.TextView_Password_Info);
         passwordInfo.SetText(_gameSettings.Contains(GamePreferencesPassword)
            ? Resource.String.settings_pwd_set
            : Resource.String.settings_pwd_not_set);

         // Вызов диалогового окна для установки пароля
         var callPassDlgButton = FindViewById<Button>(Resource.Id.Button_Password);
#pragma warning disable 618
         callPassDlgButton.Click += (sender, args) => ShowDialog(PasswordDialogId);
#pragma warning restore 618
      }

      /// <summary>
      ///    Инициализация поля ввода email
      /// </summary>
      private void InitEmailEntry()
      {
         // Показываем текущее значение в EditText, если оно есть
         var emailText = FindViewById<EditText>(Resource.Id.EditText_Email);
         if (_gameSettings.Contains(GamePreferencesEmail))
            emailText.Text = _gameSettings.GetString(GamePreferencesEmail, string.Empty);

         emailText.KeyPress += (sender, args) =>
         {
            if (args.Event.Action == KeyEventActions.Down && args.KeyCode == Keycode.Enter)
               _gameSettings.Edit().PutString(GamePreferencesEmail, emailText.Text).Commit();
         };
      }

      /// <summary>
      ///    Инициализация имени
      /// </summary>
      private void InitNicknameEntry()
      {
         // Отображаем имя в поле ввода, если оно уже есть
         var nickNameEditText = FindViewById<EditText>(Resource.Id.EditText_Nickname);
         if (_gameSettings.Contains(GamePreferencesNickname))
            nickNameEditText.Text = _gameSettings.GetString(GamePreferencesNickname, string.Empty);

         nickNameEditText.KeyPress += (sender, args) =>
         {
            if (args.Event.Action == KeyEventActions.Down && args.KeyCode == Keycode.Enter)
            {
               var updatedNickName = nickNameEditText.Text;
               _gameSettings.Edit().PutString(GamePreferencesNickname, updatedNickName).Commit();
            }
         };
      }

      /// <summary>
      ///    Инициализация аватара
      /// </summary>
      private void InitAvatar()
      {
         var avatarButton = FindViewById<ImageButton>(Resource.Id.ImageButton_Avatar);
         if (_gameSettings.Contains(GamePreferencesAvatar))
         {
            var avatar = _gameSettings.GetString(GamePreferencesAvatar, default(string));
            if (avatar != default(string))
            {
               var avatarUri = Uri.Parse(avatar);
               avatarButton.SetImageURI(avatarUri);
            }
         }
         else
         {
            avatarButton.SetImageResource(Resource.Drawable.Avatar);
         }

         avatarButton.Click += (sender, args) =>
         {
            const string avatarPrompt = "Take your picture to store as your avatar!";
            var pictureIntent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(Intent.CreateChooser(pictureIntent, avatarPrompt), TakeAvatarCameraRequest);
         };
         avatarButton.LongClick += (sender, args) =>
         {
            const string avatarPrompt = "Choose a picture to use as your avatar!";
            var pickPhotoIntent = new Intent(Intent.ActionPick);
            pickPhotoIntent.SetType("image/*");
            StartActivityForResult(Intent.CreateChooser(pickPhotoIntent, avatarPrompt), TakeAvatarGalleryRequest);
         };
      }
   }
}