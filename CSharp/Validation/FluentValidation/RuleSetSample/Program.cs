// Rule set validation sample

using System;
using FluentValidation;

namespace RuleSetSample
{
   internal static class Program
   {
      private static void Main()
      {
         var validator = new PersonValidator();
         var person = new Person();
         var result = validator.Validate(person, options => options.IncludeRuleSets("Names"));
         Console.WriteLine(result);
      }
   }
}