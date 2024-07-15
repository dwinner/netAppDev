using Fundamentals.Metrics;

namespace MetricsSample;


/// <summary>
///     Program metrics
/// </summary>
public static partial class ProgramMetrics
{
    [Counter<int>("Started", "How many times we've started the program")]
    public static partial void Started(int something, string blabbedi, string theThird);
}