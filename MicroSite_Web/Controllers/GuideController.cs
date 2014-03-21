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
    public class GuideController : ManageAccountController
    {
        [AllowCheckPermissions(false)]
        public ActionResult Index()
        {
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
                    accountMain.HostName = System.Configuration.ConfigurationManager.AppSettings["HostName"];
                    accountMain.LogoImageThumbnailPath = "";
                    accountMain.LogoImagePath = "";
                    accountMain.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
                    accountMain.FileLimit = 10;
                    CommonModel com = new CommonModel();
                    accountMain.RandomCode = com.CreateRandom("", 6);
                    accountMain.CreateTime = DateTime.Now;
                    accountMain.SalePhone = c.AccountMain.Phone;
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
                IAccountModel accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
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
                var account = account_accountMainModel.List().Select(a => a.Account).FirstOrDefault();
                account.IsSuperAdmin = true;
                account.CurrentAccountMainID = account.Account_AccountMains.FirstOrDefault().AccountMainID;
                Session[SystemConst.Session.LoginAccount] = account;

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
}
