using System;

ColorSamples();
DaysOfWeekSamples();
UsingEnumClass();

Console.ReadLine();

void UsingEnumClass()
{
   if (Enum.TryParse("Red", out Color red))
   {
      Console.WriteLine($"successfully parsed {red}");
   }

   var redtext = Enum.GetName(typeof(Color), red);
   Console.WriteLine(redtext);

   foreach (var day in Enum.GetNames(typeof(Color)))
   {
      Console.WriteLine(day);
   }

   foreach (short val in Enum.GetValues(typeof(Color)))
   {
      Console.WriteLine(val);
   }

   foreach (var item in Enum.GetValues(typeof(Color)))
   {
      Console.WriteLine(item);
   }
}

void DaysOfWeekSamples()
{
   const DaysOfWeek mondayAndWednesday = DaysOfWeek.Monday | DaysOfWeek.Wednesday;
   Console.WriteLine(mondayAndWednesday);

   const DaysOfWeek weekend = DaysOfWeek.Saturday | DaysOfWeek.Sunday;
   Console.WriteLine(weekend);

   const DaysOfWeek workday = DaysOfWeek.Monday | DaysOfWeek.Tuesday | DaysOfWeek.Wednesday | DaysOfWeek.Thursday
                              | DaysOfWeek.Friday;
   Console.WriteLine(workday);
}

void ColorSamples()
{
   const Color c1 = Color.Red;
   Console.WriteLine(c1);

   const Color c2 = (Color)2;
   Console.WriteLine(c2);
   Console.WriteLine((short)c2);
}