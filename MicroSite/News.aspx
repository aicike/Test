<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="News.aspx.cs" Inherits="MicroSite.News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul data-role="listview" data-inset="false">
        <asp:Repeater runat="server" ID="repeaterNews">
            <ItemTemplate>
                <li><a href="NewDetail.aspx" style="padding-left: 119px;">
                    <%--<div style="float:left;height:80px;overflow: hidden;width:108px;position: absolute;left: 1px;top: 0;">--%>
                    <img src="<%#Eval("S")%>" style="max-height: 80px;height:80px; width: 109px;max-width:109px;">
                    <%--</div>--%>
                    <h2>
                        <%#Eval("T")%></h2>
                    <p>
                        <%#Eval("P")%></p>
                </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Content>
