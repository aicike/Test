using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.WebAPI_Poco;

namespace MicroSite_Web.Controllers
{
    public class CenterController : UserBaseController
    {
        public ActionResult Index(int amid)
        {
            ViewBag.AMID = amid;
            //if (LoginUser == null)
            //{
            //    //没有登录
            //    return RedirectToAction("Login", new { amid = amid });
            //}
            //else
            //{
            return View();
            //}
        }

        [HttpGet]
        public ActionResult Login(int amid)
        {
            ViewBag.AMID = amid;
            return View();
        }

        [HttpPost]
        public string Login(int amid, string LoginName, string LoginPwd)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var result = userLoginInfoModel.MicroSite_Login(amid, LoginName, LoginPwd);
            if (result.HasError)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            else
            {
                var user = result.Entity as User;
                result.Entity = user.ID;
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
        }

        //[HttpPost]
        //public ActionResult LoginNoAjax(int amid, string hidName, string hidPwd)
        //{
        //    return RedirectToAction("Index", new { amid = amid });
        //    var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
        //    var result = userLoginInfoModel.MicroSite_Login(amid, hidName, hidPwd);
        //    if (result.HasError)
        //    {
        //        throw new Exception(result.Error);
        //    }
        //    else
        //    {
        //        var loginUser = result.Entity as User;
        //        if (loginUser == null)
        //        {
        //            throw new Exception("账号或密码错误。");
        //        }
        //        else
        //        {
        //            User user = new Poco.User();
        //            UserLoginInfo userLoginInfo = new UserLoginInfo();
        //            userLoginInfo.ID = loginUser.UserLoginInfo.ID;
        //            userLoginInfo.Name = loginUser.UserLoginInfo.Name;
        //            userLoginInfo.Address = loginUser.UserLoginInfo.Address;
        //            userLoginInfo.Phone = loginUser.UserLoginInfo.Phone;
        //            userLoginInfo.HeadImagePath = loginUser.UserLoginInfo.HeadImagePath;
        //            userLoginInfo.Email = loginUser.UserLoginInfo.Email;
        //            user.UserLoginInfo = userLoginInfo;
        //            user.ID = loginUser.ID;
        //            user.UserLoginInfoID = loginUser.UserLoginInfo.ID;
        //            Session["aaaaaa"] = 1;
        //            Session[SystemConst.Session.LoginUser] = user;
        //            return RedirectToAction("Index", new { amid = amid });
        //        }
        //    }
        //}

        [HttpGet]
        public ActionResult Register(int amid)
        {
            ViewBag.AMID = amid;
            return View();
        }

        [HttpPost]
        public string Register(int amid, string reg_phone, string reg_name, string reg_pwd, string reg_confirmPwd)
        {
            if (reg_pwd != reg_confirmPwd)
            {
                return "两次密码不一致。";
            }

            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            App_UserLoginInfo userLoginInfo = new App_UserLoginInfo();
            userLoginInfo.Phone = reg_phone;
            userLoginInfo.Name = reg_name;
            userLoginInfo.Pwd = reg_pwd;
            userLoginInfo.AccountMainID = amid;
            var result = userLoginInfoModel.MicroSite_Register(userLoginInfo);
            if (result.HasError)
            {
            }
            else
            {
                var user = result.Entity as User;
                result.Entity = user.ID;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
    }
}
