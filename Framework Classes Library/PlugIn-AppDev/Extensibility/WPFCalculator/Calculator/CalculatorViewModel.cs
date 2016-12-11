using System;
using System.Collections.ObjectModel;
using CalculatorContract;
using CalculatorUtils;

namespace Calculator
{
   public class CalculatorViewModel : BindableBase
   {
      public readonly object SyncCalcAddInOperators = new object();

      private readonly ObservableCollection<Lazy<ICalculatorExtension>> _activatedExtensions =
         new ObservableCollection<Lazy<ICalculatorExtension>>();

      private readonly ObservableCollection<IOperation> _calcAddInOperators = new ObservableCollection<IOperation>();

      private readonly ObservableCollection<Lazy<ICalculatorExtension>> _calcExtensions =
         new ObservableCollection<Lazy<ICalculatorExtension>>();

      public object SyncActivatedExtensions = new object();

      private string _fullInputText;
      private string _input;
      private string _result;

      private string _status;

      public string Status
      {
         get { return _status; }
         set { SetProperty(ref _status, value); }
      }

      public string Input
      {
         get { return _input; }
         set { SetProperty(ref _input, value); }
      }

      public string Result
      {
         get { return _result; }
         set { SetProperty(ref _result, value); }
      }

      public string FullInputText
      {
         get { return _fullInputText; }
         set { SetProperty(ref _fullInputText, value); }
      }

      public ObservableCollection<IOperation> CalcAddInOperators
      {
         get { return _calcAddInOperators; }
      }

      public ObservableCollection<Lazy<ICalculatorExtension>> CalcExtensions
      {
         get { return _calcExtensions; }
      }

      public ObservableCollection<Lazy<ICalculatorExtension>> ActivatedExtensions
      {
         get { return _activatedExtensions; }
      }
   }
}