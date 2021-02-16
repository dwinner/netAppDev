using System;
using Microsoft.Practices.Unity;

namespace InjectedCtorParam
{
	internal static class Program
	{
		private static void Main()
		{
			// Property injection
			var injectionProperty = new InjectionProperty(nameof(IGame.Name), "Trivial Pursuit Genus Edition");

			// Type registration
			var unityContainer = new UnityContainer();
			unityContainer.RegisterType<IGame, TrivialPursuit>(injectionProperty);

			// NOTE: Parameter injection
			// unityContainer.Resolve<Table>(new ParameterOverride("game", new TicTacToe()));

			var table = unityContainer.Resolve<Table>();
			table.AddPlayer();
			table.AddPlayer();
			table.Play();
			Console.WriteLine(table.GetGameStatus());
		}
	}
}