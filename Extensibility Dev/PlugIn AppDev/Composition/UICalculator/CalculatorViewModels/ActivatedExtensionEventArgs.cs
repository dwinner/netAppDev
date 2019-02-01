using System;

namespace CalculatorViewModels
{
	public class ActivatedExtensionEventArgs : EventArgs
	{
		public ExtensionChange ExtensionChange { get; set; }
	}
}