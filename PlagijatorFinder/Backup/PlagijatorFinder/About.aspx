<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="PlagijatorFinder.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        About
    </h2>
    <p>
    </p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Label ID="Label1" runat="server" Text="Time in panel:"></asp:Label>
            &nbsp;<asp:Label ID="Label2" runat="server"></asp:Label>
            <br />
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                Text="timePanel" />
<br />
<br />
            <asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="Timer1_Tick">
            </asp:Timer>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Time out of panel:"></asp:Label>
&nbsp;
    <asp:Label ID="Label4" runat="server"></asp:Label>
    <br />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="timeOutOfPanel" />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
