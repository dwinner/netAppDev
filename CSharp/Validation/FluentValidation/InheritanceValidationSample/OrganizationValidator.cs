using FluentValidation;

namespace InheritanceValidationSample
{
   public class OrganizationValidator : AbstractValidator<Organization>
   {
      public OrganizationValidator()
      {
         RuleFor(x => x.Name).NotNull();
         RuleFor(x => x.Email).NotNull();
         //RuleFor(x => x.HeadQuarters).SetValidator(new AddressValidator());
      }
   }
}