using System;

namespace AppDev.Sapper.Model.Attributes
{
	/// <summary>
	///    Game Status Attribute
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class GameStatusAttribute : Attribute
	{
		/// <summary>
		///    Game status
		/// </summary>
		public string Status { get; set; }
	}
}