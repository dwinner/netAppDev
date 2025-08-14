namespace UseOf_Statics.Failures;

/// <summary>
///    Represents a failure for some reason
/// </summary>
public interface IAmFailure
{
    /// <summary>
    ///    Nature of the failure
    /// </summary>
    string Reason { get; set; }
}