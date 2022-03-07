<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Auth.aspx.cs" Inherits="Music_Store.Auth1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/Auth.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-6">
            <div id="login" class="form">
                <h3>Login to your existing account!</h3>

                <label class="legend">Username</label>
                <asp:TextBox ID="username_tb" runat="server" CssClass="tb" placeholder="Username"></asp:TextBox>
                <label class="legend">Password</label>
                <asp:TextBox ID="password_tb" runat="server" CssClass="tb" TextMode="Password" placeholder="Password"></asp:TextBox>
                <br />
                <asp:Label ID="login_msg" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Button ID="login_btn" runat="server" Text="Login" CssClass="button-c ok" OnClick="login_btn_Click" /><asp:Button ID="reset_btn" runat="server" Text="Reset" CssClass="button-c cancel" />

            </div>
        </div>
        <div class="col-6">
            <div id="register" class="form">
                <h3>Register a new account!</h3>

                <label class="legend">Username</label>
                <asp:TextBox ID="username_rtb" runat="server" CssClass="tb" placeholder="Username"></asp:TextBox>

                <label class="legend">Full name</label>
                <asp:TextBox ID="name_rtb" runat="server" CssClass="tb" placeholder="Full name"></asp:TextBox>

                <label class="legend">Password</label>
                <asp:TextBox ID="password_rtb" runat="server" CssClass="tb" placeholder="Password" TextMode="Password"></asp:TextBox>

                <label class="legend">Confirm password</label>
                <asp:TextBox ID="password2_rtb" runat="server" CssClass="tb" placeholder="Confirm password" TextMode="Password"></asp:TextBox>

                <label class="legend">Email</label>
                <asp:TextBox ID="email_rtb" runat="server" CssClass="tb" placeholder="Email"></asp:TextBox>
                <br />
                <asp:Label ID="register_msg" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Button ID="register_btn" runat="server" Text="Register" CssClass="button-c ok" OnClick="register_btn_Click" /><asp:Button ID="reset_btn2" runat="server" Text="Reset" CssClass="button-c cancel" OnClick="reset_btn2_Click" />

            </div>
        </div>
    </div>
</asp:Content>
