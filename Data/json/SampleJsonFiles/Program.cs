const string personJson = """
                          {
                          	"FirstName":"Sara",
                          	"LastName":"Wells",
                          	"Age":35,
                          	"Friends":["Dylan","Ian"]
                          }
                          """;

const string personPath = "Person.json";
if (!File.Exists("personPath"))
{
   File.WriteAllText(personPath, personJson);
}

const string personArrayJson = """
                               [
                               	{
                               		"FirstName":"Sara",
                               		"LastName":"Wells",
                               		"Age":35,
                               		"Friends":["Ian"]
                               	},
                               	{
                               		"FirstName":"Ian",
                               		"LastName":"Weems",
                               		"Age":42,
                               		"Friends":["Joe","Eric","Li"]
                               	},
                               	{
                               		"FirstName":"Dylan",
                               		"LastName":"Lockwood",
                               		"Age":46,
                               		"Friends":["Sara","Ian"]
                               	}
                               ]
                               """;

const string personArrayPath = "PersonArray.json";
if (!File.Exists(personArrayPath))
{
   File.WriteAllText(personArrayPath, personArrayJson);
}