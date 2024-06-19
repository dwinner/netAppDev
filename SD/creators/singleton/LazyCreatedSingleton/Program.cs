using LazyCreatedSingleton;
using static System.Console;

var db = SingletonDatabase.Instance;

// works just fine while you're working with a real database.
var city = "Tokyo";
WriteLine($"{city} has population {db.GetPopulation(city)}");