<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MicroSite.Admin.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        账号
        <asp:TextBox runat="server" ID="txtLoginName"></asp:TextBox><br />
        密码
        <asp:TextBox runat="server" ID="txtLoginPwd" TextMode="Password"></asp:TextBox><br />
        <asp:Button runat="server" ID="btnLogin" Text="登录" OnClick="btnLogin_Click" />
    </div>
    </form>
</body>
</html>
