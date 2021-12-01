using FluentValidation;

namespace ValidationInCollectionSample
{
   public class PersonValidator : AbstractValidator<Person>
   {
      public PersonValidator()
      {
         RuleForEach(person => person.AddressLines).NotNull().WithMessage("Address {CollectionIndex} is required.");
      }
   }
}