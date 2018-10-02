using System;

namespace GreaterThanGenTemplate
{
	internal static class Program
	{
		private static void Main()
		{
			var of = Greater.Of(.7, .9);
			Console.WriteLine(of);
		}
	}
}