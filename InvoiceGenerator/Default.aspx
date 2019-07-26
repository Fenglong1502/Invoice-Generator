<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InvoiceGenerator._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Generate Invoice </h1>
    Invoice Number: 
    <asp:TextBox ID="invoiceNumTB" runat="server"></asp:TextBox>
    <br />
    
    <br />
    <asp:Button ID="Button1" runat="server" Text="Generate Invoice" OnClick="Button1_Click" />


</asp:Content>

