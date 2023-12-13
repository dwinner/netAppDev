namespace Introduction;

public class ClassToBeTested
{
   private readonly IDoSomething doSomething;

   public ClassToBeTested(IDoSomething doSomething) => this.doSomething = doSomething;

   public string GoAheadAndDoIt() => doSomething.DoIt();
}