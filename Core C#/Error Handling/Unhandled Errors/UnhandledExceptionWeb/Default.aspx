<%@ Page CodeBehind="Default.aspx.cs" Language="C#" AutoEventWireup="true" Inherits="HowToCSharp.Ch04.UnhandledExceptionWeb.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    <body>
        <form id="unhandleFormId" runat="server">
            <div>
                <asp:Button id="ButtonId" runat="server" text="Throw page exception" 
                    onclick="ButtonIdClick"></asp:Button>
            </div>
        </form>
    </body>
</html>