// Western Australia's daylight saving rules are interesting, having introduced daylight
// saving midseason in 2006 (and then subsequently rescinding it):

using System.Globalization;

var australiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Australia Standard Time");

foreach (var rule in australiaTimeZone.GetAdjustmentRules())
{
   Console.WriteLine($"Rule: applies from {rule.DateStart} to {rule.DateEnd}");
}

foreach (var rule in australiaTimeZone.GetAdjustmentRules())
{
   Console.WriteLine();
   Console.WriteLine($"Rule: applies from {rule.DateStart} to {rule.DateEnd}");
   Console.WriteLine($"   Delta: {rule.DaylightDelta}");
   Console.WriteLine($"   Start: {FormatTransitionTime(rule.DaylightTransitionStart, false)}");
   Console.WriteLine($"   End:   {FormatTransitionTime(rule.DaylightTransitionEnd, true)}");
}

return;

static string FormatTransitionTime(TimeZoneInfo.TransitionTime transitionTime, bool endTime)
{
   if (endTime && transitionTime is { IsFixedDateRule: true, Day: 1, Month: 1 }
               && transitionTime.TimeOfDay == DateTime.MinValue)
   {
      return "-";
   }

   var dayOfWeek = transitionTime.IsFixedDateRule
      ? transitionTime.Day.ToString()
      : $"{"The first second third fourth last".Split()[transitionTime.Week - 1]} {transitionTime.DayOfWeek} in";

   return
      $"{dayOfWeek} {DateTimeFormatInfo.CurrentInfo.MonthNames[transitionTime.Month - 1]} at {transitionTime.TimeOfDay.TimeOfDay}";
}