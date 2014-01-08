<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="MicroSite.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="http://baidu.com">
        <div class="listItem1">
            项目介绍
        </div>
    </a><a href="News.aspx">
        <div class="listItem1">
            活动资讯
        </div>
    </a>
    <div style="clear: both">
    </div>
    <div class="listItem2">
        户型图
    </div>
    <div class="listItem2">
        360度全景图
    </div>
</asp:Content>
