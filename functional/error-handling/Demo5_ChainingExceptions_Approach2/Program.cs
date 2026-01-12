using LanguageExt;
using static System.Console;

IO.Display("***Chaining functions that can raise errors.***");
// Creating two employees
Employee emp1 = new("E1", 12000);
Employee emp2 = new("E2", 16000.75);

// Proposing a hike percentage
var proposedHike = new Random().Next(8, 15);

// Verifying the promotion eligibility and proposed hike
IO.Verify(emp1 with { Hike = proposedHike });
IO.Verify(emp2 with { Hike = proposedHike });


internal record Employee(
   string Id,
   double Salary,
   int Hike = 0,
   string PromotionStatus = "yet to verify")
{
   public override string ToString() => $"{Id}'s current salary is ${Salary}.";
}

internal static class HrManager
{
   public static Either<Exception, Employee> CheckSalary(Employee emp) =>
      // Checking the promotion criteria.
      // Fail, if the current salary is more than $15000
      emp.Salary > 15000
         ? new Exception("the current salary exceeds $15000.")
         : emp with { PromotionStatus = "Eligible" };

   public static Either<Exception, Employee> ProposeHike(Employee emp) =>
      // Verifying the proposed hike for the employee.
      // Fail, if the proposed hike is less than 10%
      emp.Hike < 10
         ? new Exception($"the proposed hike is {emp.Hike}% which is less than 10%.")
         : emp with { PromotionStatus = "Eligible" };


   // The following function is added for Q&A 8.3    
   public static Either<Exception, Employee> IssueDinnerCoupon(Employee emp) =>
      // Deciding the dinner coupon eligibility.
      // Fail, if the proposed hike is less than 12%       
      emp.Hike < 12
         ? new Exception("to get a dinner coupon minimum salary hike should be 12 %.")
         : emp;
}

internal static class IO
{
   public static void Display(object? msg)
   {
      WriteLine(msg);
   }

   public static void Verify(Employee emp)
   {
      HrManager
         .CheckSalary(emp)
         .Bind(HrManager.ProposeHike)
         .Bind(HrManager.IssueDinnerCoupon)
         .Match
         (
            //Right: emp => Display($"{emp} He/she is eligible for promotion. Proposed hike: {emp.Hike}%"),
            emp => Display(
               $"{emp}.He/she is eligible for a promotion with {emp.Hike}% salary hike and a dinner coupon."),
            e => Display($"{emp} Request failed. Error detail: {e.Message}")
         );
   }
}