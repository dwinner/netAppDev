using System.Collections.Frozen;

// Frozen collections are great for lookups that are initialized at the start of
// a program and then used throughout the life of the application.

var andCode = Disassembler.OpCodeLookup["AND"];
Console.WriteLine(andCode);

internal class Disassembler
{
   public static readonly IReadOnlyDictionary<string, string> OpCodeLookup =
      new Dictionary<string, string>
         {
            { "ADC", "Add with Carry" },
            { "ADD", "Add" },
            { "AND", "Logical AND" },
            { "ANDN", "Logical AND NOT" }
         }
         .ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);
}