using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SelfValidatedModelSample
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         var user = new User {Id = "", Name = "Tom", Age = -22};
         var results = new List<ValidationResult>();
         var context = new ValidationContext(user);
         if (!Validator.TryValidateObject(user, context, results, true))
         {
            foreach (var error in results)
            {
               Console.WriteLine(error.ErrorMessage);
            }
         }
         else
         {
            Console.WriteLine("Пользователь прошел валидацию");
         }
      }
   }

   internal class User : IValidatableObject
   {
      protected internal string Id { get; set; }

      public string Name { get; set; }

      public int Age { get; set; }

      public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
      {
         var errors = new List<ValidationResult>();

         if (string.IsNullOrWhiteSpace(Name))
         {
            errors.Add(new ValidationResult("Не указано имя"));
         }

         if (string.IsNullOrWhiteSpace(Id))
         {
            errors.Add(new ValidationResult("Не указан идентификатор пользователя"));
         }

         if (Age < 1 || Age > 100)
         {
            errors.Add(new ValidationResult("Недопустимый возраст"));
         }

         return errors;
      }
   }
}