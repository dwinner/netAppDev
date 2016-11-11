namespace RoadmapAnalyzer.Tests.Targets.MustInvokeBaseMethodCallMethodCodeFixTests
{
	public class Case0Base
	{
		[MustInvoke]
		public virtual void Method() { }
	}

	public class Case0Sub
		: Case0Base
	{
		public override void Method() { }
	}
}
