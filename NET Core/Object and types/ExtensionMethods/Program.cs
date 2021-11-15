using System;
using ExtensionsForString;

string fox = "the quick brown fox jumped over the lazy dogs";
var wordCount = fox.GetWordCount();
Console.WriteLine($"{wordCount} words");
Console.ReadLine();