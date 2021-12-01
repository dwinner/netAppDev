using FluentValidation;

namespace RuleSetSample
{
   public class IncludePersonValidator : AbstractValidator<Person>
   {
      public IncludePersonValidator()
      {
         Include(new PersonAgeValidator());
         Include(new PersonNameValidator());
      }
   }
}