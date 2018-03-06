using System;

namespace MainExample
{
   public class Person
   {
      public DateTime DateOfBirth { get; set; }
      public string Name { get; set; }
      public Sex Sex1 { get; set; }

      public Person(string name, Sex sex, DateTime dob)
      {
         Name = name;
         Sex1 = sex;
         DateOfBirth = dob;
      }
   }
}