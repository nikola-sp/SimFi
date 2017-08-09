<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Uporedjivanje.aspx.cs" Inherits="PlagijatorFinder.Uporedjivanje" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 603px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
        <tr>
            <td class="style1">
                <asp:Label ID="lblA" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblRepeat" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="lblB" class="style1">
                <asp:Label ID="lblB" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTime" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lblDiff" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSpace" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <br />
                <asp:DropDownList ID="DropDownList1" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name">
                </asp:DropDownList>
                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:bazaRadovaConnectionString2 %>" 
                    SelectCommand="SELECT [Name] FROM [Rad] order by id desc" 
                    
                    ProviderName="<%$ ConnectionStrings:bazaRadovaConnectionString2.ProviderName %>"></asp:SqlDataSource>
            </td>
            <td>
                <asp:Button ID="uporediButton" runat="server" onclick="uporediButton_Click" 
                    Text="Uporedi" CssClass="submitButton" />
            </td>
        </tr>
    </table>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <br />
</asp:Content>
