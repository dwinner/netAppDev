using System;
using System.Collections.Generic;

namespace Decorator
{
   public sealed class SupportedProjectItem : ProjectDecorator
   {
	   private IList<string> SupportingDocuments { get; } = new List<string>();

	   public SupportedProjectItem(string newSupportingDocument)
      {
         AddSupportingDocument(newSupportingDocument);
      }

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
