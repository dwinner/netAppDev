using FluentValidation;

namespace ValidationInCollectionSample
{
   public class CustomerValidator : AbstractValidator<Customer>
   {
      public CustomerValidator()
      {
         RuleForEach(x => x.Orders).SetValidator(new OrderValidator());

         RuleForEach(customer => customer.Orders)
            .ChildRules(orders => orders.RuleFor(order => order.Total).GreaterThan(0));

         RuleForEach(customer => customer.Orders).Where(order => order.Cost != null).SetValidator(new OrderValidator());

         RuleFor(x => x.Orders)
            .Must(x => x.Count <= 10)
            .WithMessage("No more than 10 orders are allowed")
            .ForEach(orderRule =>
            {
               orderRule.Must(order => order.Total > 0)
                  .WithMessage("Orders must have a total of more than 0");
            });
      }
   }
}