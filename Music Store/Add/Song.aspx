<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Song.aspx.cs" Inherits="Music_Store.Add.Song" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/Song2.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <text>
            <u>
                Add/Update new song
            </u>
        </text>
        <text>
            <asp:Label ID="msg_lbl" runat="server" Text=""></asp:Label>
        </text>

        <table class="tbl">
            <tr>
                <td colspan="2">
                    <asp:DropDownList ID="song_dl" runat="server" CssClass="dl-song"></asp:DropDownList><asp:Button Text="Fetch" runat="server" ID="fetch_btn" CssClass="fetch-btn" OnClick="Fetch_Click"/>
                </td>
            </tr>
            <tr>
                <td rowspan="8"  style="padding: 10px;">
                    <asp:Image ID="art" runat="server" ImageUrl="~/Content/Images/noimg.jpg" CssClass="alb-art"/>
                    <br />
                    <asp:FileUpload ID="art_upload" runat="server" />
                </td>
                <td class="text-center">
                    <label class="legend">Id</label>
                    <asp:Label ID="id_lbl" runat="server" Text="Id" CssClass="id-lbl"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="text-center">
                    <label class="legend">Title</label>
                    <asp:TextBox ID="title_tb" runat="server" placeholder="Title" CssClass="tb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-center">
                    <label class="legend">Artist</label>
                    <asp:DropDownList ID="artist_dl" runat="server" CssClass="dl-artist"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="text-center">
                    <label class="legend">Price</label>
                    <asp:TextBox ID="cost_tb" runat="server" placeholder="Price" TextMode="Number" CssClass="tb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-center">
                    <asp:CheckBox ID="popular_cb" runat="server" CssClass="cb"/><label class="cb-t">Popular</label>
                    <asp:CheckBox ID="sale_cb" runat="server" CssClass="cb"/><label class="cb-t">On Sale</label>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="legend">Link</label>
                    <asp:TextBox ID="link_tb" runat="server" placeholder="Link to song on YouTube" CssClass="tb"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-center">
                    <asp:Button ID="update_btn" Text="Update" runat="server" visible="false" cssclass="upt-btn" OnClick ="Update_Click"/>
                    <asp:Button ID="addenw_btn" Text="Add New" runat="server" visible="false" CssClass="upt-btn" OnClick="Add_New"/>
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
        </table>

    </div>
</asp:Content>
