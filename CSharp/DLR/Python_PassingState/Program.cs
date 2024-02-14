// The following string could come from a file or database:

using IronPython.Hosting;
using Microsoft.Scripting;

var auditRule = "taxPaidLastYear / taxPaidThisYear > 2";
var engine = Python.CreateEngine();
var scope = engine.CreateScope();
scope.SetVariable("taxPaidLastYear", 20000m);
scope.SetVariable("taxPaidThisYear", 8000m);
var source = engine.CreateScriptSourceFromString(auditRule, SourceCodeKind.Expression);
var auditRequired = (bool)source.Execute(scope);

Console.WriteLine(auditRequired); // True