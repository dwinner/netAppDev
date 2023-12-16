namespace Strict;

public class AClassThatNeedsToDoSomething(IDoSomething doSomething)
{
   public string DoSomethingElse() => doSomething.DoSomethingElse();
}