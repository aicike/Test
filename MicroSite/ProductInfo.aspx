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
                    breakpointCenterArea:1,
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="royalSlider" class="visibleNearby" style="background-color: #000">
        <!-- simple image slide -->
        <img src="Image/366585.jpg" />
        <img src="Image/12.jpg" />
        <img src="Image/23.jpg" />
        <!-- lazy loaded image slide -->
        <!--<a class="rsImg" href="image.jpg">image desc</a>-->
    </div>

    <div style=" width: 95%; margin:25px auto">
    asdfasdf 
    </div>
</asp:Content>
