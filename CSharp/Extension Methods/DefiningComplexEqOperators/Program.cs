using DefiningComplexEqOperators;

var counter1 = new Counter { Value = 23 };
counter1 += 45;
Console.WriteLine($"counter1.Value: {counter1.Value}"); // 68