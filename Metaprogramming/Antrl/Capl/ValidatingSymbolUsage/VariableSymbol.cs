namespace ValidatingSymbolUsage
{
   /// <summary>
   ///    Represents a variable definition (name,type) in symbol table
   /// </summary>
   public class VariableSymbol : Symbol
   {
      public VariableSymbol(string aSymbolName)
         : base(aSymbolName)
      {
      }

      public VariableSymbol(string aSymbolName, BuiltInType aType)
         : base(aSymbolName, aType)
      {
      }
   }
}