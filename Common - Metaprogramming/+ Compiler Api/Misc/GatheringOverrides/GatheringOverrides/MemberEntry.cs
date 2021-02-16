using System;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GatheringOverrides
{
   public struct MemberEntry
   {
      internal string Signature { get; set; }

      internal string Summary { get; set; }

      internal Lazy<MemberDeclarationSyntax> Code { get; set; }

      public override string ToString()
      {
         var builder = new StringBuilder();
         builder.AppendLine($"{nameof(Signature)}: {Signature}");
         builder.AppendLine($"{nameof(Summary)}: {Summary}");
         builder.AppendLine("Code:");
         builder.AppendLine(Code.Value.ToString());

         return builder.ToString();
      }
   }
}