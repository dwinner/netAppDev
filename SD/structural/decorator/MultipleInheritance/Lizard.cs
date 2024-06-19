namespace MultipleInheritance;

public class Lizard : ILizard
{
   public int Age { get; set; }

   public void Crawl()
   {
      if (Age < 10)
      {
         Console.WriteLine("I am crawling!");
      }
   }
}