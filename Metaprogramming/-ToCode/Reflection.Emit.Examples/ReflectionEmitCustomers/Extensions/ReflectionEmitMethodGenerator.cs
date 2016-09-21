using System;
using System.Reflection.Emit;
using System.Reflection;

namespace ReflectionEmitCustomers.Extensions
{
	public sealed class ReflectionEmitMethodGenerator
	{		
		public Func<T, string> Generate<T>()
		{
			var target = typeof(T);
			var type = this.Module.DefineType(target.Namespace + "." + target.Name);
			var methodName = "ToString" + target.GetHashCode().ToString();
			var method = type.DefineMethod(methodName,
				MethodAttributes.Static | MethodAttributes.Public, 
				typeof(string), new Type[] { target });

			method.GetILGenerator().Generate(target);
			var createdType = type.CreateType();

			var createdMethod = createdType.GetMethod(methodName);
			return (Func<T, string>)Delegate.CreateDelegate(
				typeof(Func<T, string>), createdMethod);
		}
	}
}
