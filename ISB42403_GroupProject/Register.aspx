<%@ Page Title="" Language="C#" MasterPageFile="~/Account.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ISB42403_GroupProject.Register" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/RegisterStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Registration Page</h1>
    <p>
    Enter your email:
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    </p>
    <p>
        Enter your username:
        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
    </p>
    <p>
        Enter password:
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
    </p>
    <p>
        <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
    </p>
</asp:Content>
