using FluentValidation;

namespace RuleSetSample
{
   public class PersonValidator : AbstractValidator<Person>
   {
      public PersonValidator()
      {
         RuleSet("Names", () =>
         {
            RuleFor(x => x.Surname).NotNull();
            RuleFor(x => x.Forename).NotNull();
         });

         RuleFor(x => x.Id).NotEqual(0);
      }
   }
}