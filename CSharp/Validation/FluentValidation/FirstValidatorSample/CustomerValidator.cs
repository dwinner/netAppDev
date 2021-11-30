using FluentValidation;

namespace FirstValidatorSample
{
   public class CustomerValidator : AbstractValidator<Customer>
   {
      public CustomerValidator()
      {
         RuleFor(customer => customer.Surname).NotNull().NotEqual("foo");
         RuleFor(customer => customer.Address.Postcode).NotNull().When(customer => customer.Address != null);
         RuleFor(customer => customer.Address).SetValidator(new AddressValidator());
      }
   }
}