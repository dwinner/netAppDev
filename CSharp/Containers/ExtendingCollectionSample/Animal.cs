namespace ExtendingCollectionSample;

public class Animal(string name, int popularity)
{
   public string Name = name;
   public int Popularity = popularity;

   public Zoo? Zoo { get; internal set; }
}