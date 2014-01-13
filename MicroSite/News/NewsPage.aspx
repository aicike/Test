<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsPage.aspx.cs" Inherits="MicroSite.News.NewsPage" %>

<asp:repeater runat="server" id="repeater1">
            <ItemTemplate>
                <l data-corners="false" data-shadow="false" data-iconshadow="true" data-wrapperels="div"
    data-icon="arrow-r" data-iconpos="right" data-theme="c" class="item ui-btn ui-btn-icon-right ui-li-has-arrow ui-li ui-li-has-thumb ui-btn-up-c">
                    <div class="ui-btn-inner ui-li">
                        <div class="ui-btn-text">
                            <a href="/News/NewDetail.aspx" style="padding-left: 119px;" class="ui-link-inherit">
                                <img src="<%#Eval("S")%>" style="max-height: 80px;
                                    height: 80px; width: 109px; max-width: 109px;" class="ui-li-thumb">
                                <h2 class="ui-li-heading">
                                    <%#Eval("T")%></h2>
                                <p class="ui-li-desc">
                                    <%#Eval("P")%></p>
                            </a>
                        </div>
                        <span class="ui-icon ui-icon-arrow-r ui-icon-shadow">&nbsp;</span></div>
                </li>
            </ItemTemplate>
        </asp:repeater>
