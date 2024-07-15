using System.Diagnostics.Metrics;

namespace MetricsSample;

/// <summary>
///     Metrics
/// </summary>
public static class Metrics
{
    /// <summary>
    ///     Meter
    /// </summary>
    public static readonly Meter Meter = new(nameof(MetricsSample));
}