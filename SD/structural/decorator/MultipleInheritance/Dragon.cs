namespace MultipleInheritance;

public class Dragon(IBird bird, ILizard lizard) : IBird, ILizard
{
   public void Fly()
   {
      bird.Fly();
   }

   public int Age
   {
      get => bird.Age;
      set => bird.Age = lizard.Age = value;
   }

   public void Crawl()
   {
      lizard.Crawl();
   }
}