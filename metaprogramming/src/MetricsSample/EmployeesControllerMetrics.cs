using System.Diagnostics;
using Fundamentals.Metrics;

namespace MetricsSample;

/// <summary>
/// </summary>
public static class EmployeesControllerMetrics
{
    /// <summary>
    /// </summary>
    /// <param name="date"></param>
    [Counter<int>("RegisteredEmployees", "# of registered employees")]
    public static /*partial*/ void RegisteredEmployees(DateOnly date) => Debug.WriteLine(date.Year);
}