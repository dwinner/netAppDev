string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

var res1 = names.MaxBy(n => n.Length);
var res2 = names.MinBy(n => n.Length);

// If the sequence is empty, it returns null:
var res3 = names.Take(0).MaxBy(n => n.Length);

Console.WriteLine(res1);
Console.WriteLine(res2);
Console.WriteLine(res3);