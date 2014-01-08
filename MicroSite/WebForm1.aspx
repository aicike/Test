<html>
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <!--media=print 这个属性可以在打印时有效-->
    <style media="print">
        .Noprint
        {
            display: none;
        }
        .PageNext
        {
            page-break-after: always;
        }
    </style>
    <style>
        .NOPRINT
        {
            font-family: "宋体";
            font-size: 9pt;
        }
    </style>
    <script language="JavaScript">
        var hkey_root, hkey_path, hkey_key
        hkey_root = "HKEY_CURRENT_USER"
        hkey_path = "\\Software\\Microsoft\\Internet Explorer\\PageSetup\\"
        //        设置网页打印的页眉页脚为空
        //        function pagesetup_null() {
        //            try {
        //                var RegWsh = new ActiveXObject("WScript.Shell")
        //                hkey_key = "header"
        //                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
        //                hkey_key = "footer"
        //                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
        //            } catch (e) { }
        //        }
        //设置网页打印的页眉页脚为默认值
        function pagesetup_default() {
            try {
                var RegWsh = new ActiveXObject("WScript.Shell")
                hkey_key = "header";
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "页眉&b好像只能是文字&b页码，&p/&P")
                hkey_key = "footer"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "页脚&b好像只能是文字&b页码，&p/&P")
            } catch (e) { }
        }
        //页眉页脚资料 http://antheahuimin.blog.163.com/blog/static/18069132007612114452705/
    </script>
</head>
<body>
    <center class="Noprint">
        <p>
            <object id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0"
                width="0">
            </object>
            <input type="button" value="打印" onclick="pagesetup_default();document.all.WebBrowser.ExecWB(6,1)">
            <input type="button" value="直接打印" onclick="pagesetup_default();document.all.WebBrowser.ExecWB(6,6)">
            <input type="button" value="页面设置" onclick="document.all.WebBrowser.ExecWB(8,1)">
        </p>
        <p>
            <input type="button" value="打印预览" onclick="pagesetup_default();document.all.WebBrowser.ExecWB(7,1)">
            <br />
        </p>
        <hr align="center" width="90%" size="1" noshade>
    </center>
    <div style="text-align: center">
        <div>
            <div class="content">
                第1页内容
            </div>
        </div>
        <hr align="center" width="90%" size="1" noshade class="NOPRINT">
        <!--分页-->
        <div class="PageNext">
        </div>
        <div>
            <div class="content">
                第2页内容
            </div>
        </div>
        <hr align="center" width="90%" size="1" noshade class="NOPRINT">
        <!--分页-->
        <div class="PageNext">
        </div>
        <div>
            <div class="content">
                第3页内容
            </div>
        </div>
        <hr align="center" width="90%" size="1" noshade class="NOPRINT">
        <!--分页-->
        <div class="PageNext">
        </div>
        <div>
            <div class="content">
                第4页内容
            </div>
        </div>
    </div>
</body>
</html>
