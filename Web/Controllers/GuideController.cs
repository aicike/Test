using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Injection;
using Interface;
using Poco.Enum;
using Business;
using Common;
using System.Data.Entity;

namespace MicroSite_Web.Controllers
{
    public class GuideController : BaseController
    {
        [AllowCheckPermissions(false)]
        public ActionResult Index()
        {
            SystemConst.IsIndependence.NotAuthorizedPage();//验证项目是否独立部署
            ComplexAccountMain_Account c = new ComplexAccountMain_Account();
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var am = accountMainModel.List().FirstOrDefault();
            Account account = null;
            if (am == null)
            {
                am = new AccountMain();
            }
            else
            {
                account = am.Account_AccountMains.Select(a => a.Account).FirstOrDefault();
                if (account == null)
                {
                    account = new Account();
                }
            }
            c.AccountMain = am;
            c.Account = account;
            return View(c);
        }
        [AllowCheckPermissions(false)]
        public string SetAccountMain(ComplexAccountMain_Account c)
        {
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var count = accountMainModel.List().Count();
            bool isOk = false;
            string error = "请检查第1步中验证结果。";
            if (count > 1)
            {
                //错误：多次添加。
                isOk = false;
                error = "数据库中已配置多条基本信息，造成数据错误，请联系管理员。";
            }
            else
            {
                if (count == 0)
                {
                    //首次添加
                    AccountMain accountMain = new AccountMain();
                    accountMain.Name = c.AccountMain.Name;
                    accountMain.ProvinceID = c.AccountMain.ProvinceID;
                    accountMain.CityID = c.AccountMain.CityID;
                    accountMain.DistrictID = c.AccountMain.DistrictID;
                    accountMain.SaleAddress = c.AccountMain.SaleAddress;
                    accountMain.Phone = c.AccountMain.Phone;
                    accountMain.HostName = "www";
                    accountMain.LogoImageThumbnailPath = "";
                    accountMain.LogoImagePath = "";
                    accountMain.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
                    accountMain.FileLimit = 10;
                    CommonModel com = new CommonModel();
                    accountMain.RandomCode = com.CreateRandom("", 6);
                    accountMain.CreateTime = DateTime.Now;
                    accountMain.SalePhone = c.AccountMain.Phone;
                    accountMain.IsOrganization = SystemConst.IsOrganization;
                    var result = accountMainModel.MicroSite_Add(accountMain);
                    if (result.HasError)
                    {
                        isOk = false;
                        error = result.Error;
                    }
                    else
                    {
                        isOk = true;
                    }
                }
                else if (count == 1)
                {
                    //修改
                    var am = accountMainModel.List().FirstOrDefault();
                    am.Name = c.AccountMain.Name;
                    am.ProvinceID = c.AccountMain.ProvinceID;
                    am.CityID = c.AccountMain.CityID;
                    am.DistrictID = c.AccountMain.DistrictID;
                    am.SaleAddress = c.AccountMain.SaleAddress;
                    am.Phone = c.AccountMain.Phone;
                    am.SalePhone = c.AccountMain.Phone;
                    var result = accountMainModel.Edit(am);
                    if (result.HasError)
                    {
                        isOk = false;
                        error = result.Error;
                    }
                    else
                    {
                        isOk = true;
                    }
                }
            }
            if (isOk)
            {
                return "<script>isValidation=true;$('#wizard').smartWizard('setError',{stepnum:1,iserror:false});$('.buttonNext').click();</script>";
            }
            else
            {
                return "<script>isValidation=false;$('#wizard').smartWizard('showMessage','" + error + "');$('#wizard').smartWizard('setError',{stepnum:1,iserror:true});</script>";
            }
        }

