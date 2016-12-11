using System;
using System.ComponentModel.Composition;

namespace CalculatorUtils
{
   [MetadataAttribute]
   [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
   public class SpeedExportAttribute : ExportAttribute
   {
      public Speed Speed { get; set; }

      public SpeedExportAttribute(string contractName, Type contractType)
         : base(contractName, contractType)
      {
      }
   }
}