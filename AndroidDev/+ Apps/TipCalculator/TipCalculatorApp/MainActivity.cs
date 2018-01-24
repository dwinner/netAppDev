using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Text;
using JavaObject = Java.Lang.Object;

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
      private TextView _amountTextView; // Для отформатированной суммы счета
      private double _billAmount = DefaultBillAmout; // Сумма счета, введенная пользователем
      private double _percent = DefaultPercent; // Исходный процент чаевых
      private TextView _percentTextView; // Для вывода процента чаевых
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
         amountEditText.AddTextChangedListener(new AmountEditTextWatcher(this));

         // Назначение слушателя OnSeekBarChangeListener для percentSeekBar
         var percentSeekBar = FindViewById<SeekBar>(Resource.Id.percentSeekBar);
         percentSeekBar.SetOnSeekBarChangeListener(new SeekBarListenerImpl(this));
      }

      /// <summary>
      ///    Вычисление, вывод чаевых и общей суммы
      /// </summary>
      private void Calculate()
      {
         // Форматирование процентов и вывод в _percentTextView
         _percentTextView.Text = _PercentFormat.Format(_percent);

         // Вычисление чаевых и общей суммы
         var tip = _billAmount * _percent;
         var total = _billAmount + tip;

         // Вывод чаевых и общей суммы в формате денежной величины
         _tipTextView.Text = _CurrencyFormat.Format(tip);
         _totalTextView.Text = _CurrencyFormat.Format(total);
      }

      #region Форматировщики денежных сумм и процентов

      private static readonly NumberFormat _CurrencyFormat = NumberFormat.CurrencyInstance;
      private static readonly NumberFormat _PercentFormat = NumberFormat.PercentInstance;

      #endregion

      #region Внутренние классы-слушатели

      private sealed class SeekBarListenerImpl : JavaObject, SeekBar.IOnSeekBarChangeListener
      {
         private readonly MainActivity _mainActivity;

         public SeekBarListenerImpl(MainActivity mainActivity) => _mainActivity = mainActivity;

         public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
         {
            // Обновление процента чаевых
            _mainActivity._percent = progress / 100.0;
            _mainActivity.Calculate();
         }

         public void OnStartTrackingTouch(SeekBar seekBar)
         {
         }

         public void OnStopTrackingTouch(SeekBar seekBar)
         {
         }
      }

      private sealed class AmountEditTextWatcher : JavaObject, ITextWatcher
      {
         private readonly MainActivity _mainActivity;

         public AmountEditTextWatcher(MainActivity mainActivity) => _mainActivity = mainActivity;

         public void AfterTextChanged(IEditable s)
         {
         }

         public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
         {
         }

         public void OnTextChanged(ICharSequence s, int start, int before, int count)
         {
            // Вызывается при изменении пользователем величины счета
            if (double.TryParse(s.ToString(), out var billAmount))
               _mainActivity._amountTextView.Text = _CurrencyFormat.Format(billAmount);
            else
            {
               _mainActivity._amountTextView.Text = string.Empty;
               _mainActivity._billAmount = default(double);
            }

            _mainActivity.Calculate();
         }
      }

      #endregion
   }
}