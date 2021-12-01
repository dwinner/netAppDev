using FluentValidation;

namespace ValidationInCollectionSample
{
   public class OrderValidator : AbstractValidator<Order>
   {
      public OrderValidator()
      {
         RuleFor(x => x.Total).GreaterThan(0);
      }
   }
}