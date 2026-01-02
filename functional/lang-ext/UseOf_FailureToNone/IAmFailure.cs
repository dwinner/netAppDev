namespace UseOf_FailureToNone;

public interface IAmFailure
{
   string Reason { get; set; }
}

public class GenericFailure(string reason) : IAmFailure
{
   public string Reason { get; set; } = reason;
}