<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TabelaSlicnosti.aspx.cs" Inherits="PlagijatorFinder.TabelaSlicnosti" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="countSimBtn" runat="server" onclick="countSimBtn_Click" 
    Text="IzracunajSlicnost" />
&nbsp;&nbsp;&nbsp;
<asp:Button ID="showTableButton" runat="server" onclick="showButton_Click" 
    Text="Prikazi tabelu" />
&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Label ID="statusLbl" runat="server"></asp:Label>
<br />
<br />
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
<br />
<br />
</asp:Content>
