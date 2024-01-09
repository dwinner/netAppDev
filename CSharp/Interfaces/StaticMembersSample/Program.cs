namespace StaticMembersSample;

internal class Program
{
   private static void Main(string[] args)
   {
      Console.WriteLine("Hello, World!");
   }
}

// Static virtual/abstract interface members (from C# 11) enable static polymorphism,
// an advanced feature that we will discuss in Chapter 4

internal interface ITypeDescribable
{
   static abstract string Description { get; }
   static virtual string Category => null;
}

// An implementing class or struct must implement static abstract members, and can
// optionally implement static virtual members

internal class CustomerTest : ITypeDescribable
{
   public static string Description => "Customer tests"; // Mandatory
   public static string Category => "Unit testing"; // Optional
}