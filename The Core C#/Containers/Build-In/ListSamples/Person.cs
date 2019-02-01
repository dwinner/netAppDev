namespace ListSamples
{
   public class Person
   {
      private readonly string _name;

      public Person(string name)
      {
         _name = name;
      }

      public override string ToString()
      {
         return _name;
      }
   }
}