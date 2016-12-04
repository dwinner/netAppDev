using System;
using System.Collections.Generic;
using System.Composition;
using CalculatorContract;

namespace SimpleCalculator
{
	[Export(typeof(ICalculator))]
	public class Calculator : ICalculator
	{
		public IList<IOperation> GetOperations() => new List<IOperation>
		{
			new Operation {Name = "+", NumberOperands = 2},
			new Operation {Name = "-", NumberOperands = 2},
			new Operation {Name = "/", NumberOperands = 2},
			new Operation {Name = "*", NumberOperands = 2}
		};

		public double Operate(IOperation anOperation, double[] operands)
		{			
			double result = 0;
			switch (anOperation.Name)
			{
				case "+":
					result = operands[0] + operands[1];
					break;
				case "-":
					result = operands[0] - operands[1];
					break;
				case "/":
					result = operands[0] / operands[1];
					break;
				case "*":
					result = operands[0] * operands[1];
					break;
				default:
					throw new InvalidOperationException($"Invalid operation {anOperation.Name}");
			}

			return result;
		}
	}
}