namespace UseOf_Statics.Failures;

public class ConditionNotSatisfiedFailure(string caller = "Caller not captured") : IAmFailure
{
   public string Reason { get; set; } = $"Condition not satisfied. Caller: {caller}";
   public static IAmFailure Create(string caller) => new ConditionNotSatisfiedFailure(caller);
}