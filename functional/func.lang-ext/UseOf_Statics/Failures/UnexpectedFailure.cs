namespace UseOf_Statics.Failures;

public class UnexpectedFailure(string reason) : IAmFailure
{
   public string Reason { get; set; } = reason;

   public override string ToString() => Reason;

   public static IAmFailure Create(string reason) => new UnexpectedFailure(reason);
}