﻿using System;
using System.Web;

namespace PsCachingWebVia.Account
{
   public partial class Login : System.Web.UI.Page
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         RegisterHyperLink.NavigateUrl =
            $"Register.aspx?ReturnUrl={HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"])}";
      }
   }
}
