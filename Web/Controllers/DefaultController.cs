using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;

namespace Web.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult AppView(int id)
        {
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var entity = libraryModel.Get(id);
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "图文信息", "", WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(entity);
        }

        public ActionResult Calculator()
        {
            return View();
        }

        #region 找回密码

        [HttpGet]

        public ActionResult FindPwd(string code)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            Result result = userLoginInfoModel.FindPwd_CheckCode(code);
            ViewBag.Result = result;
            ViewBag.Code = code;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "找回密码", "", WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        public ActionResult FindPwd(string txtNewPwd, string code)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            Result result = userLoginInfoModel.FindPwd_ChangePwd(code, txtNewPwd);
            if (result.HasError)
            {
                false.NotAuthorizedPage(result.Error);
            }
            ViewBag.Ok = "true";
            return View();
        }

        public ActionResult ContactUs() {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "联系我们", WebTitleRemark,"");
            ViewBag.Title = webTitle;
            return View();
        }

        #endregion
    }
}
