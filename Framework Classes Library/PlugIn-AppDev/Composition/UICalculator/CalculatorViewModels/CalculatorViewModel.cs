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
			_calculatorManager.ImportsSatisfied += (sender, e) => { Status += $"{e.StatusMessage}\n"; };

			CalculateCommand = new DelegateCommand(OnCalculate);
		}

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

		public double Result
		{
			get { return _result; }
			set { SetProperty(ref _result, value); }
		}

		public string FullInputText { get; set; }

		public IOperation CurrentOperation
		{
			get { return _currentOperation; }
			set { SetCurrentOperation(value); }
		}

		public ObservableCollection<IOperation> CalcAddInOperators { get; } = new ObservableCollection<IOperation>();

		public void Init(params Type[] parts)
		{
			_calculatorManager.InitializeContainer(parts);
			var operators = _calculatorManager.GetOperators();
			CalcAddInOperators.Clear();
			foreach (var op in operators)
				CalcAddInOperators.Add(op);
		}


		public void OnClearLog()
		{
			Status = string.Empty;
		}

		public void OnCalculate()
		{
			if (_currentOperands.Length == 2)
			{
				var input = Input.Split(' ');
				_currentOperands[1] = double.Parse(input[2]);
				Result = _calculatorManager.InvokeCalculator(_currentOperation, _currentOperands);
			}
		}

		private void SetCurrentOperation(IOperation op)
		{
			try
			{
				_currentOperands = new double[op.NumberOperands];
				_currentOperands[0] = double.Parse(Input);
				Input += $" {op.Name} ";

				SetProperty(ref _currentOperation, op, nameof(CurrentOperation));
			}
			catch (FormatException ex)
			{
				Status = ex.Message;
			}
		}
	}
}