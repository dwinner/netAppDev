using Monostate;
using static System.Console;

var ceo = new ChiefExecutiveOfficer
{
   Name = "Adam Smith",
   Age = 55
};

var ceo2 = new ChiefExecutiveOfficer();
WriteLine(ceo2);