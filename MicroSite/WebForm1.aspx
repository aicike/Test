<html>
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <!--media=print ������Կ����ڴ�ӡʱ��Ч-->
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
            font-family: "����";
            font-size: 9pt;
        }
    </style>
    <script language="JavaScript">
        var hkey_root, hkey_path, hkey_key
        hkey_root = "HKEY_CURRENT_USER"
        hkey_path = "\\Software\\Microsoft\\Internet Explorer\\PageSetup\\"
        //        ������ҳ��ӡ��ҳüҳ��Ϊ��
        //        function pagesetup_null() {
        //            try {
        //                var RegWsh = new ActiveXObject("WScript.Shell")
        //                hkey_key = "header"
        //                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
        //                hkey_key = "footer"
        //                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
        //            } catch (e) { }
        //        }
        //������ҳ��ӡ��ҳüҳ��ΪĬ��ֵ
        function pagesetup_default() {
            try {
                var RegWsh = new ActiveXObject("WScript.Shell")
                hkey_key = "header";
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "ҳü&b����ֻ��������&bҳ�룬&p/&P")
                hkey_key = "footer"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "ҳ��&b����ֻ��������&bҳ�룬&p/&P")
            } catch (e) { }
        }
        //ҳüҳ������ http://antheahuimin.blog.163.com/blog/static/18069132007612114452705/
    </script>
</head>
<body>
    <center class="Noprint">
        <p>
            <object id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0"
                width="0">
            </object>
            <input type="button" value="��ӡ" onclick="pagesetup_default();document.all.WebBrowser.ExecWB(6,1)">
            <input type="button" value="ֱ�Ӵ�ӡ" onclick="pagesetup_default();document.all.WebBrowser.ExecWB(6,6)">
            <input type="button" value="ҳ������" onclick="document.all.WebBrowser.ExecWB(8,1)">
        </p>
        <p>
            <input type="button" value="��ӡԤ��" onclick="pagesetup_default();document.all.WebBrowser.ExecWB(7,1)">
            <br />
        </p>
        <hr align="center" width="90%" size="1" noshade>
    </center>
    <div style="text-align: center">
        <div>
            <div class="content">
                ��1ҳ����
            </div>
        </div>
        <hr align="center" width="90%" size="1" noshade class="NOPRINT">
        <!--��ҳ-->
        <div class="PageNext">
        </div>
        <div>
            <div class="content">
                ��2ҳ����
            </div>
        </div>
        <hr align="center" width="90%" size="1" noshade class="NOPRINT">
        <!--��ҳ-->
        <div class="PageNext">
        </div>
        <div>
            <div class="content">
                ��3ҳ����
            </div>
        </div>
        <hr align="center" width="90%" size="1" noshade class="NOPRINT">
        <!--��ҳ-->
        <div class="PageNext">
        </div>
        <div>
            <div class="content">
                ��4ҳ����
            </div>
        </div>
    </div>
</body>
</html>
