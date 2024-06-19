using System.Text;
using System.Xml.Serialization;
using PropertySurrogate;

var stats = new CountryStats();
stats.Capitals.Add("France", "Paris");
var xs = new XmlSerializer(typeof(CountryStats));
var stringBuilder = new StringBuilder();
var stringWriter = new StringWriter(stringBuilder);
xs.Serialize(stringWriter, stats);

var newStats = (CountryStats)xs.Deserialize(new StringReader(stringWriter.ToString()))!;
Console.WriteLine(newStats.Capitals["France"]);