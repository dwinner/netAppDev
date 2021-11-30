// Simple fluent validation

using System;
using FluentValidation;

namespace FirstValidatorSample
{
   internal static class Program
   {
      private static void Main()
      {
         var customer = new Customer();
         var validator = new CustomerValidator();
         var result = validator.Validate(customer /*, strategy => strategy.ThrowOnFailures()*/);
         if (!result.IsValid)
         {
            result.Errors.ForEach(
               failure => Console.WriteLine(
                  $"Property {failure.PropertyName} failed validation. Error was: {failure.ErrorMessage}"));
         }

         // also you can throw validation exception
         try
         {
            validator.ValidateAndThrow(customer);
         }
         catch (ValidationException validationEx)
         {
            foreach (var validationError in validationEx.Errors)
            {
               Console.WriteLine(validationError);
            }
         }
      }
   }
}