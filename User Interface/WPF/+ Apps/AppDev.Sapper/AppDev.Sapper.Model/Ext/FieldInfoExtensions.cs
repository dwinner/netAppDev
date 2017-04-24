using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PostSharp.Patterns.Contracts;

namespace AppDev.Sapper.Model.Ext
{
	public static class FieldInfoExtensions
	{
		public static IEnumerable<T> GetCustomAttributes<T>([Required] this FieldInfo fieldInfo, bool inherit = false)
			where T : Attribute
			=> fieldInfo.GetCustomAttributes(typeof(T), inherit).OfType<T>();
	}
}