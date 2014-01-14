<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProductInfo.aspx.cs" Inherits="MicroSite.ProductInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="http://192.168.1.138/Scripts/royalslider/royalslider.css" rel="stylesheet"
        type="text/css" />
    <script src="http://192.168.1.138/Scripts/royalslider/royalslider.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var si = $('#royalSlider').royalSlider({
                addActiveClass: true,
                arrowsNav: false,
                controlNavigation: 'none',
                autoScaleSlider: true,
                autoScaleSliderWidth: 1024,
                autoScaleSliderHeight: 640,
                loop: false,
                fadeinLoadedSlide: false,
                globalCaption: true,
                keyboardNavEnabled: true,
                globalCaptionInside: true,

                visibleNearby: {
                    enabled: true,
                    centerArea: 0.7,
                    center: true,
                    breakpoint: 100,
                    breakpointCenterArea: 1,
                    navigateByCenterClick: true
                }
            });




        });


    </script>
    <style>
        body
        {
            margin: 0px;
        }
        .visibleNearby
        {
            width: 100%;
            background: #141414;
            color: #FFF;
            padding-top: 5px;
            padding-bottom: 5px;
        }
        .visibleNearby .rsGCaption
        {
            font-size: 16px;
            line-height: 18px;
            padding: 12px 0 16px;
            background: #141414;
            width: 100%;
            position: static;
            float: left;
            left: auto;
            bottom: auto;
            text-align: center;
        }
        .visibleNearby .rsGCaption span
        {
            display: block;
            clear: both;
            color: #bbb;
            font-size: 14px;
            line-height: 22px;
        }
        
        /* Scaling transforms */
        .visibleNearby .rsSlide img
        {
            border-radius: 5px;
            opacity: 0.45;
            -webkit-transition: all 0.3s ease-out;
            -moz-transition: all 0.3s ease-out;
            transition: all 0.3s ease-out;
            -webkit-transform: scale(0.9);
            -moz-transform: scale(0.9);
            -ms-transform: scale(0.9);
            -o-transform: scale(0.9);
            transform: scale(0.9);
        }
        .visibleNearby .rsActiveSlide img
        {
            opacity: 1;
            -webkit-transform: scale(1);
            -moz-transform: scale(1);
            -ms-transform: scale(1);
            -o-transform: scale(1);
            transform: scale(1);
        }
    </style>
    <script>
        $(function () {

            $("#add").click(function () {
            
                var number = $("#<%= txtNumber.ClientID %>").val();
                if (number != "" && number != undefined) {
                    $("#<%= txtNumber.ClientID %>").val(parseInt(number)+ 1);
                }
                else {

                    $("#<%= txtNumber.ClientID %>").val("0");
                }
            });

            $("#delete").click(function () {
                var number = $("#<%= txtNumber.ClientID %>").val();
                if (number != "" && number != undefined) {
                    if (number == 0) {
                        $("#<%= txtNumber.ClientID %>").val("0");
                    }
                    else {
                        $("#<%= txtNumber.ClientID %>").val(parseInt(number) - 1);
                    }
                }
                else {
                    $("#<%= txtNumber.ClientID %>").val("0");
                }
            });

            $("#<%= txtNumber.ClientID %>").keyup(function () {
                var tmptxt = $(this).val();
                $(this).val(tmptxt.replace(/\D|^0/g, ''));
            }).bind("paste", function () {
                var tmptxt = $(this).val();
                $(this).val(tmptxt.replace(/\D|^0/g, ''));
            }).css("ime-mode", "disabled");

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <div id="royalSlider" class="visibleNearby" style="background-color: #3C3C3C">
        <!-- simple image slide -->
        <img src="Image/12.jpg" />
        <img src="Image/23.jpg" />
        <img src="Image/12.jpg" />
        <img src="Image/23.jpg" />
        <!-- lazy loaded image slide -->
        <!--<a class="rsImg" href="image.jpg">image desc</a>-->
    </div>
    <div style="width: 95%; margin: 25px auto;">
        <div style="line-height: 25px">
            MIUI/小米 2S(MI2S)小米2S(32G)小米手机官网正品2s手机 标准版
        </div>
        <div style="margin-top: 10px;">
            价格：<span style="color: Red; font-weight: bold">￥177.00</span>
        </div>
        <div style="margin-top: 5px; font-size: 12px; color: #8E8E8E">
            运费：快递￥177.00
        </div>
        <div style="margin: 15px 0px; height: 3px; width: 100%; background: url(Image/item_line.png) repeat-x;">
        </div>
        <div class="ui-icon-nodisc" style="height: 50px">
            <table>
                <tr>
                    <td>
                        数量：
                    </td>
                    <td>
                        <a href="#" id="delete" data-role="button" data-iconpos="notext" data-icon="minus"
                            data-theme="b" data-inline="true" data-iconshadow="false"></a>
                    </td>
                    <td style="width: 50px">
                        <asp:TextBox ID="txtNumber" type="tel" data-clear-btn="false" runat="server"> 1</asp:TextBox>
                    </td>
                    <td>
                        <a href="#" id="add" data-role="button" data-iconpos="notext" data-icon="plus" data-theme="b"
                            data-inline="true" data-iconshadow="false"></a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin: 15px 0px; height: 3px; width: 100%; background: url(Image/item_line.png) repeat-x;">
        </div>
        <div style="">
            <table align="center" style="width: 100%">
                <tr>
                    <td style="width: 49%;">
                        <a href="#" data-role="button" data-corners="false" data-theme="e">加入购物车</a>
                    </td>
                    <td>
                    </td>
                    <td style="width: 49%;">
                        <a href="#" data-role="button" data-corners="false" data-theme="c">立即购买</a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin: 15px 0px; height: 3px; width: 100%; background: url(Image/item_line.png) repeat-x;">
        </div>
        <div style="">
            商品详情
        </div>
        <div style="margin-top: 5px; font-size: 14px; color: #7B7B7B; line-height: 20px">
            商品详情商品详情商品详情商品详情商品详情商品详情商品详情商品详情商品详情
        </div>
    </div>
    </form>
</asp:Content>
