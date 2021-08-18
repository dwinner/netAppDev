using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using CalculatorContract;
using static System.Console;

namespace SimpleHost
{
	internal class Program
	{
		public ICalculator Calculator { get; set; }

		private static void Main()
		{
			var program = new Program();
			program.Bootstrap();
			program.Run();
		}

		private void Bootstrap()
		{
			var conventions = new ConventionBuilder();
			conventions.ForTypesDerivedFrom<ICalculator>().Export<ICalculator>().Shared();
			conventions.ForType<Program>().ImportProperty<ICalculator>(program => program.Calculator);

			var configuration = new ContainerConfiguration()
				.WithDefaultConventions(conventions)
				.WithAssemblies(GetAssemblies("c:/addins"));

			using (var host = configuration.CreateContainer())
			{
				host.SatisfyImports(this, conventions);
			}
		}

		private void Run()
		{
			CalculatorLoop();
		}

		private static IEnumerable<Assembly> GetAssemblies(string path)
		{
			var files = Directory.EnumerateFiles(path, "*.dll");
			return files.Select(Assembly.LoadFile).ToList();
		}

		private void CalculatorLoop()
		{
			var operations = Calculator.GetOperations();
			var operationsDict = new SortedList<string, IOperation>();
			foreach (var item in operations)
			{
				WriteLine($"Name: {item.Name}, number operands: {item.NumberOperands}");
				operationsDict.Add(item.Name, item);
			}

			WriteLine();
			string selectedOp = null;
			do
			{
				try
				{
					Write("Operation? ");
					selectedOp = ReadLine();
					if (selectedOp != null && (selectedOp.ToLower() == "exit" || !operationsDict.ContainsKey(selectedOp)))
					{
						continue;
					}

					if (selectedOp != null)
					{
						var operation = operationsDict[selectedOp];
						var operands = new double[operation.NumberOperands];
						for (var i = 0; i < operation.NumberOperands; i++)
						{
							Write($"\t operand {i + 1}? ");
							var selectedOperand = ReadLine();
							if (selectedOperand != null)
							{
								operands[i] = double.Parse(selectedOperand);
							}
						}

						WriteLine("calling calculator");
						var result = Calculator.Operate(operation, operands);
						WriteLine($"result: {result}");
					}
				}
				catch (FormatException ex)
				{
					WriteLine(ex.Message);
					WriteLine();
				}
			} while (selectedOp != "exit");
		}
	}
}