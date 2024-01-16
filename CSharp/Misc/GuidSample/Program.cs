var g = Guid.NewGuid();
var s = g.ToString();
Console.WriteLine(s);

var g1 = new Guid("{0d57629c-7d6e-4847-97cb-9e2fc25083fe}");
var g2 = new Guid("0d57629c7d6e484797cb9e2fc25083fe");
Console.WriteLine(g1 == g2); // True

var bytes = g.ToByteArray();
var g3 = new Guid(bytes);
Console.WriteLine(g3);

Console.WriteLine(Guid.Empty);
Console.WriteLine(default(Guid));
var guidBytes = Guid.Empty.ToByteArray();
Array.ForEach(guidBytes, @byte => Console.WriteLine(@byte));