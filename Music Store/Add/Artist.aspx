<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Artist.aspx.cs" Inherits="Music_Store.Add.Artist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/Artist.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('<%= Image1.ClientID %>').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }

        $('<%= FileUpload1.ClientID %>').change(function () {
            readURL(this);
        });
    </script>
    <text><u>Add/update artist</u></text>
    <text>
        <asp:Label ID="msg_lbl" runat="server" Text=""></asp:Label>
    </text>
    <div class="flexed-div">
        <div id="content">
            <asp:DropDownList ID="Artist_dl" runat="server" CssClass="dl"></asp:DropDownList><asp:Button ID="fetch_btn" runat="server" Text="Fetch" OnClick="Fetch_Click" CssClass="fth-btn" />
            <br />
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Content/Images/noimg.jpg" Width="500px" Height="500px" accept=".png,.jpg,.jpeg,.gif" CssClass="img"/>
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="mtt-10" onchange="readURL(this)" />
        </div>
        <div id="keeper">
            <asp:Label ID="id_lbl" runat="server" Text="Id" CssClass="id-lbl"></asp:Label>
            <asp:TextBox ID="name_tb" runat="server" placeholder="Name" CssClass="name-tb"></asp:TextBox>
            <asp:Button ID="update_btn" runat="server" Text="Update" CssClass="upd-btn" OnClick="Update_Click" Visible="false"/>
            <asp:Button ID="addnew_btn" runat="server" Text="Add New" CssClass="upd-btn" OnClick="Add_New" Visible="false"/>
        </div>
    </div>    
</asp:Content>
