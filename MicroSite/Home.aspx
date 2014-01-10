<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="MicroSite.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div class="ui-grid-a">
            <div class="ui-block-a">
                <a href="http://baidu.com">
                    <div class="ui-body ui-body-d listItem1" style="margin-right: 10px;">
                        项目介绍
                    </div>
                </a>
            </div>
            <div class="ui-block-b">
                <a href="News.aspx">
                    <div class="ui-body ui-body-d listItem1" style="margin-left: 10px;">
                        活动资讯</div>
                </a>
            </div>
        </div>
        <div class="ui-grid-solo">
            <div class="ui-block-a">
                <div class="listItem2 ui-shadow">
                    <div class="listItem_text font_title">
                        <div>
                            户型图</div>
                    </div>
                    <div class="listItem_img">
                        <img src="Image/366585.jpg" />
                    </div>
                    <div style="clear: both">
                    </div>
                </div>
            </div>
        </div>
        <div class="ui-grid-solo">
            <div class="ui-block-a">
                <div class="listItem2 ui-shadow">
                    <div class="listItem_img">
                        <img src="Image/366585.jpg" />
                    </div>
                    <div class="listItem_text font_title">
                        <div>
                            360度全景图</div>
                    </div>
                    <div style="clear: both">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
