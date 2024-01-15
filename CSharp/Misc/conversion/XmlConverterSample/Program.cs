// XmlConvert honors XML formatting rules:

using System.Xml;

var s = XmlConvert.ToString(true);
Console.WriteLine(s); // true (rather than True)
Console.WriteLine(XmlConvert.ToBoolean(s));

var dt = DateTime.Now;
var local = XmlConvert.ToString(dt, XmlDateTimeSerializationMode.Local);
Console.WriteLine(local);

var utc = XmlConvert.ToString(dt, XmlDateTimeSerializationMode.Utc);
Console.WriteLine(utc);

var roundtripKind = XmlConvert.ToString(dt, XmlDateTimeSerializationMode.RoundtripKind);
Console.WriteLine(roundtripKind);

var now = XmlConvert.ToString(DateTimeOffset.Now);
Console.WriteLine(now);