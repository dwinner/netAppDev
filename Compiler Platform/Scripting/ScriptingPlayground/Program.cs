using System;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Nito.AsyncEx;
using ScriptingContext;
using static System.Console;

namespace ScriptingPlayground
{
   internal static class Program
   {
      private static void Main()
      {
         AsyncContext.Run(MainAsync);
      }

      private static async Task MainAsync() => await ExecuteScriptsWithGlobalContextAsync().ConfigureAwait(false);

      private static async Task EvaluateCodeAsync()
      {
         Out.WriteLine("Enter in your script:");
         var code = In.ReadLine();
         Out.WriteLine(
            await CSharpScript.EvaluateAsync(code).ConfigureAwait(false));
      }

      private static async Task EvaluateCodeWithContextAsync()
      {
         Out.WriteLine("Enter in your script:");
         var code = In.ReadLine();
         Out.WriteLine(
            await CSharpScript.EvaluateAsync(code, ScriptOptions.Default
               .AddReferences(typeof (Context).Assembly)
               .AddImports(typeof (Context).Namespace)).ConfigureAwait(false));
      }

      private static async Task EvaluateCodeWithGlobalContextAsync()
      {
         Out.WriteLine("Enter in your script:");
         var code = In.ReadLine();
         Out.WriteLine(
            await
               CSharpScript.EvaluateAsync(code, globals: new CustomContext(new Context(4), Out))
                  .ConfigureAwait(false));
      }

      private static async Task CompileScriptAsync()
      {
         Out.WriteLine("Enter in your script:");
         var code = In.ReadLine();
         var script = CSharpScript.Create(code);
         var compilation = script.GetCompilation();
         var diagnostics = compilation.GetDiagnostics();

         if (diagnostics.Length > 0)
         {
            foreach (var diagnostic in diagnostics)
            {
               Out.WriteLine(diagnostic);
            }
         }
         else
         {
            foreach (var tree in compilation.SyntaxTrees)
            {
               var model = compilation.GetSemanticModel(tree);
               foreach (var node in tree.GetRoot().DescendantNodes(_ => true))
               {
                  var symbol = model.GetSymbolInfo(node).Symbol;
                  Out.WriteLine(
                     $"{node.GetType().Name} {node.GetText()}");

                  if (symbol != null)
                  {
                     var symbolKind = Enum.GetName(typeof (SymbolKind), symbol.Kind);
                     Out.WriteLine(
                        $"\t{symbolKind} {symbol.Name}");
                  }
               }
            }

            Out.WriteLine((await script.RunAsync().ConfigureAwait(false)).ReturnValue);
         }
      }

      private static async Task ExecuteScriptsWithStateAsync()
      {
         Out.WriteLine("Enter in your script - type \"STOP\" to quit:");

         ScriptState<object> state = null;

         while (true)
         {
            var code = In.ReadLine();

            if (code == "STOP")
            {
               break;
            }

            state = state == null
               ? await CSharpScript.RunAsync(code).ConfigureAwait(false)
               : await state.ContinueWithAsync(code).ConfigureAwait(false);

            foreach (var variable in state.Variables)
            {
               Out.WriteLine(
                  $"\t{variable.Name} - {variable.Type.Name}");
            }

            if (state.ReturnValue != null)
            {
               Out.WriteLine(
                  $"\tReturn value: {state.ReturnValue}");
            }
         }
      }

      private static async Task ExecuteScriptsWithGlobalContextAsync()
      {
         WriteLine("Enter in your script - type \"STOP\" to quit:");
         var session = new DictionaryContext();
         while (true)
         {
            var code = In.ReadLine();
            if (code == "STOP")
            {
               break;
            }

            var result = await CSharpScript.RunAsync(code, globals: session).ConfigureAwait(false);
            if (result.ReturnValue != null)
            {
               WriteLine($"\t{result.ReturnValue}");
            }
         }
      }
   }
}