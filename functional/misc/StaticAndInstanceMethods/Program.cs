using static System.Console;

Person.StaticMethod();

Person person = new();
person.InstanceMethod();

internal class Person
{
   public void InstanceMethod()
   {
      WriteLine("Invoked InstanceMethod().");
   }

   public static void StaticMethod()
   {
      WriteLine("Invoked StaticMethod().");
   }
}