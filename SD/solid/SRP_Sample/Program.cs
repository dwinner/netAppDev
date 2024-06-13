using System.Diagnostics;
using SRP_Sample;
using static System.Console;

var journal = new Journal();
journal.AddEntry("I cried today.");
journal.AddEntry("I ate a bug.");
WriteLine(journal);

var p = new PersistenceManager();
var filename = @"c:\temp\journal.txt";
p.SaveToFile(journal, filename);
Process.Start(filename);