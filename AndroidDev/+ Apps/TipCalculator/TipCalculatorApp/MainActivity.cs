using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Text;

namespace AppDevUnited.TipCalculatorApp
{
   /// <summary>
   ///    Main activity
   /// </summary>
   [Activity(
      Label = "TipCalculatorApp",
      Name = "AppDevUnited.TipCalculatorApp.MainActivity",
      MainLauncher = true,
      WindowSoftInputMode = SoftInput.StateAlwaysVisible,
      ScreenOrientation = ScreenOrientation.Portrait)]
   public class MainActivity : AppCompatActivity
   {
      private const double DefaultBillAmout = 0.0;
      private const double DefaultPercent = 0.15;
      private readonly ITextWatcher _amountEditTextWatcher;
      private TextView _amountTextView; // Для отформатированной суммы счета

      private double _billAmout = DefaultBillAmout; // Сумма счета, введенная пользователем
      private double _percent = DefaultPercent; // Исходный процент чаевых
      private TextView _percentTextView; // Для вывода процента чаевых
      private SeekBar.IOnSeekBarChangeListener _seekBarListener;
      private TextView _tipTextView; // Для вывода вычисленных чаевых
      private TextView _totalTextView; // Для вычисленной общей суммы

      /// <inheritdoc />
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.Main);

         // Получение ссылок на компоненты TextView, с которыми активность взаимодействует на программном уровне
         _amountTextView = FindViewById<TextView>(Resource.Id.amountTextView);
         _percentTextView = FindViewById<TextView>(Resource.Id.percentTextView);
         _tipTextView = FindViewById<TextView>(Resource.Id.tipTextView);
         _totalTextView = FindViewById<TextView>(Resource.Id.totalTextView);

         _tipTextView.Text = _CurrencyFormat.Format(0);
         _totalTextView.Text = _CurrencyFormat.Format(0);

         // Назначение слушателя TextWatcher для amountEditText
         var amountEditText = FindViewById<EditText>(Resource.Id.amountEditText);
         amountEditText.AddTextChangedListener(_amountEditTextWatcher);

         // Назначение слушателя OnSeekBarChangeListener для percentSeekBar
         var percentSeekBar = FindViewById<SeekBar>(Resource.Id.percentSeekBar);
         percentSeekBar.SetOnSeekBarChangeListener(_seekBarListener);
      }

      #region Форматировщики денежных сумм и процентов

      private static readonly NumberFormat _CurrencyFormat = NumberFormat.CurrencyInstance;
      private static readonly NumberFormat _PercentFormat = NumberFormat.PercentInstance;

      #endregion
   }
}