namespace UseOf_Statics.Failures;

public class InvalidDataFailure(string empty) : IAmFailure
{
   public string Reason { get; set; } = empty;
   public static IAmFailure Create(string message) => new InvalidDataFailure(message);
}