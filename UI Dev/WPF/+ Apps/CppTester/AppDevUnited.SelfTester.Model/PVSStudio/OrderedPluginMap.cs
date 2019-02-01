using System.Collections.Generic;

namespace AppDevUnited.SelfTester.Model.PVSStudio
{
   public sealed class OrderedPluginMap
   {
      #region Индексаторы

      public int this[string dataColumnName]
      {
         get
         {
            return PluginDiffUtilities.FindIndexByColumn(PluginDiffUtilities.PvsIndexNameMap, dataColumnName);
         }
      }

      #endregion

      public object[] RetrieveKeyValues(IEnumerable<object> itemArray/*, TableType tableType = TableType.Pvs*/)
      {
         return PluginDiffUtilities.ValuesByIndexes(itemArray, PluginDiffUtilities.PvsExtendedPrimaryKeysPvsMode);
      }
   }
}