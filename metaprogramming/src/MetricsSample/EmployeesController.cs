using System.Diagnostics;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;

namespace MetricsSample;

/// <summary>
/// </summary>
[Route("/api/employees")]
public class EmployeesController : Controller
{
    static readonly Counter<int> _registeredEmployees =
        Metrics.Meter.CreateCounter<int>("RegisteredEmployees", "# of registered employees");

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet("manual")]
    public IActionResult RegisterManual()
    {
        var now = DateTimeOffset.UtcNow;
        var tags = new TagList(new ReadOnlySpan<KeyValuePair<string, object?>>([
            new KeyValuePair<string, object?>("Year", now.Year),
            new KeyValuePair<string, object?>("Month", now.Month),
            new KeyValuePair<string, object?>("Day", now.Day)
        ]));

        _registeredEmployees.Add(1, tags);

        return Ok();
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Register()
    {
        EmployeesControllerMetrics.RegisteredEmployees(DateOnly.FromDateTime(DateTime.UtcNow));

        return Ok();
    }
}