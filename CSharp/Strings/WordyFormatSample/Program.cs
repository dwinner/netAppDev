using WordyFormatSample;

const double n = -123.45;
IFormatProvider fp = new WordyFormatProvider();
Console.WriteLine(string.Format(fp, "{0:C} in words is {0:W}", n));