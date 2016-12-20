using System;
using System.Composition;

namespace CalculatorUtils
{
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class)]
	public class CalculatorExtensionMetadataAttribute : Attribute
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUri { get; set; }
	}
}
