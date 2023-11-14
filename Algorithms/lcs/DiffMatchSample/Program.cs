using System.Text;
using DiffMatchPatch;

var before = File.ReadAllText("before.can", Encoding.UTF8);
var after = File.ReadAllText("after.can", Encoding.UTF8);

var dmp = new diff_match_patch();
var differences = dmp.diff_main(before, after);
dmp.diff_cleanupSemantic(differences);
foreach (var diff in differences)
{
   var diffOperation = diff.operation;
   var changedText = diff.text;
   switch (diffOperation)
   {
      case Operation.DELETE:
         Console.WriteLine("Deleted:");
         var indexOf = before.IndexOf(changedText, StringComparison.Ordinal);
         Console.WriteLine(changedText);
         break;

      case Operation.INSERT:
         Console.WriteLine("Inserted:");
         Console.WriteLine(changedText);
         break;
   }
}