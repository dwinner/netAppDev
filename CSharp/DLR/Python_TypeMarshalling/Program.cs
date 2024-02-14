using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting;

var code = """sb.Append ("World")""";
var engine = Python.CreateEngine();
var scope = engine.CreateScope();
var sb = new StringBuilder("Hello");
scope.SetVariable("sb", sb);
var source = engine.CreateScriptSourceFromString(code, SourceCodeKind.SingleStatement);
source.Execute(scope);
var s = sb.ToString();
Console.WriteLine(s);