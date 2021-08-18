namespace ScriptsAndSecurity
{
   public sealed class Person
   {
      public Person(string name, uint age)
      {
         Name = name;
         Age = age;
      }

      public uint Age { get; set; }
      public string Name { get; set; }

      public void Save()
      {
      }
   }
}