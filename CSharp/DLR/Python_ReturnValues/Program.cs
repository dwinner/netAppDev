using IronPython.Hosting;
using Microsoft.Scripting;

var code = "result = input * 3";
var engine = Python.CreateEngine();
var scope = engine.CreateScope();
scope.SetVariable("input", 2);
var source = engine.CreateScriptSourceFromString(code, SourceCodeKind.SingleStatement);
source.Execute(scope);
Console.WriteLine(scope.GetVariable("result")); // 6