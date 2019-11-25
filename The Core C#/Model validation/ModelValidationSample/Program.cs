using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ModelTypes;
using MoreLinq;

namespace ModelValidationSample
{
   internal static class Program
   {
      private static void Main()
      {
         var user1 = new User
         {
            Id = "",
            Name = "Tom",
            Age = -22
         };
         var errors = Validate(user1);
         errors.ForEach(error => Console.WriteLine(error));

         Console.WriteLine();

         var user2 = new User
         {
            Id = "d3io",
            Name = "Alice",
            Age = 33
         };
         errors = Validate(user2);
         errors.ForEach(error => Console.WriteLine(error));

         Console.ReadLine();
      }

      private static IEnumerable<string> Validate(User user)
      {
         var results = new List<ValidationResult>();
         var validationCtx = new ValidationContext(user);
         if (!Validator.TryValidateObject(user, validationCtx, results, true))
         {
            foreach (var validationResult in results)
            {
               yield return validationResult.ErrorMessage;
            }
         }
         else
         {
            yield return string.Empty;
         }
      }
   }
}