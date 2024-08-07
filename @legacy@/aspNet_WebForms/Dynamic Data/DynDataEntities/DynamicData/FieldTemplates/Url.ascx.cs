﻿using System;
using System.Web.UI;

namespace DynDataEntities
{
   public partial class UrlField : System.Web.DynamicData.FieldTemplateUserControl
   {
      protected override void OnDataBinding(EventArgs e)
      {
         HyperLinkUrl.NavigateUrl = ProcessUrl(FieldValueString);
      }

      private string ProcessUrl(string url)
      {
         if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
         {
            return url;
         }

         return "http://" + url;
      }

      public override Control DataControl
      {
         get
         {
            return HyperLinkUrl;
         }
      }

   }
}
