using System;
using System.Composition;

namespace CalculatorUtils
{
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class)]
	public class SpeedMetadataAttribute : Attribute
	{
		public Speed Speed { get; set; }
	}
}