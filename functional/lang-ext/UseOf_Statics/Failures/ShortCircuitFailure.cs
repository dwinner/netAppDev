namespace UseOf_Statics.Failures;

public class ShortCircuitFailure(string message) : IAmFailure
{
   public string Reason { get; set; } = message;
   public static IAmFailure Create(string msg) => new ShortCircuitFailure(msg);
}