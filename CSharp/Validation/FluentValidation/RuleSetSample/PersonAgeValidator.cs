using System;
using FluentValidation;

namespace RuleSetSample
{
   public class PersonAgeValidator : AbstractValidator<Person>
   {
      public PersonAgeValidator()
      {
         RuleFor(x => x.DateOfBirth).Must(BeOver18);
      }

      private static bool BeOver18(DateTime date)
      {
         throw new NotImplementedException();
      }
   }
}