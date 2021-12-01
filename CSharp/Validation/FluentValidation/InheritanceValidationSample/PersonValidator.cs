using System;
using FluentValidation;

namespace InheritanceValidationSample
{
   public class PersonValidator : AbstractValidator<Person>
   {
      public PersonValidator()
      {
         RuleFor(x => x.Name).NotNull();
         RuleFor(x => x.Email).NotNull();
         RuleFor(x => x.DateOfBirth).GreaterThan(DateTime.MinValue);
      }
   }
}