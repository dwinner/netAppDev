using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CalculatorContract;
using CalculatorUtils;

namespace CalculatorViewModels
{
	public class CalculatorViewModel : BindableBase
	{
		private readonly CalculatorManager _calculatorManager;
		private double[] _currentOperands;
		private IOperation _currentOperation;
		private string _input;
		private double _result;
		private string _status;

		public CalculatorViewModel()
		{
			_calculatorManager = new CalculatorManager();
			_calculatorManager.ImportsSatisfied += (sender, args) => Status += $"{args.StatusMessage}";
			CalculateCommand = new DelegateCommand(OnCalculate);
		}

		public ObservableCollection<IOperation> CalcAddInOperators { get; } = new ObservableCollection<IOperation>();

		public ICommand CalculateCommand { get; set; }

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

		public IOperation CurrentOperation
		{
			get { return _currentOperation; }
			set
			{
				try
				{
					_currentOperands = new double[value.NumberOperands];
					_currentOperands[0] = double.Parse(Input);
					Input += $" {value.Name}";
					SetProperty(ref _currentOperation, value);
				}
				catch (FormatException formatEx)
				{
					Status = formatEx.Message;
				}
			}
		}

		public double Result
		{
			get { return _result; }
			set { SetProperty(ref _result, value); }
		}

		public string FullInputText { get; set; }

		public void OnCalculate()
		{
			if (_currentOperands.Length == 2)
			{
				var input = Input.Split(' ');
				_currentOperands[1] = double.Parse(input[2]);
				Result = _calculatorManager.InvokeCalculator(_currentOperation, _currentOperands);
			}
		}

		public void OnClearLog() => Status = string.Empty;

		public void Init(params Type[] parts)
		{
			_calculatorManager.InitializeContainer(parts);
			var operations = _calculatorManager.GetOperators();
			CalcAddInOperators.Clear();
			foreach (var operation in operations)
			{
				CalcAddInOperators.Add(operation);
			}
		}
	}
}