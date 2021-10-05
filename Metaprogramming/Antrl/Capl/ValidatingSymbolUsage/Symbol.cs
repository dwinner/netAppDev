namespace ValidatingSymbolUsage
{
   /// <summary>
   ///    A generic programming language symbol
   /// </summary>
   public class Symbol
   {
      public Symbol(string aSymbolName) => Name = aSymbolName;

      public Symbol(string aSymbolName, BuiltInType aType)
         : this(aSymbolName) =>
         Type = aType;

      /// <summary>
      ///    Symbol name
      /// </summary>
      public string Name { get; }

      public BuiltInType Type { get; }

      /// <summary>
      ///    All symbols know what scope contains them
      /// </summary>
      public IScope Scope { get; set; }

      public override string ToString() => Type != BuiltInType.Invalid
         ? $"{'<'}{Name}:{Type}>"
         : Name;

      public static Symbol Null => new Symbol(string.Empty, BuiltInType.Invalid);
   }
}