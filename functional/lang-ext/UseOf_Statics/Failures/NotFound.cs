namespace UseOf_Statics.Failures;

public class NotFound(string message) : IAmFailure
{
   public string Reason { get; set; } = message;

   public static IAmFailure Create(string message) => new NotFound(message);
}