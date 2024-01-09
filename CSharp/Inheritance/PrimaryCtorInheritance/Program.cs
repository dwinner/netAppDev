namespace PrimaryCtorInheritance;

internal static class Program
{
   private static void Main()
   {
      Console.WriteLine(nameof(PrimaryCtorInheritance));
   }
}

// Classes with primary constructors can subclass with the following syntax:
public class BaseClass(int x)
{ /* ... */
}

public class Subclass(int x, int y) : BaseClass(x)
{ /* ... */
}

// The call to BaseClass(x) is equivalent to calling base(x) in the following example: 
public class Subclass2 : BaseClass
{
   public Subclass2(int x, int y) : base(x)
   { /* ... */
   }
}