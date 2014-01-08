<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Shop.aspx.cs" Inherits="MicroSite.Shop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('.BlocksIt').BlocksIt({
                numOfCol: 3,
                offsetX: 15,
                offsetY: 15
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <div class="BlocksIt" >
        <div class="grid">
            分类1<br />
        </div>
        <div class="grid">
            分类2<br />
            <br />
            <br />
        </div>
        <div class="grid">
            分类3<br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
        <div class="grid">
            分类4<br />
            <br />
        </div>
        <div class="grid">
            分类5<br />
            <br />
            <br />
        </div>
        <div class="grid">
            分类6<br />
            <br />
            <br />
            <br />
        </div>
        <div class="grid">
            分类7<br />
        </div>
        <div class="grid">
            分类8<br />
            <br />
            <br />
            <br />
        </div>
        <div class="grid">
            分类9<br />
            <br />
        </div>
    </div>
</asp:Content>
