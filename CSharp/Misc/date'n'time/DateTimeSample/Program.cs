// When you compare two DateTime instances, only their ticks values are compared; their DateTimeKinds are ignored:

var dt1 = new DateTime(2000, 1, 1, 10, 20, 30, DateTimeKind.Local);
var dt2 = new DateTime(2000, 1, 1, 10, 20, 30, DateTimeKind.Utc);
Console.WriteLine(dt1 == dt2); // True

var local = DateTime.Now;
var utc = local.ToUniversalTime();
Console.WriteLine(local == utc); // False

// You can construct a DateTime that differs from another only in Kind with the static DateTime.SpecifyKind method:

var d = new DateTime(2000, 12, 12); // Unspecified
var utc2 = DateTime.SpecifyKind(d, DateTimeKind.Utc);
Console.WriteLine(utc2); // 12/12/2000 12:00:00 AM