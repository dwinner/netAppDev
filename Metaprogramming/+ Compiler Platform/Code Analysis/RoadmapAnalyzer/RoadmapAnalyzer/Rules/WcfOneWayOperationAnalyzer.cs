using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RoadmapAnalyzer.Rules
{
   [DiagnosticAnalyzer(LanguageNames.CSharp)]
   public class WcfOneWayOperationAnalyzer : DiagnosticAnalyzer
   {
      public const string DiagnosticId = "WcfOneWayOperationAnalyzer";
      internal static readonly LocalizableString Title = "WcfOneWayOperationAnalyzer Title";
      internal static readonly LocalizableString MessageFormat = "WcfOneWayOperationAnalyzer '{0}'";
      internal const string Category = "WcfOneWayOperationAnalyzer Category";

      internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

      public override void Initialize(AnalysisContext context)
      {
      }
   }
}