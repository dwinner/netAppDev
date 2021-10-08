using System.Text;

namespace ValidatingSymbolUsage
{
   /// <summary>
   ///    A generic programming language symbol
   /// </summary>
   public class Symbol
   {
      protected Symbol(string aSymbolName)
         : this(aSymbolName, BuiltInType.Invalid) =>
         Name = aSymbolName;

      protected Symbol(string aSymbolName, BuiltInType aType)
         : this(aSymbolName, aType, string.Empty) =>
         Type = aType;

      protected Symbol(string aSymbolName, BuiltInType aType, string anUserDefinedType)
         : this(aSymbolName, aType, anUserDefinedType, null)
      {
         Name = aSymbolName;
         Type = aType;
         UserDefinedType = anUserDefinedType;
      }

      protected Symbol(string aSymbolName, BuiltInType aType, string anUserDefinedType, IScope aScope)
      {
         Name = aSymbolName;
         Type = aType;
         UserDefinedType = anUserDefinedType;
         Scope = aScope;
      }

      /// <summary>
      ///    Symbol name
      /// </summary>
      public string Name { get; }

      public BuiltInType Type { get; }

      public string UserDefinedType { get; init; }

      /// <summary>
      ///    All symbols know what scope contains them
      /// </summary>
      public IScope Scope { get; set; }

      public static Symbol Null => new(string.Empty, BuiltInType.Invalid);

      public override string ToString()
      {
         var debugInfo = new StringBuilder()
            .Append($"Symbol name: {Name}. ")
            .Append($"Symbol type: {Type}. ")
            .Append($"Scope: {Scope?.ScopeName}. ");
         if (!string.IsNullOrEmpty(UserDefinedType))
         {
            debugInfo.AppendFormat("User defined type: {0}.", UserDefinedType);
         }

         return debugInfo.ToString();
      }
   }
}