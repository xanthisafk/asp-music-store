<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Music_Store.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/Home.css" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Popular songs</h1>
    <div class="flexed-div">
        <asp:datalist id="DataList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" HorizontalAlign="Left">
        <ItemTemplate>
            <div class="content">
                <asp:ImageButton ID="Image" runat="server" ImageUrl='<%# "~/Assets/Art/" + Eval("Art") %>' PostBackUrl='<%# "~/Song.aspx?id=" + Eval("Id") %>'/>
                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Artist") + " - " + Eval("Title")%>' CssClass="title"></asp:Label>
            </div>
        </ItemTemplate>
    </asp:DataList>
    </div>


    
    
</asp:Content>