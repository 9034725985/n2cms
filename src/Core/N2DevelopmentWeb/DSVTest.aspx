<%@ Page MasterPageFile="~/DefaultMasterPage.Master" Language="C#" AutoEventWireup="true" CodeBehind="DSVTest.aspx.cs" Inherits="N2.TemplateWeb.DSVTest" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div><n2:Display ID="Display2" runat="server" PropertyName="Text"></n2:Display></div>
    
    <asp:DropDownList ID="ddlPaths" runat="server" OnSelectedIndexChanged="ddlPaths_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>..</asp:ListItem>
        <asp:ListItem>/</asp:ListItem>
        <asp:ListItem>test</asp:ListItem>
        <asp:ListItem>../test</asp:ListItem>
        <asp:ListItem>//test8</asp:ListItem>
        <asp:ListItem>/test8</asp:ListItem>
        <asp:ListItem>//</asp:ListItem>
    </asp:DropDownList>
    <N2:ItemDataSource ID="ids" runat="server" 
        Path="" ThrowOnInvalidPath="false" 
        Query='<%$ Code: N2.Find.Items.Where.Title.Like("test%") %>' OnDataSourceChanged="ids_DataSourceChanged" OnDeleted="ids_Deleted" OnDeleting="ids_Deleting" OnFiltering="ids_Filtering" OnInserted="ids_Inserted" OnInserting="ids_Inserting" OnItemCreated="ids_ItemCreated" OnItemCreating="ids_ItemCreating" OnSelected="ids_Selected" OnSelecting="ids_Selecting" OnUpdated="ids_Updated" OnUpdating="ids_Updating" />
    <asp:HyperLink ID="hlReload" runat="server" NavigateUrl="<%$ CurrentPage:Url %>" Text="Reload" />
    <asp:DetailsView style="float:right" AllowPaging="true" ID="dvItem" runat="server" DataSourceID="ids" OnPageIndexChanged="dvItem_PageIndexChanged" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" AutoGenerateInsertButton="True" AutoGenerateRows="false">
        <Fields>
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Url" HeaderText="Url" ReadOnly="true" SortExpression="Url" />
        </Fields>
    </asp:DetailsView>
    <asp:GridView ID="gvChildren" runat="server" DataSourceID="ids" AllowPaging="True" AllowSorting="True" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" AutoGenerateSelectButton="True" AutoGenerateColumns="false" DataKeyNames="ID" OnSelectedIndexChanged="gvChildren_SelectedIndexChanged">
        <EditRowStyle BackColor="#C0FFC0" />
        <SelectedRowStyle BackColor="#FFFFC0" />
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Url" HeaderText="Url" ReadOnly="true" SortExpression="Url" />
        </Columns>
    </asp:GridView>
    <hr />
    <asp:GridView ID="gvQuery" runat="server" DataSourceID="ids" DataMember="Query" AllowPaging="True" AllowSorting="True" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" AutoGenerateColumns="false" DataKeyNames="ID">
        <EditRowStyle BackColor="#C0FFC0" />
        <SelectedRowStyle BackColor="#FFFFC0" />
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Url" HeaderText="Url" ReadOnly="true" SortExpression="Url" />
        </Columns>
    </asp:GridView>
    <hr />
    <asp:TreeView ID="tv1" runat="server" DataSourceID="ids" />
</asp:Content>
