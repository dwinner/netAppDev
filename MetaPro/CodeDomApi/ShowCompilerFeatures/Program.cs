using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ShowCompilerFeatures
{
   internal static class Program
   {
      private static void Main()
      {
         var integratedFeatures = CodeDomProvider.GetAllCompilerInfo()
            .Aggregate(string.Empty, (current, compilerInfo) => current + GetCompilerInfo(compilerInfo));
         Console.WriteLine(integratedFeatures);
      }

      private static string GetCompilerInfo(CompilerInfo compilerInfo)
      {
         var output = new StringBuilder();
         var language = compilerInfo.GetLanguages()[0];
         output.AppendFormat("{0} features{1}", language, Environment.NewLine);

         try
         {
            var provider = CodeDomProvider.CreateProvider(language);
            output.AppendFormat("CaseInsensitive = {0}{1}",
               provider.LanguageOptions.HasFlag(LanguageOptions.CaseInsensitive), Environment.NewLine);
            var genSupport = Enum.GetValues(typeof(GeneratorSupport))
               .Cast<GeneratorSupport>()
               .Aggregate(string.Empty,
                  (current, supportableFeature) =>
                        current + $"{supportableFeature} = {provider.Supports(supportableFeature)}{Environment.NewLine}");
            output.Append(genSupport);
         }
         catch (ConfigurationErrorsException configErrorsEx)
         {
            output.Append(configErrorsEx.Message);
         }

         return output.ToString();
      }
   }
}