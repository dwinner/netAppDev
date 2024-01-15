// TimeZoneInfo.Local returns the current local time zone:

var zone = TimeZoneInfo.Local;

var standardName = zone.StandardName;
Console.WriteLine($"StandardName (local): {standardName}");

var daylightName = zone.DaylightName;
Console.WriteLine($"DaylightName (local): {daylightName}");

var dt1 = new DateTime(2019, 1, 1);
var dt2 = new DateTime(2019, 6, 1);
Console.WriteLine(zone.IsDaylightSavingTime(dt1));
Console.WriteLine(zone.IsDaylightSavingTime(dt2));
Console.WriteLine(zone.GetUtcOffset(dt1));
Console.WriteLine(zone.GetUtcOffset(dt2));

// You can obtain a TimeZoneInfo for any of the world’s time zones by calling FindSystemTimeZoneById with the zone ID:
var wa = TimeZoneInfo.FindSystemTimeZoneById("W. Australia Standard Time");

Console.WriteLine(wa.Id); // W. Australia Standard Time
Console.WriteLine(wa.DisplayName); // (GMT+08:00) Perth
Console.WriteLine(wa.BaseUtcOffset); // 08:00:00
Console.WriteLine(wa.SupportsDaylightSavingTime); // True
Console.WriteLine();

// The following returns all world timezones:
foreach (var zoneInf in TimeZoneInfo.GetSystemTimeZones())
{
   Console.WriteLine(zoneInf.Id);
}