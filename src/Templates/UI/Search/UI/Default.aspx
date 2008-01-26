<%@ Page Language="C#" MasterPageFile="Layouts/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="N2.Templates.Search.UI.Default" Title="Untitled Page" %>
<asp:Content ID="c" ContentPlaceHolderID="Content" runat="server">
    <n2:Path ID="p1" runat="server" />
    <n2:Display PropertyName="Title" runat="server" />
    <n2:Display PropertyName="Text" runat="server" />
    
    <asp:TextBox ID="txtQuery" runat="server"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
    
    <asp:Label Text='<%# string.Format("{0} pages found.", Hits.Count) %>' runat="server" />
    <asp:Repeater ID="rptHits" runat="server" DataSource='<%# Hits %>'>
        <HeaderTemplate><div class="list"></HeaderTemplate>
        <ItemTemplate>
            <div class="item hit cf i<%# Container.ItemIndex %> a<%# Container.ItemIndex % 2 %>">
                <a href='<%# Eval("Url") %>'><%# Eval("Title") %></a>
            </div>
        </ItemTemplate>
        <FooterTemplate></div></FooterTemplate>
    </asp:Repeater>
</asp:Content>
