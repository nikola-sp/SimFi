<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="PlagijatorFinder._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style3
        {
            text-align: center;
        }
        .style4
        {
            height: 21px;
            text-align: left;
        }
        .style10
        {
            font-size: medium;
        }
    .style11
    {
        width: 219px;
    }
    </style>
<%--    <script language="javascript">
        // javascript to add to your aspx page
        function ValidateModuleList(source, args) {
            var chkListModules = document.getElementById('<%= CheckBoxList1.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) 
                {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>--%>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel ID="Panel1" runat="server" Height="387px" Width="556px" 
    style="font-size: large">
        <div class="style3">
            <strong>FORMA ZA UNOS RADOVA</strong>
            <br />
        </div>
        <table bgcolor="White" border="1" style="width:100%;">
            <tr>
                <td class="style4" colspan="3" align="left">
                    <asp:FileUpload ID="FileUpload1" runat="server" style="text-align: left" 
                        Width="219px" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="statusLabel" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="nameLabel" runat="server" Text="Naziv:" CssClass="style10"></asp:Label>
                </td>
                <td class="style11">
                    <asp:TextBox ID="fileNameTextBox" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="fileNameTextBox" ErrorMessage="Ime fajla nije uneto"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="godinaLabel" runat="server" CssClass="style10" 
                        style="text-align: center" Text="Godina: "></asp:Label>
                </td>
                <td class="style11">
                    <asp:TextBox ID="godinaTextBox" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" 
                        ControlToValidate="godinaTextBox" CssClass="style10" 
                        ErrorMessage="Godina nije pravilno uneta" MaximumValue="2014" 
                        MinimumValue="1888" style="color: #CC0000" Type="Integer"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="nameLabel0" runat="server" CssClass="style10" Text="Autor:"></asp:Label>
                </td>
                <td class="style11">
                    <asp:TextBox ID="AutorTextBox" runat="server" Width="200px" 
                        ToolTip="Ukoliko postoji više autora, njihova imena razdvajati znakom ;"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="Label1" runat="server" Text="Kategorije:" CssClass="style10"></asp:Label>
                </td>
                <td class="style11">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name" 
                        style="font-size: x-small; color: #0066FF" Width="221px">
                    </asp:CheckBoxList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:bazaRadovaConnectionString2 %>" 
                        SelectCommand="SELECT [Name] FROM [Kategorija]" 
                        ProviderName="<%$ ConnectionStrings:bazaRadovaConnectionString.ProviderName %>"></asp:SqlDataSource>
                    <br />
                    <asp:ImageButton ID="BtnDodajKategoriju" runat="server" 
                        onclick="BtnDodajKategoriju_Click" Height="17px" ImageUrl="~/add.jpg" 
                        Width="19px" />
                </td>
                <td>

                    <asp:TextBox ID="KategorijaTextBox" runat="server" Visible="False" 
                        Width="200px"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnSnimiKategoriju" runat="server" Height="30px" 
                        onclick="snimiKategorijuButton_Click" Text="Dodaj kategoriju" Visible="False" 
                        Width="137px" />

                </td>
                
                </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style11">
                    &nbsp;</td>
                <td>
                    <asp:Label ID="checkBoxListLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3" colspan="3">
                    <asp:Button ID="saveAndConvertButton" runat="server" Height="30px" 
                        onclick="saveAndConvertButton_Click" Text="Unesi rad" Width="137px" />
                </td>
            </tr>
        </table>
        <asp:Label ID="Label2" runat="server"></asp:Label>
        <br />
        <asp:Label ID="resultLabel" runat="server"></asp:Label>
    </asp:Panel>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
