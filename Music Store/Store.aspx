<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="Music_Store.Store" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet", type="text/css" href="Content/Store.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="store-container">
        <div id="sort-container">
            <asp:DropDownList ID="cate_dl" runat="server" CssClass="dl">
                <asp:ListItem Value="Title">Song Name</asp:ListItem>
                <asp:ListItem Value="Artist">Artist Name</asp:ListItem>
                <asp:ListItem Value="Cost">Price</asp:ListItem>
                <asp:ListItem Value="Popular">Popularity</asp:ListItem>
                <asp:ListItem Value="OnSale">On Sale</asp:ListItem>
            </asp:DropDownList> <asp:DropDownList ID="type_dl" runat="server" CssClass="dl">
                <asp:ListItem Value="asc">Ascending</asp:ListItem>
                <asp:ListItem Value="desc">Descending</asp:ListItem>
            </asp:DropDownList> <asp:Button ID="sort_btn" runat="server" Text="Sort" CssClass="sbtn" OnClick="sort_btn_Click" />
            <div id="wallet">
                <img src="Content/Images/wallet.png"  class="wallet-img"/> <asp:Label CssClass="wallet-text" ID="money_lbl" runat="server" Text="Not logged in"></asp:Label>
            </div>
        </div>
        <asp:DataList ID="DataList1" runat="server">

            <ItemTemplate>
                
                <table class="auto-style2">
                    <tr>
                        <td class="auto-style4" rowspan="8">
                            <asp:ImageButton ID="image" runat="server" CssClass="image" ImageUrl='<%# "~/Assets/Art/" + Eval("Art") %>' PostBackUrl='<%# "Song.aspx?id="+ Eval("Id") %>' />
                        </td>
                        <td class="auto-style7" colspan="3">
                            <a href='<%# "Song.aspx?id="+ Eval("Id") %>'>
                                <asp:Label ID="title_lbl" runat="server" CssClass="title" Text='<%# Eval("Title") %>'></asp:Label>
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style5" colspan="3"></td>
                    </tr>
                    <tr>
                        <td class="auto-style8" colspan="3">
                            <asp:Label ID="artist_lbl" runat="server" CssClass="artist" Text='<%# Eval("Artist") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="text-center" colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style9">
                            <asp:Label ID="Cost_leg" runat="server" CssClass="legend" Text="Price"></asp:Label>
                        </td>
                        <td class="auto-style9">
                            <asp:Label ID="Popular_leg" runat="server" CssClass="legend" Text="Popular"></asp:Label>
                        </td>
                        <td class="auto-style9">
                            <asp:Label ID="OnSaleLeg" runat="server" CssClass="legend" Text="On Sale"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="text-center">
                            <asp:Label ID="price_lbl" CssClass="price" Text='<%# "₹" + Eval("Cost") %>' runat="server"></asp:Label>
                        </td>
                        <td class="text-center">
                            <asp:Label ID="pop_lbl" CssClass="tf" Text='<%# Eval("Popular") %>' runat="server"></asp:Label>
                        </td>
                        <td class="text-center">
                            <asp:Label ID="sale_lbl" cssclass="tf" Text='<%# Eval("OnSale") %>' runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="text-center" colspan="3">
                            <asp:Button ID="view" runat="server" Text="View Info" CssClass="button-c" PostBackUrl='<%# "Song.aspx?id="+ Eval("Id") %>' />
                        </td>
                    </tr>
                </table>
                
            </ItemTemplate>

        </asp:DataList>

    </div>
</asp:Content>
