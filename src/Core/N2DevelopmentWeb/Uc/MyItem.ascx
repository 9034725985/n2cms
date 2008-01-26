<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyItem.ascx.cs" Inherits="N2.TemplateWeb.Uc.MyItem" %>
<strong><%= CurrentItem.Title %></strong>
<ul style="border:solid <%# IsHighlighted ? "3" : "1" %>px red">
    <li>
        <%# DateTime.Now.ToString("hh:MM:ss") %><br />
        <n2:EditableDisplay PropertyName="Text" runat="server" />
        <asp:Label runat="server" ID="label2" Text="0" />
		<asp:Button ID="btnPostback" runat="server" Text="PostBack" OnClick="btnPostback_Click" /><ul>
            <N2:DroppableZone ID="SubDataContainer" ZoneName="Main" runat="server" />
        </ul>
    </li>
</ul>