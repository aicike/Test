﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProductList.aspx.cs" Inherits="MicroSite.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://192.168.1.138/Scripts/waterfall/jquery.infinitescroll.min.js"
        type="text/javascript"></script>
    <script src="http://192.168.1.138/Scripts/waterfall/jquery.masonry.js" type="text/javascript"></script>
    <style type="text/css">
        *
        {
            margin: 0;
            padding: 0;
            list-style-type: none;
        }
        a, img
        {
            border: 0;
        }
        
        body
        {
            color: #666666;
            font-family: Arial;
            font-size: 12px;
        }
        .clearfix:after
        {
            content: ".";
            display: block;
            height: 0;
            clear: both;
            visibility: hidden;
        }
        .clearfix
        {
            display: inline-table;
        }
        *html .clearfix
        {
            height: 1%;
        }
        .clearfix
        {
            display: block;
        }
        * + html .clearfix
        {
            min-height: 1%;
        }
        .demo
        {
            width: 95%;
            margin: 0 auto;
        }
        
        /* item_list */
        .item_list
        {
            position: relative;
            padding: 0 0 50px;
        }
        .item
        {
            width: 48%;
            min-height: 150px;
            background: #fff;
            overflow: hidden;
            margin: 10px 0 0 0;
            border-radius: 4px 4px 4px 4px;
            box-shadow: 0 1px 3px rgba(34, 25, 25, 0.2);
        }
        .item_t
        {
            padding: 10px 8px 0px;
        }
        
        
        
        .item_t .title
        {
            padding: 8px 0;
            line-height: 15px;
            color: #6C6C6C;
            width: 100%;
        }
        
        
        /* more */
        #more
        {
            display: block;
            margin: 10px auto 20px;
        }
        
        /* infscr-loading */
        #infscr-loading
        {
            bottom: 10px;
            position: absolute;
            text-align: center;
            height: 20px;
            line-height: 20px;
            z-index: 100;
            width: 100%;
        }
        
        
        
        /* to_top */
        .to_top a, .to_top a:hover
        {
            background: url("image/gotop.png") no-repeat;
        }
        .to_top a
        {
            background-position: 0 0;
            float: left;
            height: 50px;
            overflow: hidden;
            width: 50px;
            position: fixed;
            bottom: 35px;
            cursor: pointer;
            right: 20px;
            _position: absolute;
            _right: auto;
            _left: expression(eval(document.documentElement.scrollLeft+document.documentElement.clientWidth-this.offsetWidth)-(parseInt(this.currentStyle.marginLeft, 10)||0)-(parseInt(this.currentStyle.marginRight, 10)||20));
            _top: expression(eval(document.documentElement.scrollTop+document.documentElement.clientHeight-this.offsetHeight-(parseInt(this.currentStyle.marginTop, 10)||20)-(parseInt(this.currentStyle.marginBottom, 10)||20)));
        }
        .to_top a:hover
        {
            background-position: -51px 0px;
        }
        
        
        
        .imgDiv
        {
            width: 100%;
            border: 1px solid #deedde;
        }
        .imgs
        {
            max-width: 100%;
            min-width: 100%;
        }
        .titleDiv
        {
            margin-top: 10px;
            font-size: 16px;
            font-weight: bold;
        }
        .bottom
        {
            margin-top: 5px;
            position: relative;
            bottom: 8px;
            color: Red;
            font-size: 12px;
        }
        .loadOK
        {
            font-size: 14px;
            font-weight: bold;
            width: 98%;
            margin: 10px 0px;
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function item_masonry() {
            var Wwidth = $(".demo").width() * 0.5
            var Iwidth = $(".item").width();
            var Rwidth = parseInt((Wwidth - Iwidth)) * 1.9;
            $('.item imgDiv').load(function () {
                $('.infinite_scroll').masonry({
                    itemSelector: '.masonry_brick',
                    columnWidth: 0,
                    gutterWidth: Rwidth
                });
            });
            $('.infinite_scroll').masonry({
                itemSelector: '.masonry_brick',
                columnWidth: 0,
                gutterWidth: Rwidth
            });
        }

        $(function () {

            function item_callback() {

                $('.item').mouseover(function () {
                    $(this).css('box-shadow', '0 1px 5px rgba(35,25,25,0.5)');
                    $('.btns', this).show();
                }).mouseout(function () {
                    $(this).css('box-shadow', '0 1px 3px rgba(34,25,25,0.2)');
                    $('.btns', this).hide();
                });

                item_masonry();

            }

            item_callback();

            $(window).resize(function () {
                item_masonry();

            });

            $('.item').fadeIn();
            var sp =1
            $(".infinite_scroll").infinitescroll({
                navSelector: "#more",
                nextSelector: "#more a",
                itemSelector: ".item",
                animate: false,
                maxPage: 3,
                loading: {
                    img: "Image/Loading.gif",
                    finished: function () {
                        sp++;
                        if (sp > 3) {
                            $("#infscr-loading").hide();
                            $(".loadOK").show();
                        }
                        
                    }
                },
                errorCallback: function () {
                
                }

            }, function (newElements) {
                var $newElems = $(newElements);
                $('.infinite_scroll').masonry('appended', $newElems, false);
                $newElems.fadeIn();
                item_callback();
                return;
            });

        });



        function OpenUrl(ID) {
            var url = "ProductInfo.aspx?id=" + ID;
            window.location.href = url;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo clearfix">
        <div class="titleDiv">
            水果 新鲜</div>
        <div class="item_list infinite_scroll">
            <div class="item masonry_brick" onclick="OpenUrl(1)">
                <div class="item_t">
                    <div class="imgDiv">
                        <a href="#">
                            <img class="imgs" src="Image/logo2.jpg" />
                        </a>
                    </div>
                    <div class="title">
                        <span>鼠标滑过动画航特效鼠标滑过动画标滑过动画航特效鼠标滑过动航特效鼠标滑过动画</span>
                    </div>
                    <div class="bottom">
                        ￥17.12
                    </div>
                </div>
            </div>
            <!--item end-->
            <div class="item masonry_brick">
                <div class="item_t">
                    <div class="imgDiv">
                        <a href="#">
                            <img class="imgs" src="Image/AppDefault.png" /></a>
                    </div>
                    <div class="title">
                        <span>作动画导航特效</span></div>
                    <div class="bottom">
                        ￥17.12
                    </div>
                </div>
            </div>
            <!--item end-->
            <div class="item masonry_brick">
                <div class="item_t">
                    <div class="imgDiv">
                        <a href="#">
                            <img class="imgs" src="Image/366585.jpg" /></a>
                    </div>
                    <div class="title">
                        <span>用div+css制作一个CSS3的简约图标导航菜单</span></div>
                    <div class="bottom">
                        ￥17.12
                    </div>
                </div>
            </div>
            <!--item end-->
        </div>
        <div id="more">
            <a href="ProductListItem.aspx?maid=a&page=1"></a>
        </div>
    </div>
    <div class="loadOK" style="display: none;">
        全部加载完成
    </div>
    <div style="display: none;" id="gotopbtn" class="to_top">
        <a title="返回顶部" href="javascript:void(0);"></a>
    </div>
    <script type="text/javascript">
        $(function () {

            $(window).scroll(function () {
                $(window).scrollTop() > 1000 ? $("#gotopbtn").css('display', '').click(function () {
                    $(window).scrollTop(0);
                }) : $("#gotopbtn").css('display', 'none');
            });

        });
    </script>
</asp:Content>