<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="testStrana.aspx.cs" Inherits="PlagijatorFinder.testStrana" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <style type="text/css">
        .style3
        {
            width: 589px;
        }
        .style4
        {
            width: 31%;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
            oncheckedchanged="CheckBox1_CheckedChanged" 
            Text="Slicnost izmedju rada i svih ostalih" />
        <br __designer:mapid="18d" />
        <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" 
            oncheckedchanged="CheckBox2_CheckedChanged" Text="Slicnost izmedju dva rada" />
        <br __designer:mapid="18e" />
        <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="True" 
            oncheckedchanged="CheckBox3_CheckedChanged" 
            Text="Slicnost izmedju radova iste kategorije" />
        <br __designer:mapid="18f" />
    <asp:Button ID="testButton" runat="server" Text="testConv" 
        onclick="testButton_Click" Width="124px" CssClass="submitButton" />
    <br __designer:mapid="191" />
    <br __designer:mapid="192" />
        <br __designer:mapid="193" />
        <br __designer:mapid="194" />
        <table style="width:63%; border-color="FF9077"; border="1" 
            __designer:mapid="195">
            <tr __designer:mapid="196">
                <td class="style1" __designer:mapid="197">
                    <asp:Label ID="Label1" runat="server" Text="prvi Rad"></asp:Label>
                </td>
                <td __designer:mapid="199">
                    <asp:Label ID="Label2" runat="server" Text="drugi Rad"></asp:Label>
                </td>
            </tr>
            <tr __designer:mapid="19b">
                <td class="style1" __designer:mapid="19c">
                    &nbsp;</td>
                <td style="text-align: left" __designer:mapid="19f">
                    &nbsp;</td>
            </tr>
            <tr __designer:mapid="1a1">
                <td colspan="2" style="text-align: center" __designer:mapid="1a2">
                    <asp:Label ID="resultLabel" runat="server" 
                        style="font-size: large; text-align: center;"></asp:Label>
                </td>
            </tr>
        </table>
        <br __designer:mapid="1a4" />
        <asp:Label ID="compareLabel" runat="server"></asp:Label>
        <br __designer:mapid="1a6" />
</p>
    <asp:Panel ID="Panel1" runat="server">
        Izaberite fajl za proveru slicnosti sa svim ostalim fajlovima<br />
        <table style="width:100%;">
            <tr>
                <td class="style3">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="Name" RepeatColumns="5" 
                        Width="98%">
                    </asp:RadioButtonList>
                    <br />
                    <asp:Button ID="panel1Button" runat="server" onclick="panel1Button_Click" 
                        Text="Proveri" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
        Izaberite dva fajla za proveru slicnosti:<br /> <br />
        <table style="width:100%;">
            <tr>
                <td style="text-align: left" width="25%">
                    Prvi rad:<br />
                    <asp:DropDownList ID="DropDownList1" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name">
                    </asp:DropDownList>
                </td>
                <td  style="text-align: left" width="25%">
                    Drugi rad:<br />
                    <asp:DropDownList ID="DropDownList2" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name">
                    </asp:DropDownList>
                  
                </td>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:bazaRadovaConnectionString2 %>" 
                        ProviderName="<%$ ConnectionStrings:bazaRadovaConnectionString2.ProviderName %>" 
                        SelectCommand="SELECT [Name] FROM [Rad]"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="text-align: left" class="style4">
                    <asp:Button ID="panel2Button" runat="server" onclick="panel2Button_Click" 
                        Text="Proveri" />
                </td>
                <td align="left" style="text-align: left" width="25%">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    <asp:Panel ID="Panel3" runat="server">
        <asp:Button ID="panel3Button" runat="server" Text="Proveri" 
            onclick="panel3Button_Click" />
    </asp:Panel>
    <p>
        <asp:Literal ID="resultLiteral" runat="server"></asp:Literal>
        <br __designer:mapid="1a7" />
</p>
    <p>
    <br />
</p>
</asp:Content>
