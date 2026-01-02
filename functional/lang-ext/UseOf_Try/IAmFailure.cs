namespace UseOf_Try
{
    public interface IAmFailure
    {
        string Reason { get; set; }
    }

    public class GenericFailure : IAmFailure
    {
        public GenericFailure(string reason)
        {
            Reason = reason;
        }

        public string Reason { get; set; }
    }
}
