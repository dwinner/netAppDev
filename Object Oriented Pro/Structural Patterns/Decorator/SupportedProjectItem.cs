using System;
using System.Collections.Generic;

namespace Decorator
{
   public class SupportedProjectItem : ProjectDecorator
   {
      private readonly IList<string> _supportingDocuments = new List<string>();

      public IList<string> SupportingDocuments
      {
         get { return _supportingDocuments; }
      }

      public SupportedProjectItem() { }

      public SupportedProjectItem(string newSupportingDocument)
      {
         AddSupportingDocument(newSupportingDocument);
      }

      private void AddSupportingDocument(string newSupportingDocument)
      {
         if (!SupportingDocuments.Contains(newSupportingDocument))
            SupportingDocuments.Add(newSupportingDocument);
      }

      public void RemoveSupportingDocument(string document)
      {
         SupportingDocuments.Remove(document);
      }

      public override string ToString()
      {
         return string.Format("{0}{1}\tSupporting Documents: ",
            ProjectItem,
            Environment.NewLine);
      }
   }
}
