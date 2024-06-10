using System;
using System.Collections.Generic;

namespace Decorator
{
   public sealed class SupportedProjectItem : ProjectDecorator
   {
      public SupportedProjectItem(string newSupportingDocument)
      {
         AddSupportingDocument(newSupportingDocument);
      }

      private IList<string> SupportingDocuments { get; } = new List<string>();

      private void AddSupportingDocument(string newSupportingDocument)
      {
         if (!SupportingDocuments.Contains(newSupportingDocument))
         {
            SupportingDocuments.Add(newSupportingDocument);
         }
      }

      public override string ToString()
         => $"{ProjectItem}{Environment.NewLine}\tSupporting Documents: ";
   }
}