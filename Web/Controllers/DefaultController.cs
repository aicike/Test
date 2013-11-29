using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Business;
using System.IO;

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

        public ActionResult Advertorial(int id)
        {
            var AdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var advertorial = AdvertorialModel.Get(id);

            return View(advertorial);
        }

        public ActionResult News(int id)
        {
            var AdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var advertorial = AdvertorialModel.Get(id);

            return View(advertorial);
        }


        

        #endregion

        public ActionResult AppQrCode(int AMID)
        {
            ViewBag.AMID = AMID;
            return View();
        }

        public ActionResult QrCode(int AMID)
        {
            ViewBag.AMID = AMID;
            return View();
        }

        /// <summary>
        /// 二维码用户端
        /// </summary>
        /// <param name="AMID"></param>
        public void CreateQrCode(int AMID)
        {
            QrCodeModel model = new QrCodeModel();
            string url = "http://"+SystemConst.WebUrl + "/Default/QrCodeSkip?AMID=" + AMID;
            MemoryStream ms = model.Get_Android_DownloadUrl(url);
            if (null != ms)
            {
                Response.BinaryWrite(ms.ToArray());
            }
        }


        /// <summary>
        /// 二维码销售端
        /// </summary>
        /// <param name="AMID"></param>
        public void CreateQrCodeAccount(int AMID)
        {
            QrCodeModel model = new QrCodeModel();
            string url = "http://" + SystemConst.WebUrl + "/Default/QrCodeSkipAccount?AMID=" + AMID;
            MemoryStream ms = model.Get_Android_DownloadUrl(url);
            if (null != ms)
            {
                Response.BinaryWrite(ms.ToArray());
            }
        }

        /// <summary>
        /// 二维码用户端界面
        /// </summary>
        /// <param name="AMID"></param>
        public ActionResult QrCodeSkip(int AMID)
        {
            var AccountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var AccountModel = AccountMainModel.Get(AMID);
            if (AccountModel.AndroidDownloadPath!=null)
                AccountModel.AndroidDownloadPath = "http://" + SystemConst.WebUrl + Url.Content(AccountModel.AndroidDownloadPath);
            return View(AccountModel);
        }

        /// <summary>
        /// 二维码销售端界面
        /// </summary>
        /// <param name="AMID"></param>
        public ActionResult QrCodeSkipAccount(int AMID)
        {
            var AccountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var AccountModel = AccountMainModel.Get(AMID);
            if (AccountModel.AndroidSellDownloadPath != null)
                AccountModel.AndroidSellDownloadPath = "http://" + SystemConst.WebUrl + Url.Content(AccountModel.AndroidSellDownloadPath);
            return View(AccountModel);
        }



    }
}
