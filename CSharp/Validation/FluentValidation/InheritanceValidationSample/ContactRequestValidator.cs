using FluentValidation;

namespace InheritanceValidationSample
{
   public class ContactRequestValidator : AbstractValidator<ContactRequest>
   {
      public ContactRequestValidator()
      {
         RuleFor(x => x.Contact).SetInheritanceValidator(v =>
         {
            v.Add(new OrganizationValidator());
            v.Add(new PersonValidator());
         });
      }
   }
}