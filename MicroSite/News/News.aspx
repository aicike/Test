<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="News.aspx.cs" Inherits="MicroSite.News.News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        alert("滚动条已经到达顶部为0");
        var lastID = '<%=Convert.ToInt32(ViewState["page"]) %>';
        $(function () {
            $(window).scroll(function () {
                if ($(document).scrollTop() <= 0) {
                    //alert("滚动条已经到达顶部为0");
                }
                if ($(document).scrollTop() >= $(document).height() - $(window).height()) {
                    //alert("123");
                    if (lastID != null) {
                        var resUrl = "http://192.168.1.166/WebRequest_Other/GetAdvertorialList_Jsonp?AMID=1&ID=" + lastID + "&ListCnt=10";
                        $.ajax({
                            url: resUrl,
                            data: {},
                            type: "GET",
                            dataType: "jsonp",
                            jsonpCallback: "localJsonpCallback"
                        });
                    }
                }
            });
        });

        function localJsonpCallback(json) {
            if (json != null) {
                var html = "";
                $.each(json.List, function (i, n) {
                    html += '<li data-corners="false" data-shadow="false" data-iconshadow="true" data-wrapperels="div" data-icon="arrow-r" data-iconpos="right" data-theme="c" class="ui-btn ui-btn-icon-right ui-li-has-arrow ui-li ui-li-has-thumb ui-btn-up-c"><div class="ui-btn-inner ui-li"><div class="ui-btn-text"><a href="/News/NewDetail.aspx" style="padding-left: 119px;" class="ui-link-inherit">' +
                    '<img src="' + n.S + '" style="max-height: 80px; height: 80px; width: 109px; max-width: 109px;" class="ui-li-thumb">' +
                    '<h2 class="ui-li-heading">' + n.T + '</h2><p class="ui-li-desc">' + n.P + '</p>' +
                '</a></div><span class="ui-icon ui-icon-arrow-r ui-icon-shadow">&nbsp;</span></div></li>';
                });
                $("#ulMain").append(html);
                var length = json.List.length;
                if (length > 0) {
                    lastID = json.List[length - 1].I;
                }
                console.log(lastID);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul data-role="listview" data-inset="false" id="ulMain">
        <asp:Repeater runat="server" ID="repeaterNews">
            <ItemTemplate>
                <li><a href="/News/NewDetail.aspx" style="padding-left: 119px;">
                    <img src="<%#Eval("S")%>" style="max-height: 80px; height: 80px; width: 109px; max-width: 109px;">
                    <h2>
                        <%#Eval("T")%></h2>
                    <p>
                        <%#Eval("P")%></p>
                </a></li>
                <!--item end-->
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Content>
