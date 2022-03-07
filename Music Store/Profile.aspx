<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Music_Store.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/Profile.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="profile-container">
        <h3>Profile</h3>

        <label class="legend">Name</label>
        <asp:TextBox ID="name_tb" runat="server" CssClass="tb" placeholder="Name"></asp:TextBox>

        <label class="legend">Username</label>
        <asp:TextBox ID="username_tb" runat="server" CssClass="tb" placeholder="Username" readonly="true"></asp:TextBox>

        <label class="legend">Email</label>
        <asp:TextBox ID="email_tb" runat="server" CssClass="tb" placeholder="Email"></asp:TextBox>

        <label class="legend">Old password</label>
        <asp:TextBox ID="password_tb" runat="server" TextMode="Password" CssClass="tb"  placeholder="Old Password"></asp:TextBox>

        <label class="legend">New password</label>
        <asp:TextBox ID="passwordn_tb" runat="server" TextMode="Password" CssClass="tb" placeholder="New Password"></asp:TextBox>

        <label class="legend">Confirm new password</label>
        <asp:TextBox ID="passwordn2_tb" runat="server" TextMode="Password" CssClass="tb" placeholder="Confirm New Password"></asp:TextBox>

        <asp:Label ID="msg_lbl" runat="server" Text="" ForeColor="Red" Width="100%" />
        <asp:Button ID="update" runat="server" Text="Update" CssClass="button-c ok" OnClick="update_Click" /><asp:Button ID="logout_btn" runat="server" Text="Logout" CssClass="button-c cancel" OnClick="logout_btn_Click" />

        <asp:Button ID="song_btn" runat="server" Text="Add Song" CssClass="button-c admin" Visible="false" PostBackUrl="~/Add/Song.aspx" /><asp:Button ID="artist_btn" runat="server" Text="Add Artist" CssClass="button-c admin" Visible="false" PostBackUrl="~/Add/Artist.aspx" />

    </div>

    <div class="profile-container">
        <label class="legend">Add Money</label>
        <asp:Label ID="money_current" runat="server" CssClass="legend"/>
        <asp:TextBox ID="money_tb" runat="server" TextMode="Number" CssClass="tb" placeholder="Money!"></asp:TextBox>
        <asp:Button ID="money_btn" runat="server" Text="Add Money" CssClass="button-c cash" OnClick="Add_Money"/>
    </div>

    <div class="profile-container">
        <label class="legend">Purchased Music</label>
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
    </div>
</asp:Content>
