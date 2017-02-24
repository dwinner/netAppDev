using System;

namespace RuleImports
{
	public sealed class ImportEventArgs : EventArgs
	{
		public string StatusMessage { get; set; }
	}
}