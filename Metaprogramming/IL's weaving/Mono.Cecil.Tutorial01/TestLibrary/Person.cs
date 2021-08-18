using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestLibrary
{
    public class Person
    {
        public Person(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public string Name { get; set; }

        public string Occupation { get; set; }

        public DateTime DateOfBirth { get; private set; }

        public int GetAge()
        {
            return (int)(DateTime.Now.Subtract(DateOfBirth).TotalDays / 365);
        }

        public string GetIntro()
        {
            return "Hello, my name is " + Name;
        }
    }
}
