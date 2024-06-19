namespace MultipleInheritance;

public class Bird : IBird
{
   public int Age { get; set; }

   public void Fly()
   {
      if (Age >= 10)
      {
         Console.WriteLine("I am flying!");
      }
   }
}