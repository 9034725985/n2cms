<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="N2.Templates.UI.Layouts.Parts.Menu" %>
<n2:Display ID="dt" runat="server" PropertyName="Title" />
<n2:Box runat="server">
    <n2:Menu runat="server" CssClass="avmenu" />
</n2:Box>