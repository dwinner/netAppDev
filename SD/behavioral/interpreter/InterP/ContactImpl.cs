﻿namespace Interpreter
{
   public class ContactImpl : IContact
   {
      public ContactImpl(string firstName, string lastName, string title, string organization)
      {
         FirstName = firstName;
         LastName = lastName;
         Title = title;
         Organization = organization;
      }

      public ContactImpl()
      {
      }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Title { get; set; }

      public string Organization { get; set; }

      public override string ToString() =>
         $"FirstName: {FirstName}, LastName: {LastName}, Title: {Title}, Organization: {Organization}";
   }
}