        [AllowCheckPermissions(false)]
        public string SetAccount(ComplexAccountMain_Account c)
        {
            bool isOk = true;
            string error = "请检查第2步中验证结果。";
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            var roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var accountMain = accountMainModel.List().FirstOrDefault();
            var firstRole = roleModel.List().FirstOrDefault();
            var raw_account = account_accountMainModel.List().Select(a => a.Account).OrderBy(a => a.ID).AsNoTracking().FirstOrDefault();
            if (raw_account != null)
            {
                raw_account.Name = c.Account.Name;
                raw_account.Phone = c.Account.Phone;
                raw_account.Email = c.Account.Email;
                raw_account.LoginPwdPage = "000000";
                raw_account.LoginPwdPageCompare = "000000";
                raw_account.LoginPwd = DESEncrypt.Encrypt(c.Account.LoginPwdPage);
                var result = accountModel.Edit(raw_account);
                if (result.HasError)
                {
                    isOk = false;
                    error = result.Error;
                }
            }
            else
            {
                Account account = new Account();
                account.Name = c.Account.Name;
                account.Phone = c.Account.Phone;
                account.Email = c.Account.Email;
                account.LoginPwdPage = c.Account.LoginPwdPage;
                account.LoginPwdPageCompare = c.Account.LoginPwdPageCompare;
                IAccount_AccountMainModel model = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
                var result = model.MicroSite_Add(account, accountMain.ID, firstRole.ID);
                if (result.HasError)
                {
                    isOk = false;
                    error = result.Error;
                }
            }
            if (isOk)
            {
                //添加权限
                /*
                 * 集团权限
                 * ----------服务---------
                 * 1web基本服务，2售楼部服务
                 * ----------菜单---------
                 * 1首页，
                 * 5项目，21项目管理
                 * 8设置，29账号信息
                 * -----------------------
                 */


                var menuModel = Factory.Get<IMenuModel>(SystemConst.IOC_Model.MenuModel);
                string[] menuTokenArray = new string[] { "Token_Home", "Token_Manage", "Token_Product", "Token_Account", "Token_Project", "Token_News", "Token_AppSet", "Token_Set", "Token_User", 
                    "Token_Message", "Token_Member", "Token_Product_M", "Token_Product_T", "Token_Product_O1", "Token_Account_M", "Token_Account_R", "Token_Project_M", "Token_Project_M2", "Token_Project_I", 
                    "Token_News_U", "Token_News_S", "Token_News_Su", "Token_News_A", "Token_Keyword", "Token_Waitpage", "Token_Account_I", "Token_Library", "Token_User_M", "Token_User_Msg", "Token_User_SM", 
                    "Token_Message_N", "Token_Message_H", "Token_Member_I", "Token_Member_C", "Token_House_I", "Token_House_T", "Token_Library_I", "Token_Library_Vo", "Token_Library_Vi", "Token_Library_IT", 
                    "Token_Role", "Token_Project_Account", "Token_Project_Report", "Token_House_M", "Token_Chat", "Token_App_R", "Token_App_U" };
                int[] menuIDArray = menuModel.List_Cache().Where(a => menuTokenArray.Contains(a.Token)).Select(a => a.ID).ToArray();
                int[] serviceIDs = new[] { 1, 2 ,3 };
                int[] optionIDArray = null;

                try
                {
                    IRoleMenuModel roleMenuModel = Factory.Get<IRoleMenuModel>(SystemConst.IOC_Model.RoleMenuModel);
                    roleMenuModel.BindPermission(accountMain.ID, firstRole.ID, menuIDArray, optionIDArray, serviceIDs);
                }
                catch (Exception ex)
                {
                    isOk = false;
                    error = "未能绑定操作权限，请联系管理员。";
                }
                var result = accountModel.Login(c.Account.Phone, c.Account.LoginPwdPage);
                return "<script>isValidation=true;$('#wizard').smartWizard('setError',{stepnum:2,iserror:false});$('.buttonNext').click();</script>";
            }
            else
            {
                return "<script>isValidation=false;$('#wizard').smartWizard('showMessage','" + error + "');$('#wizard').smartWizard('setError',{stepnum:2,iserror:true});</script>";
            }
        }
    }

    public class ComplexAccountMain_Account
    {
        public AccountMain AccountMain { get; set; }

        public Account Account { get; set; }
    }
    //权限数据
    #region
    /*
----------------------------------------------
declare @a varchar(8000)
set @a=''
select @a=@a+'"'+Token+'",' FROM (SELECT a.MenuID,b.Name,b.Token FROM dbo.RoleMenu a 
LEFT JOIN dbo.Menu b
ON a.MenuID =b.ID) AS B
select @a 
------------------------------------------------
     
1	首页	Token_Home
2	管理	Token_Manage
3	产品	Token_Product
4	账号	Token_Account
5	项目	Token_Project
6	调查 活动 资讯	Token_News
7	App设置	Token_AppSet
8	设置	Token_Set
9	用户管理	Token_User
10	消息管理	Token_Message
11	会员管理	Token_Member
12	产品管理	Token_Product_M
13	类别管理	Token_Product_T
14	订单管理	Token_Product_O1
18	账号管理	Token_Account_M
19	角色管理	Token_Account_R
20	项目管理	Token_Project_M
21	项目和账号管理	Token_Project_M2
22	售楼部信息	Token_Project_I
23	用户端资讯	Token_News_U
24	销售端资讯	Token_News_S
25	调查问卷	Token_News_Su
26	活动	Token_News_A
27	关键词自动回复	Token_Keyword
28	App等待画面	Token_Waitpage
29	账号信息	Token_Account_I
30	素材管理	Token_Library
31	用户管理	Token_User_M
32	销售消息	Token_User_Msg
33	销售与用户管理	Token_User_SM
34	新建消息	Token_Message_N
35	已发送	Token_Message_H
36	会员信息	Token_Member_I
37	卡片管理	Token_Member_C
38	单元管理	Token_House_I
39	户型管理	Token_House_T
40	图片素材	Token_Library_I
41	语音素材	Token_Library_Vo
42	视频素材	Token_Library_Vi
43	图文素材	Token_Library_IT
44	角色管理	Token_Role
45	账号管理	Token_Project_Account
46	报表管理	Token_Project_Report
47	房屋管理	Token_House_M
48	聊天窗口	Token_Chat
49	App权限	Token_App_R
50	会员管理	Token_App_U
     * */
    #endregion
}
