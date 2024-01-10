var x = 150;
if (x is > 100)
{
   Console.WriteLine("x is greater than 100");
}

var bmi1 = GetWeightCategory(15);
var bmi2 = GetWeightCategory(20);
var bmi3 = GetWeightCategory(25);

return;

string GetWeightCategory(decimal bmi) => bmi switch
{
   < 18.5m => "underweight",
   < 25m => "normal",
   < 30m => "overweight",
   _ => "obese"
};