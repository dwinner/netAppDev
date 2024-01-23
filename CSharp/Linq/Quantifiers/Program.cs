var query = "Hello".Distinct();
var res = query.SequenceEqual("Helo");
Console.WriteLine(res);

var cntRes = new[] { 2, 3, 4 }.Contains(3);
var anyRes = new[] { 2, 3, 4 }.Any(n => n == 3);
var anyRes2 = new[] { 2, 3, 4 }.Any(n => n > 10);
var otherRes = new[] { 2, 3, 4 }.Any(n => n > 10);
var otherRes2 = new[] { 2, 3, 4 }.All(n => n < 2);