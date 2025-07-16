namespace UseOf_ThrowIfFailed;

public class GenericFailure(string reason) : IAmFailure
{
   public string Reason { get; set; } = reason;
}