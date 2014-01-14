<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Shop.aspx.cs" Inherits="MicroSite.Shop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('.BlocksIt').BlocksIt({
                numOfCol: 1,
                offsetX: 0,
                offsetY: 8

            });


        });


        function OpenUrl(ID) {
            var url = "ProductList.aspx?id=" + ID;
            window.location.href = url;

        }



       
    </script>
    <style>
        .MainTab 
        {
            width: 100%;
            height: 135px;
        
        }
        
        .MainTab td
        {
            cursor:pointer;
        }
        .TitDIVLeft
        {
            text-shadow: none;
            color: #ffffff;
            padding-left: 10px;
            font-weight: bold;
            font-size: 14px;
        }
        
        .RemDIVLeft
        {
            text-shadow: none;
            color: #ffffff;
            padding-left: 10px;
            font-size: 12px;
        }
        .TitDIVRight
        {
            text-shadow: none;
            color: #ffffff;
            padding-right: 10px;
            font-weight: bold;
            font-size: 14px;
            text-align: right;
        }
        
        .RemDIVRight
        {
            text-shadow: none;
            color: #ffffff;
            padding-right: 10px;
            font-size: 12px;
            text-align: right;
        }
        .ImgDIV
        {
            width: 100%;
            height: 135px;
            background-color:#ffeedd;
            overflow: hidden;
        }
        .ShowImg
        {
            min-width: 100%;
            max-width: 120%;
            min-height: 135px;
        }
        .grid
        {
            width: 100% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div class="BlocksIt">
            <div class="grid">
                <table class="MainTab" cellpadding="0" cellspacing="0" onclick="OpenUrl(1)">
                    <tr style="background-color: #66CDAA">
                        <td style="width: 30%;">
                            <div class="TitDIVLeft">
                                新鲜水果
                            </div>
                            <div class="RemDIVLeft">
                                好吃新鲜
                            </div>
                        </td>
                        <td style="width: 70%;">
                            <div class="ImgDIV">
                                <img class="ShowImg" src="Image/366585.jpg" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="grid">
                <table class="MainTab" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #AB82FF">
                        <td style="width: 70%;">
                            <div class="ImgDIV">
                                <img class="ShowImg" src="Image/366585.jpg" />
                            </div>
                        </td>
                        <td style="width: 30%;">
                            <div class="TitDIVRight">
                                新鲜畜类
                            </div>
                            <div class="RemDIVRight">
                                好吃新鲜
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="grid">
                <table class="MainTab" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #5CACEE">
                        <td style="width: 30%;">
                            <div class="TitDIVLeft">
                                新鲜干果
                            </div>
                            <div class="RemDIVLeft">
                                好吃新鲜
                            </div>
                        </td>
                        <td style="width: 70%;">
                            <div class="ImgDIV">
                                <img class="ShowImg" src="Image/366585.jpg" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="grid">
                <table class="MainTab" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #EEB422">
                        <td style="width: 70%;">
                            <div class="ImgDIV">
                                <img class="ShowImg" src="Image/366585.jpg" />
                            </div>
                        </td>
                        <td style="width: 30%;">
                            <div class="TitDIVRight">
                                新鲜海鲜
                            </div>
                            <div class="RemDIVRight">
                                好吃新鲜
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="grid">
                <table class="MainTab" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #FF8247">
                        <td style="width: 30%;">
                            <div class="TitDIVLeft">
                                新鲜禽类
                            </div>
                            <div class="RemDIVLeft">
                                好吃新鲜
                            </div>
                        </td>
                        <td style="width: 70%;">
                            <div class="ImgDIV">
                                <img class="ShowImg" src="Image/366585.jpg" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
