using System;

Book book1 = new("Professional C#", "Wrox Press");
Book book2 = new("Professional C#", "Wrox Press");

if (!ReferenceEquals(book1, book2))
{
   Console.WriteLine("Not the same reference");
}

if (book1.Equals(book2))
{
   Console.WriteLine("The same object using the generic Equals method");
}

object book3 = book2;
if (book1.Equals(book3))
{
   Console.WriteLine("The same object using the overridden Equals method");
}

if (book1 == book2)
{
   Console.WriteLine("The same book using the == operator");
}

Book? book4 = null;
if (book4 != book1)
{
   Console.WriteLine("Not the same passing one null Book");
}

var s1 = new S1(4);
var s2 = new S1(4);
//if (s1 == s2)
//{
//    Console.WriteLine("the same");
//}

if (s1.Equals(s2))
{
   Console.WriteLine("the same");
}

internal struct S1 : IEquatable<S1>
{
   public S1(int x) => A = x;
   public readonly int A;

   public bool Equals(S1 other) => throw new NotImplementedException();
}