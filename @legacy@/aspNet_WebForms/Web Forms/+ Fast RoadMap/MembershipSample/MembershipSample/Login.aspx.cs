﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MembershipSample
{
  public partial class Login : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      string userName = TextBox1.Text;
      string password = TextBox2.Text;
      if (Membership.ValidateUser(userName, password))
      {
        FormsAuthentication.RedirectFromLoginPage(userName, false);
      }
      else
      {
        Label1.Text = "invalid username or password";
      }
    }
  }
}