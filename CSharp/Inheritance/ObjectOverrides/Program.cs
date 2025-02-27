﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectOverrides
{   
   class Person
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public int Age { get; set; }

      public Person(string fName, string lName, int personAge)
      {
         FirstName = fName;
         LastName = lName;
         Age = personAge;
      }

      public Person() { }

      public override string ToString()
      {
         return string.Format("[First Name: {0}; Last Name: {1}; Age: {2}]",
            FirstName,
            LastName,
            Age);
      }            
      
      public override bool Equals(object obj)
      {        
         return obj.ToString() == ToString();
      }
            
      public override int GetHashCode()
      {
         return ToString().GetHashCode();
      }
   } 

   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with System.Object *****\n");

         // NOTE:  We want these to be identical to test
         // the Equals() and GetHashCode() methods.
         Person p1 = new Person("Homer", "Simpson", 50);
         Person p2 = new Person("Homer", "Simpson", 50);

         // Get stringified version of objects.
         Console.WriteLine("p1.ToString() = {0}", p1.ToString());
         Console.WriteLine("p2.ToString() = {0}", p2.ToString());

         // Test Overridden Equals()
         Console.WriteLine("p1 = p2?: {0}", p1.Equals(p2));

         // Test hash codes.
         Console.WriteLine("Same hash codes?: {0}", p1.GetHashCode() == p2.GetHashCode());
         Console.WriteLine();

         // Change age of p2 and test again.
         p2.Age = 45;
         Console.WriteLine("p1.ToString() = {0}", p1.ToString());
         Console.WriteLine("p2.ToString() = {0}", p2.ToString());
         Console.WriteLine("p1 = p2?: {0}", p1.Equals(p2));
         Console.WriteLine("Same hash codes?: {0}", p1.GetHashCode() == p2.GetHashCode());

         Console.WriteLine();
         StaticMembersOfObject();

         Console.ReadLine();
      }

      #region Static members of object
      static void StaticMembersOfObject()
      {
         //  Static members of System.Object.
         Person p3 = new Person("Sally", "Jones", 4);
         Person p4 = new Person("Sally", "Jones", 4);
         Console.WriteLine("P3 and P4 have same state: {0}", object.Equals(p3, p4));
         Console.WriteLine("P3 and P4 are pointing to same object: {0}",
           object.ReferenceEquals(p3, p4));
      }
      #endregion
   }
}
