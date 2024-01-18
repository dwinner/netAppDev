namespace ExtendingKeyedCollectionSample;

public class Animal(string name, int popularity)
{
   public int Popularity = popularity;

   public string Name
   {
      get => name;
      set
      {
         Zoo?.Animals.NotifyNameChange(this, value);
         name = value;
      }
   }

   public Zoo? Zoo { get; internal set; }
}