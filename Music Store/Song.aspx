<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Song.aspx.cs" Inherits="Music_Store.Song" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet", type="text/css" href="Content/Song.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="songid" runat="server" />
    <asp:HiddenField ID="actualprice" runat="server" />
    <div id="header-containers">
        <div id="wallet">
            <img src="Content/Images/wallet.png" class="wallet-img" />
            <asp:Label CssClass="wallet-text" ID="money_lbl" runat="server" Text="Not logged in"></asp:Label>
        </div>
    </div>
    <div class="container-thing">
        
    <table class="table-mg">
        <tr>
            <!-- Album art -->
            <td rowspan="4" class="table-wh">  
                <asp:Image ID="art" runat="server" ImageUrl="~/Content/Images/logo.png" CssClass="art" />
            </td>

            <td class="ttl-wh">
                <asp:Label ID="title" runat="server" Text="Song Name" CssClass="title"></asp:Label>
            </td>

            <!-- Artist photo -->
            <td rowspan="4" class="table-wh">
                <asp:Image ID="artist_photo" runat="server" ImageUrl="~/Content/Images/logo.png" CssClass="polaroid" />
            </td>

        </tr>
        <tr>
            <td class="ttl-wh">
                <asp:Label ID="artist" runat="server" Text="Artist" CssClass="artist"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ttl-wh">
                <asp:Label ID="price" runat="server" Text="Price" CssClass="price"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ttl-wh">
                <asp:Button ID="purchase" runat="server" Text="Buy Now" CssClass="buy-btn" OnClick="purchase_Click" />
            </td>
        </tr>
        <tr>
            <td rowspan="4" colspan="3" class="video-wrapper">
                
            </td>
        </tr>
    </table>
    </div>

    <asp:HiddenField ID="url_hf" runat="server" />
<!--
    <iframe src='< %= //url_hf.Value %>' title="YouTube video player" frameborder="2" allow="accelerometer; encrypted-media;"></iframe>
-->
</asp:Content>
