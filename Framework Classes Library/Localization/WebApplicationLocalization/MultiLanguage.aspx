<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiLanguage.aspx.cs" Inherits="WebApplication.MultiLanguage"
   Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title></title>
</head>
<body>
   <form id="form1" runat="server">
      <div>

         <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label>
         <br />
         <br />
         <asp:Button ID="Button1" runat="server" Text="Button" meta:resourcekey="Button1Resource1" OnClick="Button1_Click" />

         <br />
         <br />
         <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Messages, Greeting %>"></asp:Label>

      </div>
   </form>
</body>
</html>
