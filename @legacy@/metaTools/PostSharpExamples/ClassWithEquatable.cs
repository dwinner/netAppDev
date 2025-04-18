﻿using System;
//using IEquatableAspect;

namespace PostSharpExamples
{
	[Serializable]
	public sealed class ClassWithEquatableAttribute
		: IEquatableAttributeMock<ClassWithEquatable> { }

	//[ClassWithEquatable]
	public sealed class ClassWithEquatable
	{
		public int IntData { get; set; }
		public string StringData { get; set; }
	}
}
