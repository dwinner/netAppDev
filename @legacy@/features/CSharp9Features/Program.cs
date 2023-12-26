// The current Main is (maybe) async and the top level here

using System;
using CSharp9Features;

UsingRecords();
UsingPatterns();
UsingNativeInt();

foreach (var prArg in args)
{
   Console.WriteLine(prArg);
}

// return 0;

#region Records

static void UsingRecords()
{
   var phoneNumbers = new string[2];

   // position syntax and property initializers
   PersonRecord person1 = new("Nancy", "Davolio")
   {
      PhoneNumbers = phoneNumbers
   };
   var person2 = new PersonRecord
   {
      FirstName = "Nancy",
      LastName = "Davolio",
      PhoneNumbers = phoneNumbers
   };

   Console.WriteLine(person1 == person2); // True

   person1.PhoneNumbers[0] = "555-1234";
   Console.WriteLine(person1 == person2); // True

   Console.WriteLine(ReferenceEquals(person1, person2)); // False

   // constructing from existing
   var person3 = person1 with {FirstName = "John"};
   Console.WriteLine(person3);
}

#endregion

#region Pattern matching improvements

void UsingPatterns()
{
   var test = '7';
   Console.WriteLine(IsLetter(test));

   Console.WriteLine(Classify(13)); // output: Too high
   Console.WriteLine(Classify(double.NaN)); // output: Unknown
   Console.WriteLine(Classify(2.4)); // output: Acceptable
}

static bool IsLetter(char aChar)
{
   var isChar = aChar is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or '.' or ',' and not 'r';
   return isChar;
}

static string Classify(double measurement) => measurement switch
{
   < -40.0 => "Too low",
   >= -40.0 and < 0 => "Low",
   >= 0 and < 10.0 => "Acceptable",
   >= 10.0 and < 20.0 => "High",
   >= 20.0 => "Too high",
   //not 40.0 => "",
   double.NaN => "Unknown"
};

#endregion

#region Using natives

static unsafe void UsingNativeInt()
{
   nint a = 1;
   Console.WriteLine(a);
   Console.WriteLine(sizeof(nint));

   nuint b = 1;
   Console.WriteLine(b);
   Console.WriteLine(sizeof(nuint));
}

#endregion