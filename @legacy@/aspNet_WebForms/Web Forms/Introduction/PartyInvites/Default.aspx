﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PartyInvites.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Join the party</title>
    <link rel="stylesheet" href="PartyStyles.css" />
</head>
<body>
    <form id="RsvpForm" runat="server">
        <div>
            <h1>New Year's Eve at Jacqui's!</h1>
            <p>We're going to have an exciting party. And you're invited</p>
        </div>
        <asp:ValidationSummary ID="ValidationSummary" runat="server"
            ShowModelStateErrors="True" />
        <div>
            <label>Your name:</label>
            <input type="text" id="name" runat="server" />
        </div>
        <div>
            <label>Your email:</label>
            <input type="text" id="email" runat="server" />
        </div>
        <div>
            <label>Your phone:</label>
            <input type="text" id="phone" runat="server" />
        </div>
        <div>
            <label>Will you attend?</label>
            <select id="willattend" runat="server">
                <option value="">Choose an Option</option>
                <option value="true">Yes</option>
                <option value="false">No</option>
            </select>
        </div>
        <div>
            <button type="submit">Submit RSVP</button>
        </div>
    </form>
</body>
</html>
