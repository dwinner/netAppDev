using FluentValidation;

namespace RuleSetSample
{
   public class PersonNameValidator : AbstractValidator<Person>
   {
      public PersonNameValidator()
      {
         RuleFor(x => x.Surname).NotNull().Length(0, 255);
         RuleFor(x => x.Forename).NotNull().Length(0, 255);
      }
   }
}