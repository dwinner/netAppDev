using FluentValidation;

namespace FirstValidatorSample
{
   public class AddressValidator : AbstractValidator<Address>
   {
      public AddressValidator()
      {
         RuleFor(address => address.Postcode).NotNull();
      }
   }
}