<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Error.aspx.cs" Inherits="MicroSite.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body
        {
            background-color: #637db6;
        }
        a
        {
            color: #ff7301;
        }
        
        .divFont
        {
            line-height: 25px;
            font-size: 20px;
            font-weight: bold;
            color: #ffffff;
            padding-bottom: 20px;
            padding-left: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" cellpadding="0" cellspacing="0" style="margin-top: 10%;">
        <tr>
            <td>
                <img id="imgError" src="" />
            </td>
            <td class="divFont" valign="bottom">
                对不起，<br />
                您访问的页面不存在
                <br />
                <br />
                <a href="/home"><< 返回首页</a>
            </td>
        </tr>
    </table>
</asp:Content>
